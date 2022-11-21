using System;

namespace Client.ihm_main.DTO
{
    // TODO : Remplacer par la classe de Data
    /// <summary>
    /// Utilisateur de l'application.
    /// </summary>
    internal class User
    {
        /// <summary>
        /// Login de l'utilisateur.
        /// </summary>
        private string login = string.Empty;
        public string Login
        {
            get => login;
            set
            {
                login = value;
            }
        }

        /// <summary>
        /// Mot de passe de l'utilisateur.
        /// </summary>
        private string password = string.Empty;
        public string Password
        {
            get => password;
            set
            {
                password = value;
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="log">Login de l'utilisateur à créer.</param>
        /// <param name="pass">Mot de passe de l'utilisateur à construire.</param>
        public User(string log, string pass)
        {

            login = log;
            password = pass;
        }

        public User()
        {

        }

        public override bool Equals(object obj)
        {

            if(obj.GetType() != GetType())
            {
                throw new Exception("Le type testé n'est pas un utilisateur");
            }
            else
            {
                User user = (User)obj;
                return login == user.login && password == user.password;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
