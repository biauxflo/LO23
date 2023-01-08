using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;

namespace Client.ihm_game.ViewModels
{
	internal class GameViewModel : INotifyPropertyChanged
	{
		// pour ajouter un bouton, dans le xaml section bouton-> Command="{Binding Path=ParamCommand}"
		// exemple: <Button Name="BT_parameter"  Grid.Row="0" Grid.Column="0"  BorderThickness="0" Background="#a2aebb" Command="{Binding Path=ParamCommand}">
		public ICommand ParamCommand
		{
			get; set;
		}

		public ICommand FoldCommand
		{
			get; set;
		}

		public ICommand CallCommand
		{
			get; set;
		}

		public ICommand RaiseCommand
		{
			get; set;
		}
		public ICommand DefausserCommand
		{
			get; set;
		}

		public ICommand GarderMainCommand
		{
			get; set;
		}

		public ICommand CardCommand1
		{
			get; set;
		}

		public ICommand CardCommand2
		{
			get; set;
		}

		public ICommand CardCommand3
		{
			get; set;
		}

		public ICommand CardCommand4
		{
			get; set;
		}

		public ICommand CardCommand5
		{
			get; set;
		}


		private Game game;
		public Game Game
		{
			get => game;
			set
			{
				game = value;
				OnPropertyChanged(nameof(Game));
			}
		}

		// TODO : changer vers List<Player> et utiliser la methode d'Eliot (sortList)
		private Player player;
		public Player Player
		{
			get => player;
			set
			{
				player = value;
				OnPropertyChanged(nameof(Player));
			}
		}

		private List<Player> playerList;
		public List<Player> PlayerList
		{
			get => playerList;
			set
			{
				playerList = value;
				OnPropertyChanged(nameof(PlayerList));
			}
		}


		private List<string> cardList;
		public List<string> CardList
		{
			get => cardList;
			set
			{
				cardList = value;
				OnPropertyChanged(nameof(CardList));
			}
		}

		private LightUser lightUser;
		public LightUser LightUser
        {
			get => lightUser;
            set
            {
				lightUser = value;
				OnPropertyChanged(nameof(LightUser));
            }
        }
		private int bet;
		public int Bet
		{
			get => bet;
			set
			{
				bet = value;
				OnPropertyChanged(nameof(Bet));
			}
		}
		private readonly IhmGameCore core;
		public event PropertyChangedEventHandler PropertyChanged;

		/** Draw phase : we store the selected cards to change */
		private List<bool> selectedCards;

		
		/** TODO : delete when the player list is ok */
		private Card card1 = new Card(1, 'h', 1, true, true);
		private Card card2 = new Card(2, 's', 10, true, true);
		private Card card3 = new Card(3, 'c', 13, true, true);

		/** ------- */
		
		private string visibilityPlayer2;
		public string VisibilityPlayer2
		{
			get => visibilityPlayer2;
			set
			{
				visibilityPlayer2 = value;
				OnPropertyChanged();
			}
		}
		private string visibilityPlayer3;
		public string VisibilityPlayer3
		{
			get => visibilityPlayer3;
			set
			{
				visibilityPlayer3 = value;
				OnPropertyChanged();
			}
		}
		private string visibilityPlayer4;
		public string VisibilityPlayer4
		{
			get => visibilityPlayer4;
			set
			{
				visibilityPlayer4 = value;
				OnPropertyChanged();
			}
		}
		private string visibilityPlayer5;
		public string VisibilityPlayer5
		{
			get => visibilityPlayer5;
			set
			{
				visibilityPlayer5 = value;
				OnPropertyChanged();
			}
		}
		private string visibilityPlayer6;
		public string VisibilityPlayer6
		{
			get => visibilityPlayer6;
			set
			{
				visibilityPlayer6 = value;
				OnPropertyChanged();
			}
		}
		private string visibilityPlayer7;
		public string VisibilityPlayer7
		{
			get => visibilityPlayer7;
			set
			{
				visibilityPlayer7 = value;
				OnPropertyChanged();
			}
		}
		private string visibilityPlayer8;
		public string VisibilityPlayer8
		{
			get => visibilityPlayer8;
			set
			{
				visibilityPlayer8 = value;
				OnPropertyChanged();
			}
		}

		private GameStatus gameStatus;
		public GameViewModel(IhmGameCore core, Game game) 
		{
			this.core = core;
			this.game = game;

			/** 4 status, donc on va reagir d'une façon differente à chaque status :
			 * lobby; pas demarré, player list vide,
			 * running, à partir de 2 joueurs, le jeu commence et on affiche les cartes,
			 * paused, ---
			 * finished, ---
			*/
			this.gameStatus = game.status;
			
			ParamCommand = new RelayCommand(OnParamClick);
			FoldCommand = new RelayCommand(OnFoldClick);
			CallCommand = new RelayCommand(OnCallClick);
			RaiseCommand = new RelayCommand(OnRaiseClick);
            DefausserCommand = new RelayCommand(OnDefausserClick);
			GarderMainCommand = new RelayCommand(OnGarderMainClick);
			CardCommand1 = new RelayCommand(OnCardClick1);
			CardCommand2 = new RelayCommand(OnCardClick2);
			CardCommand3 = new RelayCommand(OnCardClick3);
			CardCommand4 = new RelayCommand(OnCardClick4);
			CardCommand5 = new RelayCommand(OnCardClick5);


            selectedCards = new List<bool> { false, false, false, false, false };
            lightUser = this.core.gameToData.whoAmi();
			OnPropertyChanged(nameof(LightUser));

			/* When gameStatus is "running", the player list is not null.
			 * So, we convert a lightUser to plater and we show his cards. */
			if(gameStatus == GameStatus.running)
            {
				player = ToPlayer(lightUser);
				cardList = CardPath(player.hand);

				// -> A voir si on ajoute ListPlayer
				// Fonction sort Eliot
				playerList = sortList(game.players);

				OnPropertyChanged(nameof(CardList));
				OnPropertyChanged(nameof(PlayerList));
				OnPropertyChanged(nameof(Player));
			}
			//Initialize good button

			// Hidde or show player info depending on the number of players in Game
			// By default only the self player is shown
			ChangeVisibilityPlayers();

			//Display();	
		}

		// fonction lié au bouton de paramètre
		private void OnParamClick()
		{
			core.GoToSettingsPage();
		}

		private void OnFoldClick()
		{
			// Appel fonction data (Gabrielle)
			//this.core.PlayRound(TypeAction.rise); Attente réponse data pour définir le paramètre de type TypeAction
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, new List<Card>(), TypeAction.fold);
			this.core.PlayRound(gameAction);
		}

		private void OnCallClick()
		{
			// TODO : get the correct bet tokens to call
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, new List<Card>(), TypeAction.call);
			this.core.PlayRound(gameAction); 
		}

		private void OnRaiseClick()
		{
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, this.bet, new List<Card>(), TypeAction.rise);
			this.core.PlayRound(gameAction);
		}
		public void Display()
		{
			//Fonctions à remplacer par les fonctions qui seront implémenter dans IHMGameCallsData
		}

		private void OnDefausserClick()
		{
			//Récupération du tableau d'indices des cartes sélectionnées
			/*
			for(int i = 0; i < 5; i++)
			{
				int j = 0;
				int[] tab = {};
				if (selectedCards[i] == true) {
					tab[j] = player.hand[i].index;
					j++;
				}
			}
			*/

			/** --- Test rcisnero ---
            * Change selected cards  */
			List<Card> exchangeCards = new List<Card>();

			for(int i = 0; i < 5; i++)
			{
				if(selectedCards[i])
				{
					exchangeCards.Add(player.hand[i]);
					//player.Card[i] = "/Client;component/ihm-game/Views/images/cards/" + this.card3.value + "_" + this.card3.color + ".png";
				}
			}

			// Appel fonction data (Gabrielle)
			// GameAction, value = bet tokens
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, exchangeCards, TypeAction.exchangeCards);
			this.core.PlayRound(gameAction);
		}

		private void OnGarderMainClick()
		{
			//MessageBox.Show("bouton garder main", "bouton garder main", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, new List<Card>(), TypeAction.exchangeCards);
			this.core.PlayRound(gameAction);
			// Appel fonction data (Gabrielle)
			//this.core.PlayRound(TypeAction.garder_main); Attente réponse data pour définir le paramètre de type TypeAction
		}

		// --- Test rcisnero ---
		// TODO : add togglebutton bindings with selectedCards parameter
		private void OnCardClick1()
		{
			if(!selectedCards[0]) 
				selectedCards[0] = true;
			else 
				selectedCards[0] = false;
		}
		private void OnCardClick2()
		{
			if(!selectedCards[1])
				selectedCards[1] = true;
			else
				selectedCards[1] = false;
		}
		private void OnCardClick3()
		{
			if(!selectedCards[2])
				selectedCards[2] = true;
			else
				selectedCards[2] = false;
		}
		private void OnCardClick4()
		{
			if(!selectedCards[3])
				selectedCards[3] = true;
			else
				selectedCards[3] = false;
		}
		private void OnCardClick5()
		{
			if(!selectedCards[4])
				selectedCards[4] = true;
			else
				selectedCards[4] = false;
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public List<Player> sortList(List<Player> players)
		{
			List<Player> newList = new List<Player>();
			LightUser lu = this.core.gameToData.whoAmi();
			Player firstPlayer = this.ToPlayer(lu);
			int i=0;
			foreach(Player player in players)
			{				
				if(player == firstPlayer)
				{
					break;
				}
				i++;
			}
			for(int j = i; j < players.Count; j++)
			{
				newList.Add(players[j]);

			}
			for(int j = 0; j < i; j++)
			{
				newList.Add(players[j]);
			}
			return newList;
		}

		public Player ToPlayer(LightUser lu)
		{
			Player player = game.players.Find(x => x.id == lu.id);
			return player;
		}

		/** Transform a List of Cards to their path image */
		public List<string> CardPath(List<Card> cards)
        {
			List<string> tmpList = new List<string>();
			foreach(Card card in cards)
            {
				tmpList.Add("/Client;component/ihm-game/Views/images/cards/" + card.value + "_" + card.color + ".png");
            }
			OnPropertyChanged(nameof(cardList));
			return tmpList;
        }
		//Mise à jour de l'affichage du jeu
		public void UpdateGame(Game game)
		{
			//Display player
			player = ToPlayer(lightUser);
			OnPropertyChanged(nameof(Player));
			//Display cards of the player
			cardList = CardPath(player.hand);
			OnPropertyChanged(nameof(CardList));
			//Display info of the other players
			this.ChangeVisibilityPlayers();
			playerList = sortList(game.players);
			OnPropertyChanged(nameof(PlayerList));
		}
		// Hidde or show player info depending on the number of players in Game
		// By default only the self player is shown
		public void ChangeVisibilityPlayers()
		{
			switch(this.game.nbPlayers)
			{
				case 2:
					visibilityPlayer2 = "Hidden";
					visibilityPlayer3 = "Hidden";
					visibilityPlayer4 = "Hidden";
					visibilityPlayer5 = "Visible";
					visibilityPlayer6 = "Hidden";
					visibilityPlayer7 = "Hidden";
					visibilityPlayer8 = "Hidden";
					break;
				case 3:
					visibilityPlayer2 = "Hidden";
					visibilityPlayer3 = "Hidden";
					visibilityPlayer4 = "Visible";
					visibilityPlayer5 = "Visible";
					visibilityPlayer6 = "Hidden";
					visibilityPlayer7 = "Hidden";
					visibilityPlayer8 = "Hidden";
					break;
				case 4:
					visibilityPlayer2 = "Hidden";
					visibilityPlayer3 = "Hidden";
					visibilityPlayer4 = "Visible";
					visibilityPlayer5 = "Visible";
					visibilityPlayer6 = "Visible";
					visibilityPlayer7 = "Hidden";
					visibilityPlayer8 = "Hidden";
					break;
				case 5:
					visibilityPlayer2 = "Hidden";
					visibilityPlayer3 = "Visible";
					visibilityPlayer4 = "Visible";
					visibilityPlayer5 = "Visible";
					visibilityPlayer6 = "Visible";
					visibilityPlayer7 = "Hidden";
					visibilityPlayer8 = "Hidden";
					break;
				case 6:
					visibilityPlayer2 = "Hidden";
					visibilityPlayer3 = "Visible";
					visibilityPlayer4 = "Visible";
					visibilityPlayer5 = "Visible";
					visibilityPlayer6 = "Visible";
					visibilityPlayer7 = "Visible";
					visibilityPlayer8 = "Hidden";
					break;
				case 7:
					visibilityPlayer2 = "Visible";
					visibilityPlayer3 = "Visible";
					visibilityPlayer4 = "Visible";
					visibilityPlayer5 = "Visible";
					visibilityPlayer6 = "Visible";
					visibilityPlayer7 = "Visible";
					visibilityPlayer8 = "Hidden";
					break;
				case 8:
					visibilityPlayer2 = "Visible";
					visibilityPlayer3 = "Visible";
					visibilityPlayer4 = "Visible";
					visibilityPlayer5 = "Visible";
					visibilityPlayer6 = "Visible";
					visibilityPlayer7 = "Visible";
					visibilityPlayer8 = "Visible";
					break;
				default:
					visibilityPlayer2 = "Hidden";
					visibilityPlayer3 = "Hidden";
					visibilityPlayer4 = "Hidden";
					visibilityPlayer5 = "Hidden";
					visibilityPlayer6 = "Hidden";
					visibilityPlayer7 = "Hidden";
					visibilityPlayer8 = "Hidden";
					break;
			}
			OnPropertyChanged(nameof(VisibilityPlayer2));
			OnPropertyChanged(nameof(VisibilityPlayer3));
			OnPropertyChanged(nameof(VisibilityPlayer4));
			OnPropertyChanged(nameof(VisibilityPlayer5));
			OnPropertyChanged(nameof(VisibilityPlayer6));
			OnPropertyChanged(nameof(VisibilityPlayer7));
			OnPropertyChanged(nameof(VisibilityPlayer8));
		}
	}
}
