using System;
using System.Collections.Generic;

namespace Shared.data
{
    public class Deck
    {
        public int index { get; set; }
        public List<Card> cards { get; set; }

        public Deck()
		{
			generate52Cards();
		}

		private void generate52Cards() //Can also just be the reset() function
		{
			//TODO
			//Implemente
		}
    }
}
