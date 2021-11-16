using System.Reflection.Metadata.Ecma335;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        private readonly ScoreBoard _scoreBoard;

        public TennisGame1()
        {
            _scoreBoard = new ScoreBoard(new Player(), new Player());
        }

        public void WinPoint(string playerName)
        {
            switch (playerName)
            {
                case "player1":
                    _scoreBoard.PlayerOne.Points++;
                    break;
                case "player2":
                    _scoreBoard.PlayerTwo.Points++;
                    break;
                default:
                    throw new PlayerNameUnknown();
            }
        }

        public string GetScore() => _scoreBoard.GetScore();
    }
}

