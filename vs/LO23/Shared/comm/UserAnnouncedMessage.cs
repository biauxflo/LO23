using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.comm
{
	public class UserAnnouncedMessage : MessageToClient
	{
		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public override void handle()
		{
			//calls interface

			//test code
			Console.WriteLine("message handled");
		}
	}
}
