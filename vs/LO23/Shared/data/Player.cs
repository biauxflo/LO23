using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared.data
{
	/// <summary>
	/// Enumère les différents rôles
	/// </summary>
    public enum PlayerRole
    {
        smallBlind,
        bigBlind,
        nothing,
    }
	/// <summary>
	/// Classe <c>PLayer</c> Classe modélisant un Player. C'est une classe qui hérite de LightUser
	/// </summary>
	public class Player : LightUser
    {

		public string role { get; set; }
        public bool isFolded { get; set; }
        public int tokens { get; set; }
        public int tokensBet { get; set; }
        public List<Card> hand { get; set; } // Must reference the Cards objects contained in the object Deck

		public int score
		{
			get; set;
		}
		
		public Player() 
		{
		}
		/// <summary>
		/// Constructeur de Player
		/// </summary>
		/// <param name="lu"></param>
		/// <param name="tokens"></param>
		public Player(LightUser lu, int tokens) : base(lu)
		{
			this.role = PlayerRole.nothing.ToString();
			this.isFolded = false;
			this.tokensBet = 0;
			this.hand = new List<Card>();
			//this.Card = new List<string>();
			this.tokens = tokens;
		}
		/// <summary>
		/// Permet de retirer la carte transmise en paramètre de la main du joueur concerné
		/// </summary>
		/// <param name="card"></param>
		/// <exception cref="Exception"></exception>
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

		/// <summary>
		/// Retire toutes les cartes présentes dans la main du joueur
		/// </summary>
		public void removeAllCards()
		{
			while(hand.Count > 0)
			{
				this.removeCardFromHand(hand[0]);
			}
		}
		/// <summary>
		/// Ajoute la carte transmise en paramètre dans la main du joueur
		/// </summary>
		/// <param name="card"></param>
		public void AddCardToHand(Card card)
		{

			if(this.hand.Count < 5)
			{
				this.hand.Add(card);
			}

		}
		/// <summary>
		/// Compare les deux cartes transmises en paramètre
		/// </summary>
		/// <param name="card1"></param>
		/// <param name="card2"></param>
		/// <returns></returns>
		public bool CompareCard(Card card1, Card card2)
		{
			return (card1.color == card2.color) && (card1.index == card2.index);
		}
		/// <summary>
		/// Décrémente les tokens de la valeur entière transmise en paramètre
		/// </summary>
		/// <param name="value"></param>
		public void decrementTokens(int value)
		{
			tokens -= value;
		}
		/// <summary>
		/// Incrémente les tokens de la valeur entière transmise en paramètre
		/// </summary>
		/// <param name="value"></param>
		public void incrementTokensBet(int value)
		{
			tokensBet += value;
		}
		/// <summary>
		/// Révèle les cartes du joueur
		/// </summary>
		/// <returns></returns>
		public bool reveal()
		{
			return true;
		}
		/// <summary>
		/// Permet de réinitialiser le joueur avec les valeurs par défaut, afin de jouer le prochain tour
		/// </summary>
		public void resetPlayerForNextRound()
		{
			this.score = 0;
			this.tokensBet = 0;
			this.isFolded = false;
			this.removeAllCards();
		}
	}
}
