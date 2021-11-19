namespace Tennis
{
    public class EqualScore : Score
    {
        public EqualScore(Player playerOne, Player playerTwo) : base(playerOne, playerTwo)
        {
        }

        public override string Get()
        {
            if (PlayerOne.Points <= 2)
                return PointsToScore[PlayerOne.Points] + "-All";

            return "Deuce";
        }

        public override bool ShouldUse()
        {
            return PlayerOne.Points == PlayerTwo.Points;
        }
    }
}