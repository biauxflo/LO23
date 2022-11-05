using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ihm_main.DTO;

namespace Client.ihm_main
{
    class ConnectionViewModel
    {
        public User user1 = new User();
        public User user2 = new User("utilisateur", "utilisateur123");
    }
}
