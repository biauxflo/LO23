using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ihm_main.DTO
{
    class User
    {
        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
            }
        }

        public User(string log, string pass) {
            login = log;
            password = pass;
        }

        public User() {
            login = string.Empty;
            password = string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                throw new Exception("Le type testé n'est pas un utilisateur");
            }
            else
            {
                User user = (User)obj;
                return this.login == user.login && this.password == user.password;
            }
        }
    }
}
