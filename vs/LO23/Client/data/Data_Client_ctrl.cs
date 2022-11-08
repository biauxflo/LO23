using System;

namespace Client.Data
{
    public class Data_Client
    {
        private Comm_calls_Data_Client_impl implInterfaceForComm;

        public Data_Client()
        {
            this.implInterfaceForComm = new Comm_calls_Data_Client_impl();
        }
        
        public Comm_calls_Data_Client_impl getImplInterfaceForComm()
        {
            return implInterfaceForComm;
        }
        
        public static void Main(string[] args)
        {
            Data_Client data = new Data_Client();
            Comm_calls_Data_Client_impl comm = data.getImplInterfaceForComm();
            Username[] users = new Username[2];
            users[0] = new Username("KING1");
            users[1] = new Username("KING2");
            GameInfo[] games = new GameInfo[2];
            games[0] = new GameInfo(1, 2, 4);
            games[1] = new GameInfo(2, 3, 5);
            comm.setGamesAndUsers(users, games);
            comm.displayUsers();
            comm.displayGames();
        }
    }
}
