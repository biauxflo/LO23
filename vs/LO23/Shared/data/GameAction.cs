using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    public enum TypeAction
    {
        call,
        rise,
        allin,
        fold
    }

    public class GameAction
    {
        public int id { get; set; }
        public string typeAction { get; set; }

        public GameAction(int id, string typeAction)
        {
            this.id = id;
            this.typeAction = typeAction;
        }
    }

}
