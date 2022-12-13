using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.comm.messages;
using Shared.interfaces;

namespace Shared.comm
{
	public class UnregisterUserReturn : MessageToClient
	{
		public override void Handle(ICommToDataClient commToData)
		{
			commToData.setLoggedOut();
			throw new NotImplementedException();
		}
	}
}
