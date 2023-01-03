using System;
using System.Collections.Generic;

namespace Shared.data
{
    public class Card
    {
		public static List<char> colors = new List<char> { 'h', 'd', 'c', 's'}; //Heart = coeur, Diamonds = carreau, Club = trèfle, Spade = pique
		public static List<int> values = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }; 
		//2=2, 3=3, 4=4, 5=5, 6=6, 7=7, 8=8, 9=9, 10=10, 11=Jack=Valet, 12=Queen=Dame, 13=King=Roi, 14=1 
        public int index { get; set; }
        public char color { get; set; }
        public int value { get; set; }
        public bool isInHand { get; set; }
        public bool isHidden { get; set; }

        public Card(
            int index,
            char color,
            int value,
            bool isInHand,
            bool isHidden
        ) {
            this.index = index;
            this.color = color;
            this.value = value;
            this.isInHand = isInHand;
            this.isHidden = isHidden;
        }
    }
}
