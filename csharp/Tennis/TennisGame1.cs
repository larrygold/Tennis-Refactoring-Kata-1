using System;
using System.Collections.Generic;

namespace Tennis
{
    class TennisGame1 : ITennisGame
    {
        private int _scorePlayerOne;
        private int _scorePlayerTwo;

        private static readonly Dictionary<int, string> PointsToScore = new()
        {
            {0, "Love"},
            {1, "Fifteen"},
            {2, "Thirty"},
            {3, "Forty"}
        };

        public void WinPoint(string playerName)
        {
            if (playerName == "player1")
                _scorePlayerOne ++;
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
            return GetScore(_scorePlayerOne) + "-" + GetScore(_scorePlayerTwo);
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(_scorePlayerOne - _scorePlayerTwo);
            var leadingPlayerName = _scorePlayerOne > _scorePlayerTwo ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetEqualScore()
        {
            if (_scorePlayerOne <= 2)
                return PointsToScore[_scorePlayerOne] + "-All";

            return "Deuce";
        }

        private static string GetScore(int tempScore)
        {
            if (PointsToScore.ContainsKey(tempScore))
                return PointsToScore[tempScore];

            throw new Exception();
        }

        private bool IsAdvantageOrWin()
        {
            return _scorePlayerOne >= 4 || _scorePlayerTwo >= 4;
        }

        private bool IsEquality()
        {
            return _scorePlayerOne == _scorePlayerTwo;
        }
    }
}

