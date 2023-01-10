using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
	/// <summary>
	/// TypeAction correspond à toutes les actions qu'un joueur peut effectuer lors d'un tour
	/// </summary>
    public enum TypeAction
    {
        call,
        rise,
        allin,
        fold, 
		check,
		exchangeCards
    }
	/// <summary>
	/// Classe <c>GameAction</c> Classe modélisant une GameAction
	/// </summary>
	public class GameAction
    {
		public GameAction()
		{
		}

		public Guid id
		{
			get; set;
		}

		public Guid gameId
		{
			get; set;
		}

		public Player player
		{
			get; set;
		}
		public int value
		{
			get; set;
		}
		public List<Card> listOfCards
		{
			get; set;
		}

        public TypeAction typeAction { get; set; }
		/// <summary>
		/// Constructuer d'une GameAction en fonction des attributs lui étant transmis
		/// </summary>
		/// <param name="id"></param>
		/// <param name="gameId"></param>
		/// <param name="player"></param>
		/// <param name="value"></param>
		/// <param name="listOfCards"></param>
		/// <param name="typeAction"></param>
		public GameAction(Guid id, Guid gameId, Player player, int value,  List<Card> listOfCards, TypeAction typeAction)
		{
			this.id = id;
			this.gameId = gameId;
			this.player = player;
			this.value = value;
			this.listOfCards = listOfCards;
			this.typeAction = typeAction;
		}
	}

}
