using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    public enum TypePhase
    {
        bet1,
        draw,
        bet2,
        reveal
    }

    public class Phase
    {
		public TypePhase typePhase
		{
			set; get;
		}
		public List<GameAction> actions { set; get; }

		public Phase()
		{
			this.actions = new List<GameAction>();
		}
		public Phase(TypePhase typePhase)
        {
			this.typePhase = typePhase;
            this.actions = new List<GameAction>();
        }
    }

}
