using Shared.interfaces;

namespace Shared.comm
{
	public abstract class MessageToClient
	{
		/// <summary>
		/// Operates the process of the message.
		/// </summary>
		public abstract void Handle(ICommToData commToData);
	}
}