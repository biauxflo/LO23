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

		public int goToNextPlayer()
		{
			int nextPlayerIndex = (this.currentPlayerIndex + 1) % this.players.Count;
			this.currentPlayerIndex = nextPlayerIndex;

			return nextPlayerIndex;
		}

		// Sort the Hand of the player (Select Sort)
		public void sortHand(List<Card> hand)
		{
			List<Card> sortedHand = hand.OrderBy(card => card.value).ToList();
			hand = sortedHand;
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

		public void handleGameAction(Guid playerId, GameAction action)
		{
			Player player = this.players.Find(x =>
			{
				return x.id == playerId;
			});
			int value = action.value;
			List<Card> listOfCards = action.cards;
			this.chooseAction(player, value, action, listOfCards);
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

		//Flush
		public bool isFlush(List<Card> hand)
		{
			char firstColor = hand.First().color;
			return hand.Where(card => card.color == firstColor).ToList().Count == 5;
		}

		//Straight
		public bool isStraight(List<Card> hand)
		{
			int firstValue = hand.First().value;
			if(hand.Last().value == 14 && firstValue == 2)
			{
				return hand.Where((card, index) => card.value == firstValue + index).ToList().Count == 4;
			}
			return hand.Where((card, index) => card.value == firstValue + index).ToList().Count == 5;

		}

		//Royal Flush
		public bool isRoyalFlush(List<Card> hand)
		{
			return hand.First().value == 10 && this.isStraight(hand) && this.isFlush(hand);
		}

		//Straight Flush
		public bool isStraightFlush(List<Card> hand)
		{
			return this.isFlush(hand) && this.isStraight(hand);
		}

		//Four of a kind
		public bool isFourOfAKind(List<Card> hand)
		{
			int middleCardValue = hand[2].value;
			return hand.Where(card => card.value == middleCardValue).ToList().Count == 4;
		}

		//Three of a kind
		public bool isThreeOfAKind(List<Card> hand)
		{
			int middleCardValue = hand[2].value;
			return hand.Where(card => card.value == middleCardValue).ToList().Count == 3;
		}

		//Two pair
		public bool isTwoPair(List<Card> hand)
		{
			List<int> handValues = hand.Select(card => card.value).ToList();
			return !isThreeOfAKind(hand) && handValues.Distinct().ToList().Count == 3;
		}

		//One pair
		public bool isPair(List<Card> hand)
		{
			List<int> handValues = hand.Select(card => card.value).ToList();
			return (!isThreeOfAKind(hand) && handValues.Distinct().ToList().Count == 4) || (isThreeOfAKind(hand) && handValues.Distinct().ToList().Count == 2);
		}

		//Full house
		public bool isFullHouse(List<Card> hand)
		{
			return isThreeOfAKind(hand) && isPair(hand);
		}

		//find the winner (sort is done in the main)
		public List<Player> findWinner()
		{
			List<Player> listWinners = new List<Player>();

			this.attributeEachScoreToPlayerForHisCombo();
			listWinners = this.getWinner();

			return listWinners;
		}
		public Player getPlayerWinner()
		{
			Player winner = null;
			int highestScore = 0;
			foreach(Player actualPlayer in this.players)
			{
				if(actualPlayer.score > highestScore)
				{
					highestScore = actualPlayer.score;
					winner = actualPlayer;
				}
			}
			return winner;
		}
		public List<Player> getWinner()
		{
			Player winner = this.getPlayerWinner();
			List<Player> listWinners = new List<Player>();
			listWinners.Add(winner);

			foreach(Player actualPlayer in this.players)
			{
				if(actualPlayer.score == winner.score && winner != actualPlayer)
				{
					//Equality of Royal Flush
					if(actualPlayer.score == 10)
					{
						listWinners.Add(actualPlayer);
					}

					//Equality of Straight Flush  or Equality of Straight
					else if(actualPlayer.score == 9 || actualPlayer.score == 5)
					{
						//If the winner has a 14 (Ace) and the player has not, the player become the winner
						if((this.isBetterCardThanWinner(actualPlayer.hand.Last(), winner.hand.Last()) && actualPlayer.hand.Last().value != 14) || (actualPlayer.hand.Last().value != 14 && winner.hand.Last().value == 14))
						{
							winner = this.replaceWinner(listWinners, winner, actualPlayer);
						}
						else if(this.isSameCard(actualPlayer.hand.Last(), winner.hand.Last()))
						{
							listWinners.Add(actualPlayer);
						}
					}

					//Equality of Four of a kind or Equality Full House or Equality of three of kind
					else if((actualPlayer.score == 8) || actualPlayer.score == 7 || actualPlayer.score == 4)
					{
						if(this.isBetterCardThanWinner(actualPlayer.hand[2], winner.hand[2]))
						{
							winner = this.replaceWinner(listWinners, winner, actualPlayer);
						}
					}

					//Equality of Flush
					else if(actualPlayer.score == 6 || actualPlayer.score == 1)
					{
						//Compare each cards as long as they are equal and replace the winner if needed
						winner = this.compareEachCardOfTheHandOfThePlayerWithTheCardOfTheWinner(listWinners, winner, actualPlayer, 4);
					}

					//Equality of two pairs
					else if(actualPlayer.score == 3)
					{
						//The strongest pair is always in index 3, so we compare them
						if(this.isBetterCardThanWinner(actualPlayer.hand[3], winner.hand[3]))
						{
							winner = this.replaceWinner(listWinners, winner, actualPlayer);
						}
						else if(this.isSameCard(actualPlayer.hand[3], winner.hand[3]))
						{
							//The weakest pair is always in index 1, so we compare them
							if(this.isBetterCardThanWinner(actualPlayer.hand[1], winner.hand[1]))
							{
								winner = this.replaceWinner(listWinners, winner, actualPlayer);
							}
							else if(this.isSameCard(actualPlayer.hand[1], winner.hand[1]))
							{
								Card appearOneTimePlayer = FindElementThatAppearsOnlyOnce(actualPlayer.hand).First();
								Card appearOneTimeWinner = FindElementThatAppearsOnlyOnce(winner.hand).First();

								if(this.isBetterCardThanWinner(appearOneTimePlayer, appearOneTimeWinner))
								{
									winner = this.replaceWinner(listWinners, winner, actualPlayer);
								}
								else if(this.isSameCard(appearOneTimePlayer, appearOneTimeWinner))
								{
									listWinners.Add(actualPlayer);
								}
							}
						}
					}

					//Equality of One pair 
					else if(actualPlayer.score == 2)
					{
						Card getpairForActualPlayer = this.findPair(actualPlayer.hand).First();
						Card getpairForWinner = this.findPair(winner.hand).First();

						if(this.isBetterCardThanWinner(getpairForActualPlayer, getpairForWinner))
						{
							winner = this.replaceWinner(listWinners, winner, actualPlayer);
						}

						else if(this.isSameCard(getpairForActualPlayer, getpairForWinner))
						{
							List<Card> appearOneTimePlayer = FindElementThatAppearsOnlyOnce(actualPlayer.hand);
							List<Card> appearOneTimeWinner = FindElementThatAppearsOnlyOnce(winner.hand);

							//Compare each cards (exepting the pair) as long as they are equal and replace the winner if needed
							winner = this.compareEachCardOfTheHandOfThePlayerWithTheCardOfTheWinner(listWinners, winner, actualPlayer, 2);
						}
					}

				}


			}

			return listWinners;
		}

		//Method to find that appear only once in the list
		public List<Card> FindElementThatAppearsOnlyOnce(List<Card> list)
		{
			List<Card> listToReturn = new List<Card>();
			foreach(Card card in list)
			{
				if(list.Count(x => x.value == card.value) == 1)
				{
					listToReturn.Add(card);
				}
			}

			return listToReturn;
		}

		//Method that find the element that appears only once in a list for one pair
		public List<Card> findPair(List<Card> list)
		{
			List<Card> element = new List<Card>();
			int count = 0;
			for(int i = 0; i < list.Count; i++)
			{
				for(int j = i + 1; j < list.Count; j++)
				{
					if(list[i].value == list[j].value)
					{
						count++;
					}
				}
				if(count == 1)
				{
					element.Add(list[i]);

				}
				count = 0;
			}

			return element;
		}

		public Player replaceWinner(List<Player> listWinners, Player winner, Player actualPlayer)
		{
			winner = actualPlayer;
			listWinners.Clear();
			listWinners.Add(actualPlayer);

			return winner;
		}

		//Method that compare the value of two cards
		public bool isBetterCardThanWinner(Card card, Card winner)
		{
			return card.value > winner.value;
		}

		public bool isSameCard(Card card, Card winner)
		{
			return card.value == winner.value;
		}

		//Attribute the score of his combo to the player
		public void attributeEachScoreToPlayerForHisCombo()
		{
			foreach(Player actualPlayer in this.players)
			{
				if(this.isRoyalFlush(actualPlayer.hand))
				{
					actualPlayer.score = 10;
				}
				else if(this.isStraightFlush(actualPlayer.hand))
				{
					actualPlayer.score = 9;
				}
				else if(this.isFourOfAKind(actualPlayer.hand))
				{
					actualPlayer.score = 8;
				}
				else if(this.isFullHouse(actualPlayer.hand))
				{
					actualPlayer.score = 7;
				}
				else if(this.isFlush(actualPlayer.hand))
				{
					actualPlayer.score = 6;
				}
				else if(this.isStraight(actualPlayer.hand))
				{
					actualPlayer.score = 5;
				}
				else if(this.isThreeOfAKind(actualPlayer.hand))
				{
					actualPlayer.score = 4;
				}
				else if(this.isTwoPair(actualPlayer.hand))
				{
					actualPlayer.score = 3;
				}
				else if(this.isPair(actualPlayer.hand))
				{
					actualPlayer.score = 2;
				}
				else
				{
					actualPlayer.score = 1;
				}
			}
		}

		//Recursive method that compare each card of the hand of the player with the card of the winner
		public Player compareEachCardOfTheHandOfThePlayerWithTheCardOfTheWinner(List<Player> listWinners, Player winner, Player actualPlayer, int index)
		{
			if(index == -1)
			{
				listWinners.Add(actualPlayer);
			}

			if(index >= 0)
			{
				if(this.isBetterCardThanWinner(actualPlayer.hand[index], winner.hand[index]) == true)
				{
					winner = this.replaceWinner(listWinners, winner, actualPlayer);
					return winner;
				}
				else if(this.isSameCard(actualPlayer.hand[index], winner.hand[index]) == true)
				{
					winner = this.compareEachCardOfTheHandOfThePlayerWithTheCardOfTheWinner(listWinners, winner, actualPlayer, index - 1);
				}
			}
			return null;
		}

		public void chooseAction(Player player, int value, GameAction action, List<Card> listOfCards)
		{
			// Check player existence in the game
			bool isPlayerInTheGame = this.players.Contains(player);
			if(!isPlayerInTheGame)
			{
				Console.WriteLine("Player is not in the game");
			}

			switch(action.typeAction)
			{
				case TypeAction.call:
					this.call(player, value);
					break;

				case TypeAction.rise:
					this.rise(player, value);
					break;
				case TypeAction.allin:
					this.allIn(player, value);

					break;
				case TypeAction.fold:
					this.fold(player, listOfCards);

					break;
				case TypeAction.check:
					this.bet(player, value);
					// TODO: check means not betting, BUT while still being in the game 
					// check only possible if nobody has bet during the current Round
					break;
				case TypeAction.exchangeCards:
					this.exchangeCards(player, listOfCards);
					break;
			}
			// TODO: increment the current player index here 
		}

		private void bet(Player player, int value)
		{
			player.tokensBet = value;
			if(player.tokensBet == this.highestBet)
			{
				_ = this.goToNextPlayer();
			}
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

			}
		}

		private void fold(Player player, List<Card> listOfCards)
		{
			// To remove once we have a proper constructor for game, same as for exchangeCards
			this.deck = new Deck();

			for(int card = 0; card < player.hand.Count; card++)
			{
				player.removeCardFromHand(listOfCards[card]); // we take back the cards from the player
			}
			this.deck.giveBackCards(listOfCards); //give back to the deck the cards
			player.isFolded = true;
		}

		private void allIn(Player player, int value)
		{
			value = player.tokens;
			player.incrementTokens(value, player.tokensBet);
			player.decrementTokens(0, player.tokens);
			this.pot += value;
			this.highestBet = value;
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
		}

		public void resetRound()
		{
			foreach(Player player in this.players)
			{
				player.isFolded = false;
				player.removeAllCards();
			}
			this.deck.giveBackCards(this.deck.cards);
			// to do: mix the cards
			this.pot = 0;
			this.highestBet = 0;
			this.nbNoRise = 0;
			this.currentPlayerIndex = 0; // to DO : how do we choose the first player of each round
			this.smallBlind = 0;
			this.bigBlind = this.updateBlind();
			Phase p = new Phase(TypePhase.bet1);
			this.currentPhase = p;
			Round r = new Round();
			r.addPhase(p);
			this.rounds.Add(r);
			foreach(Player player in this.players)
			{
				//player.distributeCards();
			}
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
			return revealedCards;
		}
	}
}

