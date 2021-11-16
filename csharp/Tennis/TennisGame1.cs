using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class Player
    {
        public int Score { get; set; }
    }

    public class Players
    {
        private Player _playerOne;
        private Player _playerTwo;

        public Players(Player playerOne, Player playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

        public bool HaveEqualScores() => _playerOne.Score == _playerTwo.Score;
    }

    public class TennisGame1 : ITennisGame
    {
        private readonly Dictionary<int, string> _pointsToScore;

        private readonly Players _players;
        private readonly Player _playerOne;
        private readonly Player _playerTwo;

        public TennisGame1()
        {
            _playerOne = new Player();
            _playerTwo = new Player();
            _players = new Players(_playerOne, _playerTwo);
            _pointsToScore = new()
            {
                {0, "Love"},
                {1, "Fifteen"},
                {2, "Thirty"},
                {3, "Forty"}
            };
        }

        public void WinPoint(string playerName)
        {
            switch (playerName)
            {
                case "player1":
                    _playerOne.Score ++;
                    break;
                case "player2":
                    _playerTwo.Score++;
                    break;
                default:
                    throw new PlayerNameUnknown();
            }
        }

        public string GetScore()
        {
            if (IsEquality())
            {
                return GetEqualScore();
            }

            if (IsAdvantageOrWin())
            {
                return GetAdvantageOrWinScore();
            }

            return GetOngoingScore();
        }

        private string GetOngoingScore()
        {
            return GetIndividualScore(_playerOne.Score) + "-" + GetIndividualScore(_playerTwo.Score);
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(_playerOne.Score - _playerTwo.Score);
            var leadingPlayerName = _playerOne.Score > _playerTwo.Score ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetEqualScore()
        {
            if (_playerOne.Score <= 2)
                return _pointsToScore[_playerOne.Score] + "-All";

            return "Deuce";
        }

        private string GetIndividualScore(int points)
        {
            if (_pointsToScore.ContainsKey(points))
                return _pointsToScore[points];

            throw new Exception();
        }

        private bool IsAdvantageOrWin()
        {
            return _playerOne.Score >= 4 || _playerTwo.Score >= 4;
        }

        private bool IsEquality()
        {
            return _players.HaveEqualScores();
        }
    }

    internal class PlayerNameUnknown : Exception
    {
    }
}

