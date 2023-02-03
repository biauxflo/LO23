using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace network_test
{
    class Message
    {
        public string content;
        public string sender;
        public DateTime date;

        public Message(string content = "", string sender = "")
        {
            this.content = content;
            this.sender = sender;
            this.date = DateTime.Now;
        }

        public void print()
        {
            Console.WriteLine("[" + this.date.ToString() + "] " + sender + " : " + content);
        }
    }
}
