using System;
using System.Collections.Generic;

namespace Shared.data
{
	/// <summary>
	/// Classe <c>Card</c> Modélise la classe associée à une carte 
	/// </summary>
	public class Card : IComparable<Card>
	{
		public static List<char> colors = new List<char> { 'h', 'd', 'c', 's'}; // Heart <=> 'Coeur', Diamonds <=> 'Carreau', Club <=> 'Trèfle', Spade <=> 'Pique'
		public static List<int> values = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }; // 2,...,10 <=> 2,...,10, 11 <=> Jack <=> 'Valet', 12 <=> Queen <=> 'Dame', 13 <=> King <=> 'Roi', 14 <=> 1 
		public int index { get; set; }
        public char color { get; set; }
        public int value { get; set; }
        public bool isInHand { get; set; }
        public bool isHidden { get; set; }
		/// <summary>
		/// Constructeur permettant de créer une carte, en initialisant tous ses attributs
		/// </summary>
		/// <param name="index"></param>
		/// <param name="color"></param>
		/// <param name="value"></param>
		/// <param name="isInHand"></param>
		/// <param name="isHidden"></param>
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
		/// <summary>
		/// Permet de comparer la carte en cours avec la carte passée en paramètre
		/// </summary>
		/// <param name="otherCard"></param>
		/// <returns></returns>
		public int CompareTo(Card otherCard)
		{
			return this.value - otherCard.value;
		}
	}
}
