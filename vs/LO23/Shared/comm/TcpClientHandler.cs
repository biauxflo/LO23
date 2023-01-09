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
	/// Gère une connexion TCP entre deux machines.
	/// </summary>
	/// <typeparam name="SendType">Type de message à envoyer (voir MessageToClient et MessageToServer)</typeparam>
	/// <typeparam name="ReceiveType">Type de messages à recevoir (voir MessageToClient et MessageToServer)</typeparam>
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
		/// Débute l'écoute et la gestion de messages.
		/// </summary>
		private void Listener()
		{
			try
			{
				//Read message from JSON
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
					//read all data
					do
					{
						bytesRead = nwStream.Read(buffer, 0, this.client.ReceiveBufferSize);
						_ = sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
					} while(nwStream.DataAvailable);

					JsonTextReader reader = new JsonTextReader(new StringReader(sb.ToString())){
						SupportMultipleContent = true
					};
					//Deserialize the JSON to create message object
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
		/// Envoie un message à l'autre machine.
		/// </summary>
		/// <param name="msg">Message à envoyer</param>
		public void Send(SendType msg)
		{
			try
			{
				//Serialize message into JSON
				string data = JsonConvert.SerializeObject(
					msg, Formatting.None,
					new JsonSerializerSettings
					{
						TypeNameHandling = TypeNameHandling.All
					}
				);
				//Send data to the other computer
				NetworkStream nwStream = this.client.GetStream();
				byte[] bytesToSend = Encoding.UTF8.GetBytes(data);
				nwStream.Write(bytesToSend, 0, bytesToSend.Length);
				//Notify that the message was sent
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
