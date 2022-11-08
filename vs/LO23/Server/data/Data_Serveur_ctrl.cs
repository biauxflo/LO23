using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Data;

namespace Server.Data
{
    public class Data_Serveur_ctrl
    {
        private Comm_calls_Data_Server_Impl implInterfaceForComm;

        public Data_Serveur_ctrl()
        {
            implInterfaceForComm = new Comm_calls_Data_Server_Impl();
        }

        public Comm_calls_Data_Server_Impl getImplInterfaceForComm()
        {
            return implInterfaceForComm;
        }
        
        public static void Main(string[] args)
        {
            Data_Serveur_ctrl data = new Data_Serveur_ctrl();
            Comm_calls_Data_Server_Impl comm2 = data.getImplInterfaceForComm();
            User user = new User("KING8", 8);
            User user2 = new User("KING9", 9);
            User user3 = new User("KING10", 4);
            comm2.registerUser(user);
            comm2.registerUser(user2);
            comm2.registerUser(user3);
            comm2.displayUsers();

            
            Console.WriteLine("*************************\n");
            comm2.removeUser(4);
            comm2.displayUsers();
        }
    }
}