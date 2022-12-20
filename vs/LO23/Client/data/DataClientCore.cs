using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.interfaces;

namespace Client.data
{
	public class DataClientCore
	{
		private List<LightUser> users = new List<LightUser>(); //List of connected users sent from server
		public List<LightUser> Users
		{
			get => users;
			set
			{
				users = value;
				//interfaceFromMain.UserListUpdated(users); //TODO: Not yet implemented - Waiting for v2
			}
		}

		private List<LightGame> games = new List<LightGame>(); //List of games sent from server
		public List<LightGame> Games
		{
			get => games;
			set
			{
				games = value;
				interfaceFromMain.GameListUpdated(games);
			}
		}

		public User CurrentUser { get; set; } //The current user connected on this client

		public CommToDataClient implInterfaceForComm { get; private set; }
		public Game joinedGame { get; set; }

		public IDataToComm interfaceFromComm { private get; set; }
		public IDataToMain interfaceFromMain { internal get; set; }
		public IHMMainToDataClient implInterfaceForMain { get; private set; }

		public DataClientCore()
        {
            this.implInterfaceForComm = new CommToDataClient(this);
			this.implInterfaceForMain = new IHMMainToDataClient(this);
		}

        public void request_PlayGameToComm(Guid gameId, LightUser lightUser)
        {
			interfaceFromComm.requestPlayGame(gameId, lightUser);
        }

        public void request_displayGameToIHMMain(Game game)
        {
			interfaceFromMain.GameLaunched(game);
        }

		internal void sendCreateNewGame(GameOptions options)
		{
			interfaceFromComm.createNewGame(options);
		}

		internal void ask_announceUser(LightUser user)
		{
			interfaceFromComm.announceUser(user);
		}

        internal void SendConnectionSucceedToMain(LightUser user)
        {
            interfaceFromMain.ConnectionSucceed(user);
        }

        internal void SendConnectionFailedToMain(string error)
        {
            interfaceFromMain.ConnectionFailed(error);
        }

		internal void SendProfileCreatioFailedToMain(string error)
		{
			interfaceFromMain.ProfileCreatioFailed(error);
		}

		internal void SendProfileCreationSucceedToMain()
		{
			interfaceFromMain.ProfileCreationSucceed();
		}
	}
}
