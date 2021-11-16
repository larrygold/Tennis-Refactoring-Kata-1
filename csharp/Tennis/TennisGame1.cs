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

        public string GetIndividualScore(int points)
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

        public string GetAdvantageOrWinScore()
        {
            var scoresDelta = Math.Abs(PlayerOne.Score - PlayerTwo.Score);
            var leadingPlayerName = PlayerOne.Score > PlayerTwo.Score ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        public string GetOngoingScore()
        {
            return GetIndividualScore(PlayerOne.Score) + "-" + GetIndividualScore(PlayerTwo.Score);
        }


    }

    public class TennisGame1 : ITennisGame
    {
        private readonly Players _players;

        public TennisGame1()
        {
            _players = new Players(new Player(), new Player());
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
            if (_players.HaveEqualScores())
            {
                return _players.GetEqualScore();
            }

            if (_players.OneHasAdvantageOrWins())
            {
                return _players.GetAdvantageOrWinScore();
            }

            return _players.GetOngoingScore();
        }

        private Player GetPlayerOne()
        {
            return _players.PlayerOne;
        }

        private Player GetPlayerTwo()
        {
            return _players.PlayerTwo;
        }
    }

    internal class PlayerNameUnknown : Exception
    {
    }
}

