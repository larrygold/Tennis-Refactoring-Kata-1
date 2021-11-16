using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    class Player
    {
        public int Score { get; set; }
    }

    class TennisGame1 : ITennisGame
    {
        private int _scorePlayerTwo
        {
            get => _playerTwo.Score;
            set => _playerTwo.Score = value;
        }

        private  Dictionary<int, string> PointsToScore;

        private readonly Player _playerOne;

        private readonly Player _playerTwo;

        public TennisGame1()
        {
            _playerOne = new Player();
            _playerTwo = new Player();
            PointsToScore = new()
            {
                {0, "Love"},
                {1, "Fifteen"},
                {2, "Thirty"},
                {3, "Forty"}
            };
        }

        public void WinPoint(string playerName)
        {
            if (playerName == "player1")
                _playerOne.Score ++;
            else
                _scorePlayerTwo ++;
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
            return GetIndividualScore(_playerOne.Score) + "-" + GetIndividualScore(_scorePlayerTwo);
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(_playerOne.Score - _scorePlayerTwo);
            var leadingPlayerName = _playerOne.Score > _scorePlayerTwo ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetEqualScore()
        {
            if (_playerOne.Score <= 2)
                return PointsToScore[_playerOne.Score] + "-All";

            return "Deuce";
        }

        private string GetIndividualScore(int points)
        {
            if (PointsToScore.ContainsKey(points))
                return PointsToScore[points];

            throw new Exception();
        }

        private bool IsAdvantageOrWin()
        {
            return _playerOne.Score >= 4 || _scorePlayerTwo >= 4;
        }

        private bool IsEquality()
        {
            return _playerOne.Score == _scorePlayerTwo;
        }
    }
}

