using System;

namespace Tennis
{
    public class AdvantageOrWinScore : Score
    {
        public AdvantageOrWinScore(Player playerOne, Player playerTwo) : base(playerOne, playerTwo)
        {
        }
        public override string Get()
        {
            var scoresDelta = Math.Abs(PlayerOne.Points - PlayerTwo.Points);
            var leadingPlayerName = PlayerOne.Points > PlayerTwo.Points ? "player1" : "player2";

            if (scoresDelta == 1)
                return $"Advantage {leadingPlayerName}";

            return $"Win for {leadingPlayerName}";
        }

        public override bool ShouldUse()
        {
            return PlayerOne.Points >= 4 || PlayerTwo.Points >= 4;
        }
    }
}