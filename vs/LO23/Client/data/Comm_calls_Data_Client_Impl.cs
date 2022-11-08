using System;
using System.Collections.Generic;
using System.Data;


namespace Client.Data
{
    public class Username
    {
        private string name;

        public Username(string name)
        {
            this.name = name;
        }

        public string getUsername()
        {
            return name;
        }
    }
    
    public class GameInfo
    {
        private int id;
        private int nbPlayers;
        private int nbMaxPlayers;
        
        public GameInfo(int id, int nbPlayers, int nbMaxPlayers)
        {
        this.id = id;
        this.nbPlayers = nbPlayers;
        this.nbMaxPlayers = nbMaxPlayers;
        }

        public int getId()
        {
            return id;
        }

        public int getNbPlayers()
        {
            return nbPlayers;
        }

        public int getNbMaxPlayers()
        {
            return nbMaxPlayers;
        }
    }
    
    public class Comm_calls_Data_Client_impl : Shared.interfaces.Interface_Comm_calls_Data_Client
    {
        private Username[] users;
        private GameInfo[] games;
        public Comm_calls_Data_Client_impl(){}

        public void setGamesAndUsers(Username[] users, GameInfo[] games)
        {
	}
}

