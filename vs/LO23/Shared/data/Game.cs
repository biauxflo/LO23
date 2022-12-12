using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
	public class Game : LightGame
	{
		public List<Round> rounds
		{
			get; set;
		} //Un round est un enchainement de phases de jeu => Il va de la distribution des cartes jusqu'au reveal des cartes et répartition des gains
		public int turn
		{
			set; get;
		} //Un turn correspond au tour de jeu d'un joueur
		public int smallBlind
		{
			set; get;
		}
		public int bingBlind
		{
			set; get;
		}
		public int currentPlayerIndex
		{
			set; get;
		} //Le joueur qui doit jouer actuellement
		public Phase currentPhase
		{
			set; get;
		} //La phase actuelle (phase de mise, de pari, ou de révélation des cartes)
		public int pot
		{
			set; get;
		} //L'ensemble des jetons misés
		public int highestBet
		{
			set; get;
		} //Le nombre de jetons misés par un joueur le plus important 
		public int nbNoRise
		{
			set; get;
		} //Nombre de turn depuis le début de la phase courante qui n'ont pas rise (augmenter le pari minimum nécessaire) => Sert à déduire la fin d'une phase
		public List<ChatMessage> chat
		{
			set; get;
		}
		public int nbPlayers
		{
			get; set;
		}
		public Deck deck
		{
			get; private set;
		} //L'ensemble des cartes dans le jeu, qu'elles soient en main, dans la pioche ou la défausse

		public Game()// nina changed to public to test
		{
		}

		public Game(Guid id, GameOptions options) : base(id, options)
		{
			this.rounds = new List<Round>();
			this.turn = 0;
			this.smallBlind = options.StartingBigBlind / 2;
			this.bingBlind = options.StartingBigBlind;
			this.currentPlayerIndex = 0;
			this.currentPhase = new Phase();
			this.pot = 0;
			this.highestBet = 0;
			this.nbNoRise = 0;
			this.chat = new List<ChatMessage>();
			this.deck = new Deck();
		}

		public static LightGame ToLightGame(Game game)
		{
			return new LightGame(game.id, game.gameOptions);
		}

		public int goToNextPlayer()
		{
			int nextPlayerIndex = (this.currentPlayerIndex + 1) % this.players.Count;
			this.currentPlayerIndex = nextPlayerIndex;

			return nextPlayerIndex;
		}

		//Method that distribute cards to all Players
		public void distributeCards()
		{
			List<Card> cardsAvailable = this.deck.cards.Where(c => c.isInHand == false).ToList();

			for(int i = 0; i < 5; i++)
			{
				foreach(Player player in this.players)
				{
					Card card = cardsAvailable[0];
					card.isInHand = true;
					player.hand.Add(card);
					cardsAvailable.RemoveAt(0);
				}
			}
		}

		public void runGame()
		{

			/*
			 * Game in lobby status
			 * 
			 * //Waiting for players ot start game 
			 * while nbPlayers (or players.size()) < nbPlayersMax  
			 *		if nbPlayers > nbPlayersMin && launchGameTimer not started
			 *			start launchGameTimer
			 *		
			 *		if launchGameTimer > 1minute
			 *			break;
			 *		else
			 *			wait
			 *	
			 * //Starting game
			 * 
			 * initialize Game //nothing may happen, maybe just check everythign is alright and do nothing else, if the rest has already been done when players join for example
			 * 
			 * 
			 * while nbRounds < roundMax && nbPlayers > 1:
				 * currentRound = creation of a Round (corresponds to creating a round and initialising it) //shuffle deck, make players pay blind, distribute cards ->send game changes to players
				 * put game in running status
				 * currentRound.enterPhaseBet() //Here in the object Round is all the logic to enter in a beting phase, for example creates an object phase and add it in Round class's phases array. Declare this phase as the current one for
				 * 
				 * indexCurrentPlayer = 0;
				 * 
				 * while nbNoRise < nbPlayers // As long as not everyone did not rise, that means that not everyone has bet the same amount, menaing you can not pass to the next phase
				 *		currentRound.currentPhase.RequestAndExecuteActionFromPlayer(players.getAt(indexCurrentPlayer)) //could be implmented differently
				 *		indexCurrentPlayer +1 % nbPlayers
				 *		
				 *  currentRound.exitPhaseBet()  //Here in the object Round is all the logic to exit from a beting phase, could be nothing
				 *	
				 *	currentRound.enterPhaseExchange()
				 *	for each player still active:
				 *		currentRound.currentPhase.RequestAndExecuteActionFromPlayer(player)) //could be implemented differently
				 *		
				 *	currentRound.exitPhaseExchange()
				 *	
				 *	currentRound.enterPhaseReveal() 
				 *	currentRound.exitPhaseReveal() 
				 *	
				 *	finishRound()
				 *
			 * finishGame()
			 * 
			 * 
			 * 
			 * 
			 * 
			 * 
			 * 
			 * 
			 */
		}

		public void exchangeCards(Player player, List<Card> listOfCards)
		{
			int nb = listOfCards.Count;

			// To remove once we have a proper constructor for game
			this.deck = new Deck();

			List<Card> listOfNewCards = new List<Card>();
			for(int i = 0; i < nb; i++)
			{
				player.removeCardFromHand(listOfCards[i]); // we take back the cards from the player
			}
			this.deck.giveBackCards(listOfCards); //give back to the deck the cards

			for(int i = 0; i < nb; i++)
			{

				Card cardTmp = this.deck.giveNewCard(); // what's the next card i can give
				listOfNewCards.Add(cardTmp); //add to the list of new cards
				player.AddCardToHand(cardTmp); // add card to player's hand
				

			}
		}

		public void printHand(List<Card> hand) // Sylvain's function I needed -> ATTENTION watch out ctrl c ctrl v
		{
			foreach(Card card in hand)
			{
				Console.WriteLine(card.color + " : " + card.value);
			}
		}
	}		
}
