using System;
using Tennis;

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

    public enum Score
    {
        Love = 0,
        Fifteen = 1,
        Thirty = 2,
        Forty = 3
    }

    public class TennisGame3 : ITennisGame
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly string[] _individualScores;

        public TennisGame3(string player1Name, string player2Name)
        {
            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
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
                    return "Advantage " + GetLeadingPlayerName();
                }

                return "Win for " + GetLeadingPlayerName();
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
                var playerScore = (Score) player.Points;
                return playerScore.ToString();
            }

            // TO DO
            throw new Exception();
        }

        private string GetLeadingPlayerName()
        {
            if (_player1.Points > _player2.Points)
            {
                return _player1.Name;
            }

            return _player2.Name;
        }

        private bool IsWinOrAdvantage()
        {
            return _player1.Points >= 4 || 
                   _player2.Points >= 4 || 
                   _player1.Points + _player2.Points >= 6;
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

