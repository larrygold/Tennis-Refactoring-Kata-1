using System;

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

            var score = "";
            if (IsAdvantageOrWin())
            {
                var scoresDelta = Math.Abs(_scorePlayerOne - _scorePlayerTwo);
                var leadingPlayerName = _scorePlayerOne > _scorePlayerTwo ? "player1" : "player2";

                if (scoresDelta == 1)
                {
                    return $"Advantage {leadingPlayerName}";
                }
                else
                {
                    return $"Win for {leadingPlayerName}";
                }

                /*
                var minusResult = _scorePlayerOne - _scorePlayerTwo;
                if (minusResult == 1) score = "Advantage player1";
                else if (minusResult == -1) score = "Advantage player2";
                else if (minusResult >= 2) score = "Win for player1";
                else score = "Win for player2";
            */
            }
            else
            {
                for (var i = 1; i < 3; i++)
                {
                    var tempScore = 0;
                    if (i == 1) tempScore = _scorePlayerOne;
                    else { score += "-"; tempScore = _scorePlayerTwo; }
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
                }
            }
            return score;
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

