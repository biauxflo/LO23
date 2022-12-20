using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;
using Shared.interfaces;

namespace Shared.comm
{
	public class GetProfileReturn : MessageToClient
	{
		public LightUser user;

		public GetProfileReturn(LightUser user)
		{
			this.user = user;
		}

		public override void Handle(ICommToDataClient commToData)
		{
			commToData.getProfileReturn(this.user);
		}
	}
}
