using System;

namespace Tennis
{
    public class OngoingScore : Score
    {
        public OngoingScore(Player playerOne, Player playerTwo) : base (playerOne, playerTwo)
        {
        }

        public override string Get()
        {
            return GetIndividualScore(PlayerOne.Points) + "-" + GetIndividualScore(PlayerTwo.Points);
        }

        public override bool ShouldUse()
        {
            return true;
        }

        private string GetIndividualScore(int points)
        {
            if (PointsToScore.ContainsKey(points))
                return PointsToScore[points];

            throw new Exception();
        }
    }
}