using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class Player
    {
        public int Points { get; set; }
    }

    public class EqualScore : Score
    {
        private Player _playerOne;

        public EqualScore(int points, Player playerOne)
        {
            _playerOne = playerOne;
        }

        public override string Get()
        {
            if (_playerOne.Points <= 2)
                return PointsToScore[_playerOne.Points] + "-All";

            return "Deuce";
        }

    }

    public class AdvantageOrWinScore : Score
    {
        private Player _playerOne;
        private Player _playerTwo;
        public AdvantageOrWinScore(Player playerOne, Player playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }
        public override string Get()
        {
            var scoresDelta = Math.Abs(_playerOne.Points - _playerTwo.Points);
            var leadingPlayerName = _playerOne.Points > _playerTwo.Points ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }
    }

    public class OngoingScore : Score
    {
        private readonly Player _playerOne;
        private readonly Player _playerTwo;

        public OngoingScore(Player playerOne, Player playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

        public override string Get()
        {
            return GetIndividualScore(_playerOne.Points) + "-" + GetIndividualScore(_playerTwo.Points);
        }

        private string GetIndividualScore(int points)
        {
            if (PointsToScore.ContainsKey(points))
                return PointsToScore[points];

            throw new Exception();
        }
    }

    public abstract class Score
    {
        protected readonly Dictionary<int, string> PointsToScore = new()
        {
            { 0, "Love" },
            { 1, "Fifteen" },
            { 2, "Thirty" },
            { 3, "Forty" }
        };
        public abstract string Get();
    }

    public class ScoreBoard
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public ScoreBoard(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }

        public string GetScore()
        {
            if (HaveEqualScores())
            {
                return new EqualScore(PlayerOne.Points, PlayerOne).Get();
            }

            if (OneHasAdvantageOrWins())
            {
                return new AdvantageOrWinScore(PlayerOne, PlayerTwo).Get();
            }

            return new OngoingScore(PlayerOne, PlayerTwo).Get();
        }

        private bool HaveEqualScores() => PlayerOne.Points == PlayerTwo.Points;

        private bool OneHasAdvantageOrWins() => PlayerOne.Points >= 4 || PlayerTwo.Points >= 4;
    }

    public class TennisGame1 : ITennisGame
    {
        private readonly ScoreBoard _scoreBoard;

        public TennisGame1()
        {
            _scoreBoard = new ScoreBoard(new Player(), new Player());
        }

        public void WinPoint(string playerName)
        {
            switch (playerName)
            {
                case "player1":
                    _scoreBoard.PlayerOne.Points++;
                    break;
                case "player2":
                    _scoreBoard.PlayerTwo.Points++;
                    break;
                default:
                    throw new PlayerNameUnknown();
            }
        }

        public string GetScore() => _scoreBoard.GetScore();
    }

    internal class PlayerNameUnknown : Exception
    {
    }
}

