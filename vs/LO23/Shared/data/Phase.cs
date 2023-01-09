using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
	/// <summary>
	/// Permet d'énumérer les phases possibles dans une partie
	/// </summary>
    public enum TypePhase
    {
        bet1,
        draw,
        bet2,
        reveal
    }
	/// <summary>
	/// Classe <c>Phase</c> Classe modélisant une Phase de jeu
	/// </summary>
	public class Phase
    {
		public TypePhase typePhase
		{
			set; get;
		}
		public List<GameAction> actions { set; get; }
		/// <summary>
		/// Constructeur de phase
		/// </summary>
		public Phase()
		{
			this.actions = new List<GameAction>();
		}
		/// <summary>
		/// Constructeur de phase
		/// </summary>
		/// <param name="typePhase"></param>
		public Phase(TypePhase typePhase)
        {
			this.typePhase = typePhase;
            this.actions = new List<GameAction>();
        }
    }

}
