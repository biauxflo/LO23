using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    enum TypePhase : ushort
    {
        mise1 = 1,
        echange = 2,
        mise2 = 2,
        reveal = 4
    }

    public class Phase
    {
        public List<GameAction> actions { set; get; }

        public Phase()
        {
            this.actions = new List<GameAction>();
        }
    }

}