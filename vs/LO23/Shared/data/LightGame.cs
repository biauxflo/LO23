using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
   public enum GameStatus : ushort
    {
        lobby = 0,
        running = 1,
        paused = 2,
        finished = 3
    }

   public class LightGame
    {
        int id { get; set; }
        GameStatus status { get; set; }
        int indexRound { get; set; }
        Player[] players { get; set; }
        User[] spectators { get; set; }
        User[] lobby { get; set; }

        public LightGame(int id, GameStatus status, int indexRound, Player[] players, User[] spectators, User[] lobby)
        {
            this.id = id;
            this.status = status;
            this.indexRound = indexRound;
            this.players = players;
            this.spectators = spectators;
            this.lobby = lobby;
        }
    }
}
