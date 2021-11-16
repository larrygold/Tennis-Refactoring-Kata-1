using System;
using System.Collections.Generic;

namespace Tennis
{
    public class Player
    {
        public int _scorePlayerOne { get; set; }
        public int _scorePlayerTwo { get; set; }
    }

    public class TennisGame1 : ITennisGame
    {
        private static readonly Dictionary<int, string> PointsToScore = new()
        {
            {0, "Love"},
            {1, "Fifteen"},
            {2, "Thirty"},
            {3, "Forty"}
        };

        private readonly Player _player = new Player();

        public void WinPoint(string playerName)
        {
            if (playerName == "player1")
                _player._scorePlayerOne ++;
            else
                _player._scorePlayerTwo ++;
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
            return GetIndividualScore(_player._scorePlayerOne) + "-" + GetIndividualScore(_player._scorePlayerTwo);
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(_player._scorePlayerOne - _player._scorePlayerTwo);
            var leadingPlayerName = _player._scorePlayerOne > _player._scorePlayerTwo ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetEqualScore()
        {
            if (_player._scorePlayerOne <= 2)
                return PointsToScore[_player._scorePlayerOne] + "-All";

            return "Deuce";
        }

        private static string GetIndividualScore(int points)
        {
            if (PointsToScore.ContainsKey(points))
                return PointsToScore[points];

            throw new Exception();
        }

        private bool IsAdvantageOrWin()
        {
            return _player._scorePlayerOne >= 4 || _player._scorePlayerTwo >= 4;
        }

        private bool IsEquality()
        {
            return _player._scorePlayerOne == _player._scorePlayerTwo;
        }
    }
}

