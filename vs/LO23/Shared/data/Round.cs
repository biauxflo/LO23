using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    public class Round
    {
        public List<Phase> phases { get; set; }

        public Round()
        {
            this.phases = new List<Phase>();
        }
		public void addPhase(Phase phase)
		{
			this.phases.Add(phase);
		}
    }
}
