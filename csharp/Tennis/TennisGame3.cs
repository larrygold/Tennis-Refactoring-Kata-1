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
        private string[] _individualScores;

        public TennisGame3(string player1Name, string player2Name)
        {
            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
            _individualScores = new string[] { "Love", "Fifteen", "Thirty", "Forty" };
        }

        public string GetScore()
        {
            if (IsEquality())
            {
                if (IsDeuce())
                {
                    return "Deuce";
                }

                return GetPlayerScore(_player1) + "-All";
            }

            if (IsWinOrAdvantage())
            {
                var scoresDelta = Math.Abs(_player1.Points - _player2.Points);

                if (scoresDelta == 1)
                {
                    return "Advantage " + GetLeadingPlayerScore();
                }

                return "Win for " + GetLeadingPlayerScore();
            }


            return GetPlayerScore(_player1) + "-" + GetPlayerScore(_player2);
        }

        private bool IsDeuce()
        {
            return _player1.Points >= 3;
        }

        private string GetPlayerScore(Player player)
        {
            if (player.Points <= 3)
            {
                return _individualScores[player.Points];
            }

            // TO DO
            throw new Exception();
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

