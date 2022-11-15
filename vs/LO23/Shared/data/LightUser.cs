using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{

    public class LightUser

    {
        public int id { get; set; }
        public string userName;// userName et login c'est la même chose 
        public string image;

        public LightUser(int idt = 1, string userNamet = "usernametest", string imaget = "imagetest")
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