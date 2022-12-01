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
		public string Name { get;set; }
	}
}
