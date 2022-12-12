﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shared.data
{
    public enum PlayerRole
    {
        smallBlind,
        bigBlind,
        nothing,
    }

	// TODO : Classes observables ? Methode "ObservableObject" pour réutiliser l'interface INotifyPropertyChanged
	public class Player : INotifyPropertyChanged
    {
        public string role { get; set; }
        public bool isFolded { get; set; }
        public int tokens { get; set; }
        public int tokensBet { get; set; }
        public List<Card> hand { get; set; }

		/** Test rcisnero
		 * Temporary string list to add images paths
		 */
		private List<string> cardImage;
		public event PropertyChangedEventHandler PropertyChanged;

		public List<string> Card
		{
			get => cardImage;
			set
			{
				cardImage = value;
				OnPropertyChanged(nameof(Card));
			}
		}
		/** Fin test rcisnero **/

		public Player(int tokens)
        {
            this.role = PlayerRole.nothing.ToString();
            this.isFolded = false;
            this.tokens = tokens;
            this.tokensBet = 0;
            this.hand = new List<Card>();

			this.cardImage = new List<string>(); // Test, list init
        }

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/** Test rcisnero
		 * Converts one card from the hand to path
		 * - if we keep the cardImage attribute, we delete this method. 
		 */
		/*public string getCardToString(int index)
		{
			return "/Client;component/ihm-game/Views/images/" + this.hand[index].value + "_" + this.hand[index].color + ".png";
		}*/
    }
}
