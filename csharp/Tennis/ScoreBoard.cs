namespace Tennis
{
    public class ScoreBoard
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public ScoreBoard(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }

        public string GetScore()
        {
            if (HaveEqualScores())
            {
                return new EqualScore(PlayerOne).Get();
            }

            if (OneHasAdvantageOrWins())
            {
                return new AdvantageOrWinScore(PlayerOne, PlayerTwo).Get();
            }

            return new OngoingScore(PlayerOne, PlayerTwo).Get();
        }

        private bool HaveEqualScores() => PlayerOne.Points == PlayerTwo.Points;

        private bool OneHasAdvantageOrWins() => PlayerOne.Points >= 4 || PlayerTwo.Points >= 4;
    }
}