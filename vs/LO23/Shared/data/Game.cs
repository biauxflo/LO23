using Shared.constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Shared.data
{
	public class Game : LightGame
	{

		private int nbPlayersStillPlaying;
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
		public int bigBlind
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
			this.bigBlind = options.StartingBigBlind;
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


		public void initGame()
		{
			foreach(LightUser lu in lobby) //Creation of players - tokens distribution
			{
				players.Add(new Player(lu, gameOptions.StartingTokens));
			}

			initRound();
		}


		public void addUser(LightUser user)
		{
			lobby.Add(user);

			if(lobby.Count >= Constants.NB_PLAYERS_MIN)
			{
				initGame(); //Creating players, giving cards and tokens, starting game
			}
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
		{/*
			int nbUser = this.lobby.Count;
			if (nbUser < nbMinPlayers)
			{
				System.Threading.Thread.Sleep(5000);
			}
			/*
			 * Timer timer = new System.Timers.Timer(120000);
			timer.Start();
			while()
			
			System.Threading.Thread.Sleep(2000);
			if(nbUser > nbMaxPlayers)
			{
				Console.WriteLine(" trop de user dans le lobby, veuillez quitter");
			}
			for(int i = 0; i < nbUser; i++)
			{	Player p =new  Player(this.lobby[i].id,this.lobby[i].username,this.lobby[i].image);
				this.players.Add(p);
			}
			//this.initializeGame();
			int nbPlayers = this.players.Count();
			int nbRounds = 1;//remplacer avec game options
			int roundMax = 4;
			while(nbRounds < roundMax && nbPlayers > 2)
			{   Phase p1 = new Phase(TypePhase.bet1);
			{   Phase p2 = new Phase(TypePhase.draw);
				Round r1 = new Round();
				r1.addPhase(p1);
				this.rounds.Add(r1);

				//this.distributeCards(this.players);
				this.paySmallBlind(players[currentPlayerIndex]);
				currentPlayerIndex++;
				this.payBigBlind(players[currentPlayerIndex+1]);
				currentPlayerIndex++;

				IDataToComm.notifyGameChange()
				
				players[currentPlayerIndex]).chooseAction();

				//switch()

			}

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

		private void payBigBlind(Player player)
		{
			player.tokens -= this.bigBlind;
			this.pot += this.bigBlind;
		}

		private void paySmallBlind(Player player)
		{
			player.tokens -= this.smallBlind;
			this.pot += this.smallBlind;

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

		public void handleGameAction(GameAction action)
		{
			// Check player existence in the game
			bool isPlayerInTheGame = this.players.Contains(action.player);
			if (!isPlayerInTheGame)
			{
				Console.WriteLine("Player is not in the game");
			}

			switch(action.typeAction)
			{
				case TypeAction.call:
					this.call(action.player, action.value);
					break;

				case TypeAction.rise:
					this.rise(action.player, action.value);
					break;
				case TypeAction.allin:
					this.allIn(action.player, action.value);

					break;
				case TypeAction.fold:
					fold(action.player);

					break;
				case TypeAction.exchangeCards:
					this.exchangeCards(action.player, action.listOfCards);
					break;
			}

			if(nbNoRise >= nbPlayersStillPlaying)
				if(currentPhase.typePhase == TypePhase.reveal)
					initRound();
				else
					goToNextPhase();
			else
				goToNextPlayer();

		}

		private void goToNextPhase()
		{
			Phase newPhase = new Phase(currentPhase.typePhase++); //Hopefully it gives the next phase
			newCurrentPhase(newPhase);
		}

		private void newCurrentPhase(Phase newPhase)
		{
			currentPhase = newPhase;
			rounds[rounds.Count - 1].addPhase(newPhase); //Adding newPhase to current round (which is the last one in the rounds list)
		}

		private void rise(Player player, int value)
		{
			if(player.tokens < value)
			{
				Console.WriteLine("Player doesn't have enough tokens to bet that amount.");
			}
			else
			{
				player.decrementTokens(value, player.tokens);
				player.incrementTokens(value, player.tokensBet);
				this.pot += value;
				this.highestBet = value;
				nbNoRise = 0; //reset nb turn of no rising event
			}
		}

		private void fold(Player player)
		{
			player.isFolded = true;
			for(int card = 0; card < player.hand.Count; card++)
			{
				player.removeCardFromHand(player.hand[card]);
			}

			nbPlayersStillPlaying--;
		}

		private void allIn(Player player, int value)
		{
			value = player.tokens;
			player.incrementTokens(value, player.tokensBet);
			player.decrementTokens(0, player.tokens);
			this.pot += value;
			this.highestBet = value;
			nbNoRise = 0; //reset nb turn of no rising event
		}

		private void call(Player player, int value)
		{
			value = this.highestBet;
			if(player.tokens < value)
			{
				Console.WriteLine("Player doesn't have enough tokens to bet that amount.");
			}
			else
			{
				player.decrementTokens(value, player.tokens);
				player.incrementTokens(value, player.tokensBet);
				this.pot += value;
			}

			nbNoRise++;
		}
		
		
	
		

		public void initRound()
		{
			nbPlayersStillPlaying = 0;
			foreach(Player player in this.players)
			{
				player.isFolded = false;
				player.removeAllCards();
				nbPlayersStillPlaying++;
			}

			this.deck.giveBackCards(this.deck.cards);
			// to do: mix the cards
			this.pot = 0;
			this.highestBet = 0;
			this.nbNoRise = 0;
			this.currentPlayerIndex = 0; // to DO : how do we choose the first player of each round
			this.smallBlind = 0;
			this.bigBlind = this.updateBlind();

			Round r = new Round();
			rounds.Add(r);

			Phase p = new Phase(TypePhase.bet1);
			newCurrentPhase(p);

			deck.shuffleCards();
			distributeCards();
		}
		public int updateBlind()
		{
			this.bigBlind *= 2; //to verify
			return this.bigBlind;
		}

		public List<Card> revealCardsOfPlayer(Player player)
		{
			List<Card> revealedCards = new List<Card>();
			if(player.reveal())
			{
				revealedCards = player.hand;
			}

			nbNoRise++;
			return revealedCards;
		}

	}
}

