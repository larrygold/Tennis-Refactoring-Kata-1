using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        private readonly List<Score> _scores;

        public TennisGame1(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            _scores = new List<Score>() {new EqualScore(PlayerOne, PlayerTwo), new AdvantageOrWinScore(PlayerOne, PlayerTwo),
                new OngoingScore(PlayerOne, PlayerTwo)};
        }

        public void WinPoint(Player player)
        {
            if (player != PlayerOne && player != PlayerTwo)
                throw new PlayerNameUnknown();

            player.Points++;
        }

        public string GetScore()
        {
            foreach (var score in _scores)
            {
                if (score.ShouldUse())
                {
                    return score.Get();
                }
            }

            throw new Exception();

        }
    }
}

