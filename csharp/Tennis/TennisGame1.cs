using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        private readonly ScoreBoard _scoreBoard;

        public TennisGame1(Player playerOne, Player playerTwo)
        {
            _scoreBoard = new ScoreBoard(playerOne, playerTwo);
        }

        public void WinPoint(Player player)
        {
            if (player != _scoreBoard.PlayerOne && player != _scoreBoard.PlayerTwo)
                throw new PlayerNameUnknown();

            player.Points++;
        }

        public string GetScore() => _scoreBoard.GetScore();
    }
}

