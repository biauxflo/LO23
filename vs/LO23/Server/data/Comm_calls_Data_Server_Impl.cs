using System;
using System.Collections.Generic;
using System.Data;


namespace Server.Data
{
    public class User
    {
        private string username;
        private int idJoueur;

        public User(string username,int idJoueur)
        {
            this.username = username;
            this.idJoueur = idJoueur;
            
        }
        
        public string DisplayInfoPlayer()
        {
            return "Username : " + username + " / " + "IdJoueur : " + idJoueur;
        }

        public int getIdJoueur()
        {
            return idJoueur;
        }
    }

    public class Game
    {
        private string idGame;
        private int nbPlayer;
        private int nbPlayerMax;
        
        public Game(string idGame, int nbPlayer, int nbPlayerMax)
        {
            this.idGame = idGame;
            this.nbPlayer = nbPlayer;
            this.nbPlayerMax = nbPlayerMax;
        }

        public string DisplayInfoGame()
        {
            return "IdGame : " + idGame + " / " + "NbPlayer : " + nbPlayer + " / " + "NbPlayerMax : " + nbPlayerMax;
        }
    }

    
    public class Comm_calls_Data_Server_Impl : Shared.interfaces.Interface_Comm_calls_Data_Server
    {
        private User user;
        private static List<User> users = new List<User>();
        private static List<Game> games = new List<Game>();

        public Comm_calls_Data_Server_Impl()
        {

        }

        public Comm_calls_Data_Server_Impl(string username, int idJoueur)
        {
            this.user = new User(username, idJoueur);
            registerUser(user);
        }
        
        public User getUser()
        {
            return this.user;
       
        }

        public List<User> registerUser(User user)
        {
            users.Add(user);
            return users;
        }

        public void displayUsers()
        {
            foreach (User user in users)
            {
                Console.WriteLine(user.DisplayInfoPlayer());
            }
        }

        public void removeUser(int idJoueur)
        {
            foreach(User user in users)
            {
                if(user.getIdJoueur() == idJoueur)
                {
                    users.Remove(user);
                    break;
                }
            }
        
    }

        //On va juste retourner la liste des games stockés sur le serveur
        public List<Game> getGames()
        {
            return games;
        }

        public Comm_calls_Data_Server_Impl getCommCallsDataServerImpl()
        {
            return this;
        }

    }

}

