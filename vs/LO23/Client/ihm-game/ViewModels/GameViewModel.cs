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

		private Game game;
		public Game Game
		{
			get=>game;
			set
			{
				game = value;
				OnPropertyChanged();
			}
		}

		private readonly IhmGameCore core;
		public event PropertyChangedEventHandler PropertyChanged;

		/** --- Test rcisnero ---
		 * ICommand methods for each card (impossible to call only one method in WPF ?)
		 */
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

		private List<bool> selectedCards;

		/** TODO : delete (4 lines) when we get actual game from data */
		public Player player;
		private Card card1 = new Card(1, 'h', 1, true, true);
		private Card card2 = new Card(2, 's', 10, true, true);
		private Card card3 = new Card(3, 'c', 13, true, true);
		/** ------- */

		public Player Player
		{
			get => player;
			set
			{
				player = value;
				OnPropertyChanged(nameof(Player));
			}
		}
		// --- Fin Test rcisnero ---

		public GameViewModel(IhmGameCore core, Game game) 
		{
			this.core = core;
			this.game = game;

			ParamCommand = new RelayCommand(OnParamClick);
			FoldCommand = new RelayCommand(OnFoldClick);
			CallCommand = new RelayCommand(OnCallClick);
			RaiseCommand = new RelayCommand(OnRaiseClick);

			// --- Test rcisnero ---
			CardCommand1 = new RelayCommand(OnCardClick1);
			CardCommand2 = new RelayCommand(OnCardClick2);
			CardCommand3 = new RelayCommand(OnCardClick3);
			CardCommand4 = new RelayCommand(OnCardClick4);
			CardCommand5 = new RelayCommand(OnCardClick5);

			/** TODO : delete (3 lines) when we get actual game from data */
			player = new Player(100);
			selectedCards = new List<bool> { false, false, false, false, false };
			TestCards();

			player.tokens = this.game.gameOptions.StartingTokens;
            // --- Fin Test rcisnero ---

			Display();	
		}

		// fonction lié au bouton de paramètre
		private void OnParamClick()
		{
			core.GoToSettingsPage();
		}

		private void OnFoldClick()
		{
			/** --- Test rcisnero ---
			 * TODO : delete when we get actual game from data - Button used to change selected cards
			 * Change selected cards
			 */
			for(int i = 0; i < 5; i++)
			{
				if(selectedCards[i])
				{
					player.Card[i] = "/Client;component/ihm-game/Views/images/cards/" + this.card3.value + "_" + this.card3.color + ".png";
				}
			}

			// Test : pot changed
			this.game.pot = 100;
			OnPropertyChanged(nameof(Game));

			// Test : tokens balance changed (simulates a 500 tokens gain)
			this.player.tokens += 500;
			OnPropertyChanged(nameof(Player));
			// --- Fin Test rcisnero ---

			// Appel fonction data (Gabrielle)
			//this.core.PlayRound(TypeAction.rise); Attente réponse data pour définir le paramètre de type TypeAction

		}

		private void OnCallClick()
		{
			//MessageBox.Show("bouton call", "bouton call", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
			// Test : simulates a 100 tokens call
			// TODO : get the maximum bet from current game (game.highestBet ?) and put it when user click this button
			this.player.tokensBet += 100;
			OnPropertyChanged(nameof(Player));

			// Appel fonction data (Gabrielle)
			//this.core.PlayRound(TypeAction.call); Attente réponse data pour définir le paramètre de type TypeAction

		}

		private void OnRaiseClick()
		{
			MessageBox.Show("bouton raise", "bouton raise", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

			// Appel fonction data (Gabrielle)
			//this.core.PlayRound(TypeAction.rise); Attente réponse data pour définir le paramètre de type TypeAction
		}
		public void Display()
		{
			//Fonctions à remplacer par les fonctions qui seront implémenter dans IHMGameCallsData
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

		/** TODO : delete (TestCards fonction) when we get actual game from data */
		public void TestCards()
		{
			player.Card.Add("/Client;component/ihm-game/Views/images/cards/" + this.card1.value + "_" + this.card1.color + ".png");
			player.Card.Add("/Client;component/ihm-game/Views/images/cards/" + this.card2.value + "_" + this.card2.color + ".png");
			player.Card.Add("/Client;component/ihm-game/Views/images/cards/" + this.card1.value + "_" + this.card1.color + ".png");
			player.Card.Add("/Client;component/ihm-game/Views/images/cards/" + this.card2.value + "_" + this.card2.color + ".png");
			player.Card.Add("/Client;component/ihm-game/Views/images/cards/" + this.card1.value + "_" + this.card1.color + ".png");
			OnPropertyChanged(nameof(Player));
		}
		// --- Fin Test rcisnero ---

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public List<Player> sortList(List<Player> players)
		{
			List<Player> newList = new List<Player>();
			Player firstPlayer = new Player(100);
			int i=0;
			foreach(Player player in players)
			{
				Console.WriteLine(player);
				i++;
				if(player == firstPlayer)
				{
					break;
				}
				
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
	}
}
