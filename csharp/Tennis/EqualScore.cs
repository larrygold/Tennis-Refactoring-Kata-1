namespace Tennis
{
    public class EqualScore : Score
    {
        public EqualScore(Player playerOne) : base(playerOne)
        {
        }

        public override string Get()
        {
            if (PlayerOne.Points <= 2)
                return PointsToScore[PlayerOne.Points] + "-All";

            return "Deuce";
        }

    }
}