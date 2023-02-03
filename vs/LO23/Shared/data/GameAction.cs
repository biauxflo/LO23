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
		check,
		exchangeCards,
		becomingSpectator
    }

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
