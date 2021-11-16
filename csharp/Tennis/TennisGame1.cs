using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class Player
    {
        public int Score { get; set; }
    }

    public class EqualScore : IScore
    {
        private int _points;
        private readonly Dictionary<int, string> _pointsToScore;

        public EqualScore(int points)
        {
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
                return GetEqualScore();
            }

            if (OneHasAdvantageOrWins())
            {
                return GetAdvantageOrWinScore();
            }

            return GetOngoingScore();
        }

        private bool HaveEqualScores() => PlayerOne.Score == PlayerTwo.Score;

        private bool OneHasAdvantageOrWins() => PlayerOne.Score >= 4 || PlayerTwo.Score >= 4;

        private string GetIndividualScore(int points)
        {
            if (_pointsToScore.ContainsKey(points))
                return _pointsToScore[points];

            throw new Exception();
        }

        private string GetEqualScore()
        {
            if (PlayerOne.Score <= 2)
                return _pointsToScore[PlayerOne.Score] + "-All";

            return "Deuce";
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(PlayerOne.Score - PlayerTwo.Score);
            var leadingPlayerName = PlayerOne.Score > PlayerTwo.Score ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetOngoingScore()
        {
            return GetIndividualScore(PlayerOne.Score) + "-" + GetIndividualScore(PlayerTwo.Score);
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
                    _scoreBoard.PlayerOne.Score++;
                    break;
                case "player2":
                    _scoreBoard.PlayerTwo.Score++;
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

