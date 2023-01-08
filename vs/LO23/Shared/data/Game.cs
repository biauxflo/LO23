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

		public bool gameStarted
		{
			get; set;
		}
		public Game()
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
			this.gameStarted = false;
			
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
				this.nbPlayers += 1;
				this.nbPlayersStillPlaying += 1;
			}

			initRound();
			status = GameStatus.running;

		}


		public void addUser(LightUser user)
		{
			lobby.Add(user);

			if(lobby.Count >= gameOptions.NbPlayersMin || lobby.Count >= Constants.NB_PLAYERS_MAX)
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

		/// <summary>
		/// Permet de trier la main du joueur
		/// </summary>
		public void sortHand(List<Card> hand)
		{
			List<Card> sortedHand = hand.OrderBy(card => card.value).ToList();
			hand = sortedHand;
		}

		/// <summary>
		/// Permet de distribuer les cartes aux joueurs
		/// </summary>
		public void distributeCards()
		{
			for(int i = 0; i < 5; i++)
			{
				foreach(Player player in this.players)
				{
					Card card = deck.giveNewCard();
					player.hand.Add(card);
				}
			}
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
			// List<Card> listOfNewCards = new List<Card>();
			for(int i = 0; i < listOfCards.Count; i++)
			{
				player.removeCardFromHand(listOfCards[i]); // we take back the cards from the player
			}
			this.deck.changeStatusOfCards(listOfCards); //give back to the deck the cards

			for(int i = 0; i < listOfCards.Count; i++)
			{
				Card cardTmp = this.deck.giveNewCard(); // what's the next card i can give
				// listOfNewCards.Add(cardTmp); //add to the list of new cards
				player.AddCardToHand(cardTmp); // add card to player's hand
			}

			nbNoRise++;
		}

		/// <summary>
		/// Permet d'afficher les cartes
		/// </summary>
		public void printHand(List<Card> hand) // Sylvain's function I needed -> ATTENTION watch out ctrl c ctrl v
		{
			foreach(Card card in hand)
			{
				Console.WriteLine(card.color + " : " + card.value);
			}
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un Flush
		/// </summary>
		public bool isFlush(List<Card> hand)
		{
			char firstColor = hand.First().color;
			return hand.Where(card => card.color == firstColor).ToList().Count == 5;
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un straight
		/// </summary>
		public bool isStraight(List<Card> hand)
		{
			int firstValue = hand.First().value;
			if(hand.Last().value == 14 && firstValue == 2)
			{
				return hand.Where((card, index) => card.value == firstValue + index).ToList().Count == 4;
			}
			return hand.Where((card, index) => card.value == firstValue + index).ToList().Count == 5;

		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un royal flush	
		/// </summary>
		public bool isRoyalFlush(List<Card> hand)
		{
			return hand.First().value == 10 && this.isStraight(hand) && this.isFlush(hand);
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un straight flush
		/// </summary>
		public bool isStraightFlush(List<Card> hand)
		{
			return this.isFlush(hand) && this.isStraight(hand);
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un four of a kind
		/// </summary>
		public bool isFourOfAKind(List<Card> hand)
		{
			int middleCardValue = hand[2].value;
			return hand.Where(card => card.value == middleCardValue).ToList().Count == 4;
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un three of a kind
		/// </summary>
		public bool isThreeOfAKind(List<Card> hand)
		{
			int middleCardValue = hand[2].value;
			return hand.Where(card => card.value == middleCardValue).ToList().Count == 3;
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un two pair
		/// </summary>
		public bool isTwoPair(List<Card> hand)
		{
			List<int> handValues = hand.Select(card => card.value).ToList();
			return !isThreeOfAKind(hand) && handValues.Distinct().ToList().Count == 3;
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un one pair
		/// </summary>
		public bool isPair(List<Card> hand)
		{
			List<int> handValues = hand.Select(card => card.value).ToList();
			return (!isThreeOfAKind(hand) && handValues.Distinct().ToList().Count == 4) || (isThreeOfAKind(hand) && handValues.Distinct().ToList().Count == 2);
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un full house
		/// </summary>
		public bool isFullHouse(List<Card> hand)
		{
			return isThreeOfAKind(hand) && isPair(hand);
		}
		public void distributePot()
		{
			List<Player> listWinners = this.findWinner();
			int nbOfWinners = listWinners.Count;
			if(nbOfWinners != 0)
			{

				int valueToDistribute = this.pot / nbOfWinners;
				foreach(Player player in listWinners)
				{
					player.tokens += valueToDistribute;
				}
				this.pot = 0;
			}
			else
			{
				Console.WriteLine("il n'y a pas de gagnant donc il y aurait eu une division par zéro");

			}

		}

		/// <summary>
		/// Fonction qui permet de renvoyer la liste contenant le ou les gagnants de la partie
		/// </summary>
		public List<Player> findWinner()
		{
			List<Player> listWinners = new List<Player>();

			this.attributeEachScoreToPlayerForHisCombo();
			listWinners = this.getWinner();

			return listWinners;
		}

		/// <summary>
		/// Fonction qui permet de trouver le premier gagnant
		/// </summary>
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

		/// <summary>
		/// Fonction qui permet de trouver tous le ou les gagnant en cas d'égalité
		/// </summary>
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

		/// <summary>
		/// Fonction qui permet de trouver un élément qui apparait une fois dans une liste
		/// </summary>
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

		/// <summary>
		/// Fonction qui permet de trouver les paires d'une liste
		/// </summary>
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

		/// <summary>
		/// Fonction qui permet de remplacer le gagnant par un autre gagnant s'il a une meilleure main
		/// </summary>
		public Player replaceWinner(List<Player> listWinners, Player winner, Player actualPlayer)
		{
			winner = actualPlayer;
			listWinners.Clear();
			listWinners.Add(actualPlayer);

			return winner;
		}

		/// <summary>
		/// Fonction qui permet de comparer deux cartes
		/// </summary>
		public bool isBetterCardThanWinner(Card card, Card winner)
		{
			return card.value > winner.value;
		}

		/// <summary>
		/// Fonction qui permet de savoir si les cartes ont la même valeur
		/// </summary>
		public bool isSameCard(Card card, Card winner)
		{
			return card.value == winner.value;
		}

		/// <summary>
		/// Fonction qui attribue le score à chaque joueur en fonction de sa main
		/// </summary>
		public void attributeEachScoreToPlayerForHisCombo()
		{
			foreach(Player actualPlayer in this.players.Where(p => !p.isFolded ))
			{
        		actualPlayer.hand.Sort();

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

		/// <summary>
		/// Fonction récursive permettant de comparer chaque carte une à une avec avec une autre main
		/// </summary>
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


		public void handleGameAction(GameAction action)
		{
			Console.WriteLine("action.player.id", action.player.id);
			// Check player existence in the game
			Player playerInTheGame = this.players.Find(player => player.id == action.player.id);
			if (playerInTheGame == null)
			{
				Console.WriteLine("Player is not in the game");
			} else {
				switch(action.typeAction)
				{
					case TypeAction.call:
						this.call(playerInTheGame, action.value);
						break;

					case TypeAction.rise:
						this.rise(playerInTheGame, action.value);
						break;
					case TypeAction.allin:
						this.allIn(playerInTheGame, action.value);

						break;
					case TypeAction.fold:
						fold(playerInTheGame);

						break;
					case TypeAction.check:
						nbNoRise++; //check means doing nothing, so not rising, so nbNoRise++
						break;
					case TypeAction.exchangeCards:
						this.exchangeCards(playerInTheGame, action.listOfCards);
						break;
				}

				if(nbNoRise >= nbPlayersStillPlaying)
				{
					goToNextPhase();
				}
				else
				{
					goToNextPlayer();
				}
			}
		}

		private void goToNextPhase()
		{
			Phase newPhase;
			if(nbPlayersStillPlaying == 1)
			{
				newPhase = new Phase(TypePhase.reveal); //Only one player left, so reveal phase
			}
			else
			{
				newPhase = new Phase(++currentPhase.typePhase); //it gives the next phase
			}
			newCurrentPhase(newPhase);
			nbNoRise = 0;

			if(currentPhase.typePhase == TypePhase.reveal) // if the phase we went to is the reveal phase, we distribute pot and start another round
			{
				distributePot();
				initRound();
			}
			else
			{
				goToNextPlayer();
			}

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
				throw new Exception();

			}
			else
			{
				player.decrementTokens(value);
				player.incrementTokensBet(value);
				this.pot += value;


				if((player.tokensBet - highestBet) <= 0)
					nbNoRise++; //No rise has been done, so increasing the nbNoRise value
				else
				{
					nbNoRise = 0; //reset nb turn of no rising event
					this.highestBet += (player.tokensBet - highestBet);
				}
			}
		}

		public void fold(Player player)
		{
			deck.changeStatusOfCards(player.hand);
			player.removeAllCards(); // we take back the cards from the player
			player.isFolded = true;

			nbPlayersStillPlaying--;
		}

		private void allIn(Player player, int value)
		{
			value = player.tokens;
			player.incrementTokensBet(value);
			player.decrementTokens(value);
			this.pot += value;

			if((player.tokensBet - highestBet) <= 0)
				nbNoRise++; //No rise has been done, so increasing the nbNoRise value
			else
			{
				nbNoRise = 0; //reset nb turn of no rising event
				this.highestBet += (player.tokensBet - highestBet);
			}
		}

		private void call(Player player, int value)
		{
			value = this.highestBet - player.tokensBet;
			if(player.tokens < value)
			{
				Console.WriteLine("Player doesn't have enough tokens to bet that amount.");
				throw new Exception();
			}
			else
			{
				player.decrementTokens(value);
				player.incrementTokensBet(value);
				this.pot += value;
			}

			nbNoRise++;
		}

		public void initRound()
		{
			nbPlayersStillPlaying = 0;
			foreach(Player player in this.players)
			{
				player.resetPlayerForNextRound();
				nbPlayersStillPlaying++;
			}

			deck.changeStatusOfCards(deck.cards);

			this.pot = 0;
			this.highestBet = 0;
			this.nbNoRise = 0;
			this.currentPlayerIndex = 0;
			goToNextPlayer();
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

