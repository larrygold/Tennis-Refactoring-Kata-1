using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class Player
    {
        public int Score { get; set; }
    }

    public class Players
    {
        private readonly Dictionary<int, string> _pointsToScore;

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public Players(Player playerOne, Player playerTwo)
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

        public bool HaveEqualScores() => PlayerOne.Score == PlayerTwo.Score;
        public bool OneHasAdvantageOrWins() => PlayerOne.Score >= 4 || PlayerTwo.Score >= 4;

        public string GetIndividualScore(int points, TennisGame1 tennisGame1)
        {
            if (_pointsToScore.ContainsKey(points))
                return _pointsToScore[points];

            throw new Exception();
        }

        public string GetEqualScore()
        {
            if (PlayerOne.Score <= 2)
                return _pointsToScore[PlayerOne.Score] + "-All";

            return "Deuce";
        }

    }

    public class TennisGame1 : ITennisGame
    {
        private readonly Dictionary<int, string> _pointsToScore;

        private readonly Players _players;

        public TennisGame1()
        {
            _players = new Players(new Player(), new Player());
            _pointsToScore = new()
            {
                {0, "Love"},
                {1, "Fifteen"},
                {2, "Thirty"},
                {3, "Forty"}
            };
        }

        public void WinPoint(string playerName)
        {
            switch (playerName)
            {
                case "player1":
                    GetPlayerOne().Score ++;
                    break;
                case "player2":
                    GetPlayerTwo().Score++;
                    break;
                default:
                    throw new PlayerNameUnknown();
            }
        }

        public string GetScore()
        {
            if (IsEquality())
            {
                return GetEqualScore();
            }

            if (IsAdvantageOrWin())
            {
                return GetAdvantageOrWinScore();
            }

            return GetOngoingScore();
        }

        private Player GetPlayerOne()
        {
            return _players.PlayerOne;
        }

        private Player GetPlayerTwo()
        {
            return _players.PlayerTwo;
        }

        private string GetOngoingScore()
        {
            return _players.GetIndividualScore(GetPlayerOne().Score, this) + "-" + _players.GetIndividualScore(GetPlayerTwo().Score, this);
        }

        private string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(GetPlayerOne().Score - GetPlayerTwo().Score);
            var leadingPlayerName = GetPlayerOne().Score > GetPlayerTwo().Score ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        private string GetEqualScore()
        {
            return _players.GetEqualScore();
        }

        private bool IsAdvantageOrWin()
        {
            return _players.OneHasAdvantageOrWins();
        }

        private bool IsEquality()
        {
            return _players.HaveEqualScores();
        }
    }

    internal class PlayerNameUnknown : Exception
    {
    }
}

