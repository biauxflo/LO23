using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace network_test
{
    class Server
    {
        int currentId; //id given to the client
        ConcurrentBag<ClientHandler> clients;
        public Server()
        {
            this.currentId = 0;
            this.clients = new ConcurrentBag<ClientHandler>();
        }

        public void run(string ip, int port)
        {

            //setup server
            TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
            TcpClient client = default;
            listener.Start();
            Console.WriteLine("Listening...");

            //handle stop server
            Console.CancelKeyPress += delegate {
                Console.WriteLine("Quit");
                listener.Stop();
            };

            //listen to new clients
            while (true)
            {
                client = listener.AcceptTcpClient();
                //start new Thread
                    clients.Add(new ClientHandler(client, this.currentId++, this));

            }
        }

        public void broadcast(Message msg)
        {
            foreach (var client in this.clients)
            {
                client.sendMessage(msg);
            }
        }
    }

    class ClientHandler
    {
        TcpClient client;
        int id;
        Server server;
        public ClientHandler(TcpClient client, int id, Server server)
        {
            this.id = id;
            this.client = client;
            this.server = server;
            //this.server = server;
            Console.WriteLine("Client " + id + " connected");
            Thread thread = new Thread(handling);
            thread.Start();
        }

        private void handling()
        {
            while (true)
            {
                try
                {
                    //read message
                    Message msg = this.receiveMessage();
                    msg.sender = "Client " + id;
                    msg.print();
                    this.server.broadcast(msg);
                }
                catch (Exception)
                {
                    Console.WriteLine("Client " + this.id + " disconnected");
                    break;
                }
            }
        }

        private Message receiveMessage()
        {
            NetworkStream nwStream = this.client.GetStream();
            byte[] buffer = new byte[this.client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(buffer, 0, this.client.ReceiveBufferSize);
            Message msg = JsonConvert.DeserializeObject<Message>(Encoding.ASCII.GetString(buffer, 0, bytesRead));
            return msg;
        }

        public void sendMessage(Message msg)
        {
            try
            {
                NetworkStream nwStream = this.client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(JsonConvert.SerializeObject(msg));
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
