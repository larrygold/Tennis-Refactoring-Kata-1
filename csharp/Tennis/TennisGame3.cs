using System;

namespace Tennis
{
    public class TennisGame3 : ITennisGame
    {
        private int _player2Points;
        private int _player1Points;
        private string _player1Name;
        private string _player2Name;

        public TennisGame3(string player1Name, string player2Name)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        public string GetScore()
        {
            var individualScores = new string[] { "Love", "Fifteen", "Thirty", "Forty" };
            var scorePlayer1 = "";
            if (_player1Points <= 3)
            {
                scorePlayer1 = individualScores[_player1Points];
            }


            if (IsEquality())
            {
                if (_player1Points < 3)
                {
                    return scorePlayer1 + "-All";
                }

                return "Deuce";
            }

            if ((_player1Points < 4 && _player2Points < 4) &&(_player1Points + _player2Points < 6))
            {
                var scorePlayer2 = individualScores[_player2Points];
                return scorePlayer1 + "-" + scorePlayer2;
            }
            else
            {
                if (_player1Points > _player2Points)
                {
                    scorePlayer1 = _player1Name;
                }
                else
                {
                    scorePlayer1 = _player2Name;
                }

                if ((_player1Points - _player2Points) * (_player1Points - _player2Points) == 1)
                {
                    return "Advantage " + scorePlayer1;
                }

                return "Win for " + scorePlayer1;

            }
        }

        private bool IsEquality()
        {
            return _player1Points == _player2Points;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == _player1Name)
                _player1Points ++;
            else if (playerName == _player2Name)
                _player2Points ++;
            else
                throw new InvalidPlayerException();
        }

    }

    public class InvalidPlayerException : Exception
    {
    }
}

