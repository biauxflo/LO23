using Shared.constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace Shared.data
{
	/// <summary>
	/// Classe <c>Game</c> Classe modélisant une partie, hérite de LightGame
	/// </summary>
	public class Game : LightGame
	{
		private int nbPlayersStillPlaying;
		public List<Round> rounds
		{
			get; set;
		} 
		public int turn
		{
			set; get;
		} 
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
		} 
		public Phase currentPhase
		{
			set; get;
		}
		public int pot
		{
			set; get;
		} 
		public int highestBet
		{
			set; get;
		} 
		public int nbNoRise
		{
			set; get;
		} 
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
		} 

		public bool gameStarted
		{
			get; set;
		}

		public Game()
		{
		}

		/// <summary>
		/// Constructeur permettant de créer une instance de game
		/// </summary>
		/// <param name="id"></param>
		/// <param name="options"></param>
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
			this.status = GameStatus.lobby;
			this.gameStarted = false;
			
		}
		/// <summary>
		/// Permet de construire une LightGame à partir d'une instance de Game
		/// </summary>
		/// <param name="game"></param>
		/// <returns></returns>
		public static LightGame ToLightGame(Game game)
		{
			return new LightGame(game.id, game.gameOptions);
		}

		/// <summary>
		/// Méthode permettant d'initialiser une partie : fixe l'attribut currentPlayerIndex à 0, appelle la méthode initRound() pour lancer un nouveau tour, 
		/// change le statut de la partie à running, et fixe gameStarted à true pour indiquer que la partie a commencé
		/// </summary>
		public void initGame()
		{
			this.currentPlayerIndex = 0;
			initRound();
			status = GameStatus.running;
			gameStarted = true;

		}

		/// <summary>
		/// Permet d'ajouter un utilisateur dans le lobby de la partie : 
		/// </summary>
		/// <param name="user"></param>
		public void addUser(LightUser user)
		{  
			lobby.Add(user);

			if(!this.gameStarted && (lobby.Count >= gameOptions.NbPlayersMin || lobby.Count >= Constants.NB_PLAYERS_MAX))
			{
				initGame(); //Creating players, giving cards and tokens, starting game
			}
		}

		/// <summary>
		/// Permet de passer au joueur suivant dans la partie en cours
		/// </summary>
		/// <returns></returns>
		public int goToNextPlayer()
		{
			int nextPlayerIndex = (this.currentPlayerIndex + 1) % this.players.Count;
			this.currentPlayerIndex = nextPlayerIndex;

			if(players[nextPlayerIndex].isFolded)
				nextPlayerIndex = goToNextPlayer();

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
		/// <summary>
		/// Permet d'ajouter les tokens du joueur concerné dans la grosse blind
		/// </summary>
		/// <param name="player"></param>
		private void payBigBlind(Player player)
		{
			player.tokens -= this.bigBlind;
			this.pot += this.bigBlind;
		}
		/// <summary>
		/// Permet d'ajouter les tokens du joueur concerné dans la petite blind
		/// </summary>
		/// <param name="player"></param>
		private void paySmallBlind(Player player)
		{
			player.tokens -= this.smallBlind;
			this.pot += this.smallBlind;

		}
		/// <summary>
		/// Cette méthode permet à un joueur d'échanger une ou plusieurs de ses cartes avec une ou plusieurs cartes du deck
		/// </summary>
		/// <param name="player"></param>
		/// <param name="listOfCards"></param>
		public void exchangeCards(Player player, List<Card> listOfCards)
		{
			for(int i = 0; i < listOfCards.Count; i++)
			{
				player.removeCardFromHand(listOfCards[i]); 
			}
			this.deck.changeStatusOfCards(listOfCards); // gives the cards back to the deck 

			for(int i = 0; i < listOfCards.Count; i++)
			{
				Card cardTmp = this.deck.giveNewCard(); // what's the next card i can give
				player.AddCardToHand(cardTmp); 
			}

			nbNoRise++;
		}

		/// <summary>
		/// Permet d'afficher les cartes 
		/// </summary>
		public void printHand(List<Card> hand)
		{
			foreach(Card card in hand)
			{
				Console.WriteLine(card.color + " : " + card.value);
			}
		}

		/// <summary>
		/// Fonction qui permet de savoir si la main du joueur est un Flush ou non
		/// </summary>
		public bool isFlush(List<Card> hand)
		{
			char firstColor = hand.First().color;
			return hand.Where(card => card.color == firstColor).ToList().Count == 5;
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est une quinte ('straight' en anglais)
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
		/// Fonction qui permet de savoir si c'est un 'royal flush'	
		/// </summary>
		public bool isRoyalFlush(List<Card> hand)
		{
			return hand.First().value == 10 && this.isStraight(hand) && this.isFlush(hand);
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un 'straight flush'
		/// </summary>
		public bool isStraightFlush(List<Card> hand)
		{
			return this.isFlush(hand) && this.isStraight(hand);
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un 'four of a kind'
		/// </summary>
		public bool isFourOfAKind(List<Card> hand)
		{
			int middleCardValue = hand[2].value;
			return hand.Where(card => card.value == middleCardValue).ToList().Count == 4;
		}

		/// <summary>
		/// Fonction qui permet de savoir si c'est un 'three of a kind'
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
		/// <summary>
		/// Permet de distribuer le pot
		/// </summary>
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

		/// <summary>
		/// En fonction du typeAction associé à la GameAction transmise, détermine quelle action réaliser
		/// </summary>
		/// <param name="action"></param>
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
		/// <summary>
		/// Permet de passer à la phase de jeu suivante
		/// </summary>
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
		/// <summary>
		/// Permet de créer une instance de phase identique à la phase actuelle
		/// </summary>
		/// <param name="newPhase"></param>
		private void newCurrentPhase(Phase newPhase)
		{
			currentPhase = newPhase;
			rounds[rounds.Count - 1].addPhase(newPhase); //Adding newPhase to current round (which is the last one in the rounds list)
		}
		/// <summary>
		/// Permet de miser un certain nombre de tokens
		/// </summary>
		/// <param name="player"></param>
		/// <param name="value"></param>
		/// <exception cref="Exception"></exception>
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
		/// <summary>
		/// Permet à un joueur de se coucher, et donc abandonner le tour en cours
		/// </summary>
		/// <param name="player"></param>
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
		/// <summary>
		/// Permet à un joueur de miser la même somme que le joueur le précédant dans ce tour
		/// </summary>
		/// <param name="player"></param>
		/// <param name="value"></param>
		/// <exception cref="Exception"></exception>
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
		/// <summary>
		/// Permet d'initialiser un Round
		/// </summary>
		public void initRound()
		{
			nbPlayersStillPlaying = 0;

			foreach(LightUser lu in lobby) //Creation of players - tokens distribution
			{
				if(players.Count >= Constants.NB_PLAYERS_MAX)
					break;

				if(players.Count(pl => pl.id == lu.id) == 0)
				{
					players.Add(new Player(lu, gameOptions.StartingTokens));
					this.nbPlayers += 1;
				}
			}
			
			foreach(Player player in this.players)
			{
				player.resetPlayerForNextRound();
				nbPlayersStillPlaying++;
			}

			deck.changeStatusOfCards(deck.cards);

			this.pot = 0;
			this.highestBet = 0;
			this.nbNoRise = 0;
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
		/// <summary>
		/// Permet de mettre à jour la grosse blind
		/// </summary>
		/// <returns></returns>
		public int updateBlind()
		{
			this.bigBlind *= 2; //to verify
			return this.bigBlind;
		}
		/// <summary>
		/// Permet de révéler les cartes de l'instance de Player lui ayant été transmise
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
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

