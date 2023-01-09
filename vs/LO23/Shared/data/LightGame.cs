using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.constants;

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
        public Guid id { get; set; }
        public GameStatus status { get; set; }
        public int indexRound { get; set; }
        public List<Player> players { get; set; }
        public List<LightUser> spectators { get; set; }
        public List<LightUser> lobby { get; set; }
		public GameOptions gameOptions {get; private set;} //Options de la game à sa création

		protected LightGame(){}

		public LightGame(Guid id, GameOptions options)
        {
            this.id = id;
			this.gameOptions = options;
            this.status = GameStatus.lobby;
            this.indexRound = 0;
            this.players = new List<Player>();
            this.spectators = new List<LightUser>();
            this.lobby = new List<LightUser>();
        }
    }
}
