using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ihm_main.DTO
{
    class Game
    {
        private string name = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        // 1 : partie en cours
        private int gameStatus = default;
        public int GameStatus
        {
            get => gameStatus;
            set
            {
                gameStatus = value;
            }
        }

        public Game()
        {

        }

        public Game(string name, int gameStatus)
        {
            Name = name;
            GameStatus = gameStatus;
        }
    }
}
