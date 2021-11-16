using System;
using System.Collections.Generic;

namespace Tennis
{
    public class Player
    {
        public int ScorePlayerOne { get; set; }
        public int ScorePlayerTwo { get; set; }
    }

    public class TennisGame1 : ITennisGame
    {
        private static Dictionary<int, string> _pointsToScore;

        private readonly Player _playerOne;
        private readonly Player _playerTwo;

        public TennisGame1()
        {
            _playerOne = new Player();
            _playerTwo = new Player();
            _pointsToScore = new()
            {
                { 0, "Love" },
                { 1, "Fifteen" },
                { 2, "Thirty" },
                { 3, "Forty" }
            };
        }

        public void WinPoint(string playerName)
        {
            if (playerName == "player1")
                _playerOne.ScorePlayerOne ++;
            else
                _playerOne.ScorePlayerTwo ++;
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
            return GetIndividualScore(_playerOne.ScorePlayerOne) + "-" + GetIndividualScore(_playerOne.ScorePlayerTwo);
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(_playerOne.ScorePlayerOne - _playerOne.ScorePlayerTwo);
            var leadingPlayerName = _playerOne.ScorePlayerOne > _playerOne.ScorePlayerTwo ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetEqualScore()
        {
            if (_playerOne.ScorePlayerOne <= 2)
                return _pointsToScore[_playerOne.ScorePlayerOne] + "-All";

            return "Deuce";
        }

        private static string GetIndividualScore(int points)
        {
            if (_pointsToScore.ContainsKey(points))
                return _pointsToScore[points];

            throw new Exception();
        }

        private bool IsAdvantageOrWin()
        {
            return _playerOne.ScorePlayerOne >= 4 || _playerOne.ScorePlayerTwo >= 4;
        }

        private bool IsEquality()
        {
            return _playerOne.ScorePlayerOne == _playerOne.ScorePlayerTwo;
        }
    }
}

