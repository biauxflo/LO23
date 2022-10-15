using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace network_test
{
    class Client
    {
        TcpClient clientSocket;

        public Client()
        {
            //create a TCP Client
            this.clientSocket = new TcpClient();
        }

        public void run(string ip, int port)
        {
            //try to connect
            do
            {
                try
                {
                    this.clientSocket.Connect(ip, port);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (!this.clientSocket.Connected);

            //listen to server
            Thread tcpListenerThread = new Thread(receiveMessage);
            tcpListenerThread.Start();

            //send message to the server
            string text;
            do
            {
                text = Console.ReadLine();
                try
                {
                    sendMessage(text);
                }catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    text = "/quit";
                }
            } while (text != "/quit");

            //quit
            //Console.ReadLine();
            tcpListenerThread.Abort();
            this.clientSocket.Close();
        }

        private void sendMessage(string text)
        {
            Message msg = new Message(text, "client");
            NetworkStream nwStream = this.clientSocket.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(JsonConvert.SerializeObject(msg));
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private void receiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] bytesToRead = new byte[this.clientSocket.ReceiveBufferSize];
                    NetworkStream nwStream = this.clientSocket.GetStream();
                    int bytesRead = nwStream.Read(bytesToRead, 0, this.clientSocket.ReceiveBufferSize);
                    Message resp = JsonConvert.DeserializeObject<Message>(Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                    resp.print();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}

