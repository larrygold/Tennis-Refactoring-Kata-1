using System;
using System.Collections.Generic;

namespace Tennis
{
    public class ScoreBoard
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        private List<Score> _scores;

        public ScoreBoard(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            _scores = new List<Score>() {new EqualScore(PlayerOne, PlayerTwo), new AdvantageOrWinScore(PlayerOne, PlayerTwo),
                new OngoingScore(PlayerOne, PlayerTwo)};
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