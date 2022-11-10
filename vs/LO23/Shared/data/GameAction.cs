using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    enum TypeAction : ushort
    {
        call = 1,
        rise = 2,
        allin = 3,
        fold = 4
    }


    class GameAction
    {

        private int id { get; set; }
        private TypeAction typeAction { get; set; }

        public GameAction(int id, TypeAction typeAction)
        {
            this.id = id;
            this.typeAction = typeAction;

        }
    }

}