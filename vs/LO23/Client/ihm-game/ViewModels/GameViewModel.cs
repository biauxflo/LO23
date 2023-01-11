using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ihm_game.ViewModels
{
	internal class GameViewModel : INotifyPropertyChanged
	{
		#region Button and card commands
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

		#endregion

		#region Properties

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
		private int minRise;
		public int MinRise
		{
			get => minRise;
			set
			{
				minRise = value;
				OnPropertyChanged(nameof(MinRise));
			}
		}
		private int nbPlayers;
		public int NbPlayers
		{
			get => nbPlayers;
			set
			{
				nbPlayers = value;
				OnPropertyChanged(nameof(NbPlayers));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Visibility Properties

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

		#endregion

		/** Draw phase : we store the selected cards to change */
		private List<bool> selectedCards;
		private readonly IhmGameCore core;

		/// <summary>
		/// Constructeur de la classe GameViewModel.
		/// </summary>
		/// <param name="core">Recupéré de IHMGameCore</param>
		/// <param name="game">Recupéré de Data</param>
		public GameViewModel(IhmGameCore core, Game game) 
		{
			this.core = core;
			this.game = game;
			
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

			player = ToPlayer(lightUser);
			if(player != null)
            {
				cardList = CardPath(player.hand);

				playerList = game.players.FindAll(pl => pl.id != player.id);

				OnPropertyChanged(nameof(CardList));
				OnPropertyChanged(nameof(PlayerList));
				OnPropertyChanged(nameof(Player));
			}

			ChangeVisibilityPlayers();
		}

		/// <summary>
		/// On affiche la page de configuration (devenir spectateur, quitter la partie, etc)
		/// </summary>
		private void OnParamClick()
		{
			core.GoToSettingsPage(this.game);
		}

		#region Action Buttons
		private void OnFoldClick()
		{
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, new List<Card>(), TypeAction.fold);
			this.core.PlayRound(gameAction);
		}

		private void OnCallClick()
		{
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, new List<Card>(), TypeAction.call);
			this.core.PlayRound(gameAction); 
		}

		private void OnRaiseClick()
		{
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, this.bet, new List<Card>(), TypeAction.rise);
			this.core.PlayRound(gameAction);
		}

		#endregion

		/// <summary>
		/// Fonction pour échanger les cartes, on recupère la liste selectedCards et on envoie l'action et la liste vers Data
		/// </summary>
		private void OnDefausserClick()
		{
			List<Card> exchangeCards = new List<Card>();

			for(int i = 0; i < 5; i++)
			{
				if(selectedCards[i])
				{
					exchangeCards.Add(player.hand[i]);
				}
			}

			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, exchangeCards, TypeAction.exchangeCards);
			this.core.PlayRound(gameAction);
		}

		private void OnGarderMainClick()
		{
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, new List<Card>(), TypeAction.exchangeCards);
			this.core.PlayRound(gameAction);
		}

		#region Selected Cards functions
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

		#endregion

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		/// <summary>
		/// Converti LightUser en Player
		/// </summary>
		/// <param name="lu"></param>
		/// <returns>Objet de type Player</returns>
		public Player ToPlayer(LightUser lu)
		{
			Player player = game.players.Find(x => x.id == lu.id);
			return player;
		}
		
		/// <summary>
		/// On recupère une liste de cartes et on le transforme vers une liste de string avec le chemin d'accès à l'image de la carte
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
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
		/// <summary>
		/// Mise à jour de l'affichage du jeu
		/// </summary>
		public void UpdateGame(Game game)
		{
			player = ToPlayer(lightUser);
			nbPlayers = game.nbPlayers;
			OnPropertyChanged(nameof(NbPlayers));
			if(player != null)
			{
				//Display player
				OnPropertyChanged(nameof(Player));
				//Display cards of the player
				cardList = CardPath(player.hand);
				OnPropertyChanged(nameof(CardList));
				//Put the correct minimum rise
				this.minRise = game.highestBet - player.tokensBet;
				this.bet = this.minRise;
				OnPropertyChanged(nameof(Bet));
				OnPropertyChanged(nameof(MinRise));
			}
			//Display info of the other players
			this.ChangeVisibilityPlayers();
			playerList = game.players.FindAll(pl => pl.id != lightUser.id);
			OnPropertyChanged(nameof(PlayerList));
		}
		/// <summary>
		/// On affiche l'information des joueurs selon le nombre de joueurs dans la partie.
		/// </summary>
		public void ChangeVisibilityPlayers()
		{
			int nbPlayers = this.game.nbPlayers;
			if(this.player == null)
			{
				nbPlayers++; //if spectator then should see the other players
			}
			switch(nbPlayers)
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
