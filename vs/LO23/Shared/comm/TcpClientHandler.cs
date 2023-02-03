using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.comm
{
	/// <summary>
	/// Handles a connection between two distant TCP clients.
	/// </summary>
	/// <typeparam name="SendType">Type of messages to send</typeparam>
	/// <typeparam name="ReceiveType">Type of messages to receive</typeparam>
	public class TcpClientHandler<SendType, ReceiveType>
	{
		readonly TcpClient client;
		readonly Thread thread;
		readonly Action<ReceiveType> handler;
		readonly string debugId;
		public TcpClientHandler(TcpClient client, Action<ReceiveType> handler, string debugId=null)
		{
			this.client = client;
			this.handler = handler;

			this.thread = new Thread(this.Listener);
			this.thread.Start();
			this.debugId = debugId;	
		}

		/// <summary>
		/// Starts to listen to all messages and calls the handler when receiving messages.
		/// </summary>
		private void Listener()
		{
			try
			{
				JsonSerializer serializer = new JsonSerializer
				{
					TypeNameHandling = TypeNameHandling.All,
					ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
					ContractResolver = new PrivateSetterContractResolver()
				};
				NetworkStream nwStream = this.client.GetStream();
				byte[] buffer = new byte[this.client.ReceiveBufferSize];

				while(nwStream.CanRead)
				{
					StringBuilder sb = new StringBuilder();
					int bytesRead = 0;

					do
					{
						bytesRead = nwStream.Read(buffer, 0, this.client.ReceiveBufferSize);
						_ = sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
					} while(nwStream.DataAvailable);


					JsonTextReader reader = new JsonTextReader(new StringReader(sb.ToString())){
						SupportMultipleContent = true
					};

					while(reader.Read())
					{
						ReceiveType msg = serializer.Deserialize<ReceiveType>(reader);

						Console.WriteLine(this.debugId == null ?
							string.Format("-> (in) {0}", msg.GetType()):
							string.Format("-> (in from {1}) {0}", msg.GetType(), this.debugId)
						);
						this.handler(msg);
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				//TODO : handle exception
			}
		}

		/// <summary>
		/// Sends a message to the other TCP client.
		/// </summary>
		/// <param name="msg">Message to send</param>
		public void Send(SendType msg)
		{
			try
			{

				string data = JsonConvert.SerializeObject(
					msg, Formatting.None,
					new JsonSerializerSettings
					{
						TypeNameHandling = TypeNameHandling.All
					}
				);

				NetworkStream nwStream = this.client.GetStream();
				byte[] bytesToSend = Encoding.UTF8.GetBytes(data);
				nwStream.Write(bytesToSend, 0, bytesToSend.Length);

				Console.WriteLine(this.debugId == null ?
					string.Format("<- (out) {0} [{1}]", msg.GetType(), bytesToSend.Length) :
					string.Format("<- (out to {2}) {0} [{1}]", msg.GetType(), bytesToSend.Length, this.debugId)
				);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				//TODO : handle exception
			}
		}

		// TODO : clean stop or destroy
	}
}
