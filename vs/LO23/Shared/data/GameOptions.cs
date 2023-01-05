using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
	public class GameOptions
	{
		public int StartingTokens
		{
			set; get;
		}
		public bool CanSpecJoin
		{
			set; get;
		}
		public bool CanSpecChat
		{
			set; get;
		}
		public int NbRoundMax
		{
			set; get;
		}
		public int NbPlayersMax
		{
			set; get;
		}
		public int StartingBigBlind
		{
			set; get;
		}
		public int BigBlindValue
		{
			set; get;
		}
		public string Name {
			set; get;
		}
		public int NbPlayersMin {
			set; get;
		}

		public GameOptions(
			string name,
			int startingTokens,
			bool canSpecJoin,
			bool canSpecChat,
			int nbRoundMax,
			int nbPlayersMax,
			int nbPlayersMin,
			int startingBigBlind,
			int bigBlindValue
		)
		{
			this.Name = name;
			this.StartingTokens = startingTokens;
			this.CanSpecJoin = canSpecJoin;
			this.CanSpecChat = canSpecChat;
			this.NbRoundMax = nbRoundMax;
			this.NbPlayersMax = nbPlayersMax;
			this.NbPlayersMin = nbPlayersMin;
			this.StartingBigBlind = startingBigBlind;
			this.BigBlindValue = bigBlindValue;
		}
	}
}
