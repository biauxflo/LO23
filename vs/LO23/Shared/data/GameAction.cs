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
		public Guid id
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
		public GameAction action
		{
			get; set;
		}
		public List<Card> listOfCards
		{
			get; set;
		}

        public TypeAction typeAction { get; set; }

		public GameAction(Guid id, Player player, int value, GameAction action, List<Card> listOfCards, TypeAction typeAction)
		{
			this.id = id;
			this.player = player;
			this.value = value;
			this.action = action;
			this.listOfCards = listOfCards;
			this.typeAction = typeAction;
		}
	}

}
