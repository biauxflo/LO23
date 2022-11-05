using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ihm_main.DTO
{
    class User
    {
        private string login { get; set; }
        private string password { get; set; }

        public User(string log, string pass) {
            login = log;
            password = pass;
        }

        public User() {
            login = string.Empty;
            password = string.Empty;
        }
    }
}
