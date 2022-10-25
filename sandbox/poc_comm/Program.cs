using System;

namespace network_test
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 10000;
            string choice;
            do
            {
                Console.WriteLine("s : server\nc : client\n");
                choice = Console.ReadLine();
            } while(choice != "c" && choice != "s");

            if(choice == "s")
            {
                Server server = new Server();
                server.run(ip, port);
            }
            else
            {
                Client client = new Client();
                client.run(ip, port);
            }
        }
    }
}
