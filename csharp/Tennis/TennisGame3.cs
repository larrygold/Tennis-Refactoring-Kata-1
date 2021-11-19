namespace Tennis
{
    public class TennisGame3 : ITennisGame
    {
        private int _player2;
        private int _player1;
        private string _player1Name;
        private string _player2Name;

        public TennisGame3(string player1Name, string player2Name)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        public string GetScore()
        {
            string s;
            if ((_player1 < 4 && _player2 < 4) && (_player1 + _player2 < 6))
            {
                string[] p = { "Love", "Fifteen", "Thirty", "Forty" };
                s = p[_player1];
                return (_player1 == _player2) ? s + "-All" : s + "-" + p[_player2];
            }
            else
            {
                if (_player1 == _player2)
                    return "Deuce";
                s = _player1 > _player2 ? _player1Name : _player2Name;
                return ((_player1 - _player2) * (_player1 - _player2) == 1) ? "Advantage " + s : "Win for " + s;
            }
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
                _player1 += 1;
            else
                _player2 += 1;
        }

    }
}

