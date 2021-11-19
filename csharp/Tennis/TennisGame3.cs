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
            var score = "";

            if ((_player1Points < 4 && _player2Points < 4) && (_player1Points + _player2Points < 6))
            {
                var individualScores = new string[] { "Love", "Fifteen", "Thirty", "Forty" };
                score = individualScores[_player1Points];
                if (_player1Points == _player2Points)
                {
                    return score + "-All";
                }
                else
                {
                    return score + "-" + individualScores[_player2Points];
                }
            }
            else
            {
                if (_player1Points == _player2Points)
                    return "Deuce";
                score = _player1Points > _player2Points ? _player1Name : _player2Name;
                return ((_player1Points - _player2Points) * (_player1Points - _player2Points) == 1) ? "Advantage " + score : "Win for " + score;
            }
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

