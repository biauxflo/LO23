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
        fold, 
		exchangeCards
    }

    public class GameAction
    {
        public int id { get; set; }
        public TypeAction typeAction { get; set; }

        public GameAction(int id, TypeAction typeAction)
        {
            this.id = id;
            this.typeAction = typeAction;
        }
    }

}
