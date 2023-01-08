using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared.data
{
    public enum PlayerRole
    {
        smallBlind,
        bigBlind,
        nothing,
    }

	// TODO : Classes observables ? Methode "ObservableObject" pour réutiliser l'interface INotifyPropertyChanged
	public class Player : LightUser
    {
		//public event PropertyChangedEventHandler PropertyChanged;

		public string role { get; set; }
        public bool isFolded { get; set; }
        public int tokens { get; set; }
        public int tokensBet { get; set; }
        public List<Card> hand { get; set; } //Doit referencer les objets cartes contenus dans l'objet Deck

		public int score
		{
			get; set;
		}
		
		/*
		public List<string> Card
		{
			get; set; 
		}*/

		/*
		public List<string> Card
		{
			get => cardImage;
			set
			{
				cardImage = value;
				//OnPropertyChanged(nameof(Card));
			}
		}*/

		/// <summary>
		/// créer la méthode OnPropertyChanges pour créer un événement.
		/// Le nom du membre appelant sera utilisé comme paramètre
		/// </summary>
		/*
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
		}
		/*
				public Player(int tokens)
				{
					this.role = PlayerRole.nothing.ToString();
					this.isFolded = false;
					this.tokens = tokens;
					this.tokensBet = 0;
					this.hand = new List<Card>();
				}
		*/
		public Player() 
		{
		}
		public Player(LightUser lu, int tokens) : base(lu)
		{
			this.role = PlayerRole.nothing.ToString();
			this.isFolded = false;
			this.tokensBet = 0;
			this.hand = new List<Card>();
			//this.Card = new List<string>();
			this.tokens = tokens;
		}

	/*	public Player(Guid id, string username, string image)
		{
			this.id = id;
			this.username = username;
			this.image = image;
			this.role = PlayerRole.nothing.ToString();
			this.isFolded = false;
			this.tokens = tokens;
			this.tokensBet = 0;
			this.hand = new List<Card>();

			this.tokens = 1000;
		}
	*/
		public void removeCardFromHand(Card card)
		{   
			int remove = 0;//index of the card to remove in player's hand
			bool flag =false;

			int i = 0;
			while(!flag && i < this.hand.Count)
			{   
				if(this.CompareCard(this.hand[i], card) == true)
				{
					flag = true;
					remove = i;
				}

				i++;				
			}

			if(flag)
			{
				this.hand.RemoveAt(remove);
			}
			else
			{
				throw new Exception();
			}
		}


		public void removeAllCards()
		{
			while(hand.Count > 0)
			{
				this.removeCardFromHand(hand[0])
			}
		}
		public void AddCardToHand(Card card)
		{

			if(this.hand.Count < 5)
			{
				this.hand.Add(card);
			}

		}

		public bool CompareCard(Card card1, Card card2)
		{
			return (card1.color == card2.color) && (card1.index == card2.index);
		}

		public void decrementTokens(int value)
		{
			tokens -= value;
		}

		public void incrementTokensBet(int value)
		{
			tokensBet += value;
		}
		
		public bool reveal()// need to precise how we know how to reveal or not the cards
		{
			return true;
		}
		public void resetPlayerForNextRound()
		{
			this.score = 0;
			this.tokensBet = 0;
			this.isFolded = false;
			this.removeAllCards();
		}
	}
}
