using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class Player
    {
        public int Points { get; set; }
    }

    public class EqualScore : IScore
    {
        private int _points;
        private readonly Dictionary<int, string> _pointsToScore;

        public EqualScore(int points)
        {
            _points = points;
            _pointsToScore = new()
            {
                { 0, "Love" },
                { 1, "Fifteen" },
                { 2, "Thirty" },
                { 3, "Forty" }
            };
        }

        public string Get()
        {
            if (_points <= 2)
                return _pointsToScore[_points] + "-All";

            return "Deuce";
        }

    }

    public interface IScore
    {
        public string Get();
    }

    public class ScoreBoard
    {
        private readonly Dictionary<int, string> _pointsToScore;

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public ScoreBoard(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            _pointsToScore = new()
            {
                { 0, "Love" },
                { 1, "Fifteen" },
                { 2, "Thirty" },
                { 3, "Forty" }
            };
        }

        public string GetScore()
        {
            if (HaveEqualScores())
            {
                return new EqualScore(PlayerOne.Points).Get();
            }

            if (OneHasAdvantageOrWins())
            {
                return GetAdvantageOrWinScore();
            }

            return GetOngoingScore();
        }

        private bool HaveEqualScores() => PlayerOne.Points == PlayerTwo.Points;

        private bool OneHasAdvantageOrWins() => PlayerOne.Points >= 4 || PlayerTwo.Points >= 4;

        private string GetIndividualScore(int points)
        {
            if (_pointsToScore.ContainsKey(points))
                return _pointsToScore[points];

            throw new Exception();
        }

        private string GetEqualScore()
        {
            if (PlayerOne.Points <= 2)
                return _pointsToScore[PlayerOne.Points] + "-All";

            return "Deuce";
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(PlayerOne.Points - PlayerTwo.Points);
            var leadingPlayerName = PlayerOne.Points > PlayerTwo.Points ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetOngoingScore()
        {
            return GetIndividualScore(PlayerOne.Points) + "-" + GetIndividualScore(PlayerTwo.Points);
        }

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

