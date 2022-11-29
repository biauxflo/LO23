using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
   public enum GameStatus
    {
        lobby,
        running,
        paused,
        finished,
    }

   public class LightGame
    {
        public int id { get; set; }
        public string status { get; set; }
        public int indexRound { get; set; }
        public List<Player> players { get; set; }
        public List<LightUser> spectators { get; set; }
        public List<LightUser> lobby { get; set; }

        // Default constructor
        public LightGame()
        {
        }

        public LightGame(int id)
        {
            this.id = id;
            this.status = GameStatus.lobby.ToString();
            this.indexRound = 0;
            this.players = new List<Player>();
            this.spectators = new List<LightUser>();
            this.lobby = new List<LightUser>();
        }

        public void addUser(LightUser user)
        {
            lobby.Add(user);
        }
    }
}
