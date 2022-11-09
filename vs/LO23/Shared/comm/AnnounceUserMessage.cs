using System;

namespace Shared.comm
{
	public class AnnounceUserMessage : MessageToServer
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
