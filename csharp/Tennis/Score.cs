using System.Collections.Generic;

namespace Tennis
{
    public abstract class Score
    {
        protected readonly Dictionary<int, string> PointsToScore = new()
        {
            { 0, "Love" },
            { 1, "Fifteen" },
            { 2, "Thirty" },
            { 3, "Forty" }
        };

        protected Player PlayerOne;
        protected Player PlayerTwo;

        protected Score(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }

        protected Score(Player playerOne)
        {
            PlayerOne = playerOne;
        }

        public abstract string Get();
    }
}