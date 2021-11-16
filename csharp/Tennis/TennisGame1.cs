using System;
using System.Collections.Generic;

namespace Tennis
{
    class TennisGame1 : ITennisGame
    {
        private int _scorePlayerOne;
        private int _scorePlayerTwo;

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
                switch (_scorePlayerOne)
                {
                    case 0:
                        return "Love-All";
                    case 1:
                        return "Fifteen-All";
                    case 2:
                        return "Thirty-All";
                    default:
                        return "Deuce";
                }
            }

            if (IsAdvantageOrWin())
            {
                var scoresDelta = Math.Abs(_scorePlayerOne - _scorePlayerTwo);
                var leadingPlayerName = _scorePlayerOne > _scorePlayerTwo ? "player1" : "player2";

                if (scoresDelta == 1)
                    return $"Advantage {leadingPlayerName}";

                return $"Win for {leadingPlayerName}";
            }

            var score = "";
            var tempScore = 0;

            tempScore = _scorePlayerOne;
            score = GetScore(tempScore);

            score += "-";

            tempScore = _scorePlayerTwo;
            switch (tempScore)
            {
                case 0:
                    score += "Love";
                    break;
                case 1:
                    score += "Fifteen";
                    break;
                case 2:
                    score += "Thirty";
                    break;
                case 3:
                    score += "Forty";
                    break;
            }

            return score;
        }

        private static string GetScore(int tempScore)
        {
            var pointsToScore = new Dictionary<int, string>()
            {
                {0, "Love"},
                {1, "Fifteen"},
                {2, "Thirty"},
                {3, "Forty"}
            };

            if (pointsToScore.ContainsKey(tempScore))
                return pointsToScore[tempScore];

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

