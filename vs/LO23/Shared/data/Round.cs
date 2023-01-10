using System;
using System.Collections.Generic;

namespace Shared.data
{
	/// <summary>
	/// Classe <c>Round</c> Classe modélisant un Round
	/// </summary>
	public class Round
    {
        public List<Phase> phases { get; set; }
		/// <summary>
		/// Constructeur de Round
		/// </summary>
        public Round()
        {
            this.phases = new List<Phase>();
        }
		/// <summary>
		/// Ajoute une nouvelle phase
		/// </summary>
		/// <param name="phase"></param>
		public void addPhase(Phase phase)
		{
			this.phases.Add(phase);
		}
    }
}
