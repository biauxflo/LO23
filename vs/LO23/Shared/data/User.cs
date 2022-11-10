using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.data {

    public class User : LigthUser
    {
        public string password;

        public bool status;
        public string firstname;
        public int age;



        public User(int idt ,string imaget , string userNamet, string passwordt, bool statust, string firstnamet, int aget)
        {
            id = idt;
            image = imaget;

            userName = userNamet;
            password = passwordt;
            status = statust;
            firstname = firstnamet;
            age = aget;
        }

    }
}