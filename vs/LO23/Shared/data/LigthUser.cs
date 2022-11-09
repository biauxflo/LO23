using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{

    public class LigthUser

    {
        public int id;
        public string userName;// userName et login c'est la même chose 
        public string image;

        public LigthUser(int idt = 1, string userNamet = "usernametest", string imaget = "imagetest")
        {
            if (idt == 1) { id = idt; }
            else
            {
                // id = Guid.NewGuid(); // ou  Guid.NewGuid().ToString()
                id = 1;
            }
            userName = userNamet;
            image = imaget;
        }
    }
}