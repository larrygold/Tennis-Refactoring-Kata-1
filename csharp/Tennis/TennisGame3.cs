using System;

namespace Tennis
{
    public class Player
    {
        public int Points { get; set; }
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }
    }

    public class TennisGame3 : ITennisGame
    {
        private int _player2Points;
        private string _player2Name;
        private readonly Player _player;

        public TennisGame3(string player1Name, string player2Name)
        {
            _player = new Player(player1Name);
            _player2Name = player2Name;
        }

        public string GetScore()
        {
            var individualScores = new string[] { "Love", "Fifteen", "Thirty", "Forty" };
            var scorePlayer1 = "";
            if (_player.Points <= 3)
            {
                scorePlayer1 = individualScores[_player.Points];
            }


            if (IsEquality())
            {
                if (_player.Points < 3)
                {
                    return scorePlayer1 + "-All";
                }

                return "Deuce";
            }

            if (_player.Points < 4 && _player2Points < 4 && _player.Points + _player2Points < 6)
            {
                var scorePlayer2 = individualScores[_player2Points];
                return scorePlayer1 + "-" + scorePlayer2;
            }

            if (_player.Points >= 4 || _player2Points >= 4 || _player.Points + _player2Points >= 6)
            {
                if (_player.Points > _player2Points)
                {
                    scorePlayer1 = _player.Name;
                }
                else
                {
                    scorePlayer1 = _player2Name;
                }

                if ((_player.Points - _player2Points) * (_player.Points - _player2Points) == 1)
                {
                    return "Advantage " + scorePlayer1;
                }

                return "Win for " + scorePlayer1;
            }

            throw new Exception();
        }

        private bool IsEquality()
        {
            return _player.Points == _player2Points;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == _player.Name)
                _player.Points ++;
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

