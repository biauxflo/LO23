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
        public int value { get; set; }
		public List<Card> cards
		{
			get; set;
		}

		public GameAction(int id, TypeAction typeAction, int value, List<Card> cards)
		{
			this.id = id;
			this.typeAction = typeAction;
			this.value = value;
			this.cards = cards;
		}
	}

}
