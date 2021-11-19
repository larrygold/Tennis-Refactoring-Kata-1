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
            Points = 0;
        }
    }

    public class TennisGame3 : ITennisGame
    {
        private readonly Player _player1;
        private readonly Player _player2;

        public TennisGame3(string player1Name, string player2Name)
        {
            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
        }

        public string GetScore()
        {
            var individualScores = new string[] { "Love", "Fifteen", "Thirty", "Forty" };
            var scorePlayer1 = "";
            if (_player1.Points <= 3)
            {
                scorePlayer1 = individualScores[_player1.Points];
            }

            if (IsEquality())
            {
                if (_player1.Points < 3)
                {
                    return scorePlayer1 + "-All";
                }

                return "Deuce";
            }

            if (IsWinOrAdvantage())
            {
                if ((_player1.Points - _player2.Points) * (_player1.Points - _player2.Points) == 1)
                {
                    return "Advantage " + GetLeadingPlayerScore();
                }

                return "Win for " + GetLeadingPlayerScore();
            }


            var scorePlayer2 = individualScores[_player2.Points];
            return scorePlayer1 + "-" + scorePlayer2;
        }

        private string GetLeadingPlayerScore()
        {
            string scorePlayer1;
            if (_player1.Points > _player2.Points)
            {
                scorePlayer1 = _player1.Name;
            }
            else
            {
                scorePlayer1 = _player2.Name;
            }

            return scorePlayer1;
        }

        private bool IsWinOrAdvantage()
        {
            return _player1.Points >= 4 || _player2.Points >= 4 || _player1.Points + _player2.Points >= 6;
        }

        private bool IsEquality()
        {
            return _player1.Points == _player2.Points;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == _player1.Name)
                _player1.Points ++;
            else if (playerName == _player2.Name)
                _player2.Points++;
            else
                throw new InvalidPlayerException();
        }

    }

    public class InvalidPlayerException : Exception
    {
    }
}

