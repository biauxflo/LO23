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

		// --- Test rcisnero ---
		public Player player;
		private Card card1 = new Card(1, 'h', 1, true, true);
		private Card card2 = new Card(2, 's', 2, true, true);
		private Card card3 = new Card(3, 'c', 3, true, true);

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
			Game = game;

			ParamCommand = new RelayCommand(OnParamClick);

			FoldCommand = new RelayCommand(OnFoldClick);

			CallCommand = new RelayCommand(OnCallClick);

			RaiseCommand = new RelayCommand(OnRaiseClick);

			// --- Test rcisnero ---
			player = new Player(100);
			TestCards();
			// --- Fin Test rcisnero ---

			Display();
			
		}

		// fonction lié au bouton
		// mécanisme temporaire juste pour tester affichage d'une console de message
		private void OnParamClick()
		{
			// exemple de fenêtre warning 
			/* MessageBoxResult result = MessageBox.Show("message dans la fenêtre","nom de la fenêtre", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if(result == MessageBoxResult.OK)
             {

             } */
			MessageBox.Show("message dans la fenêtre", "nom de la fenêtre", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
		}

		private void OnFoldClick()
		{
			//MessageBox.Show("bouton fold", "bouton fold", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
			
			// --- Test rcisnero ---
			player.Card[0] = "/Client;component/ihm-game/Views/images/cards/" + this.card3.value + "_" + this.card3.color + ".png";
			OnPropertyChanged(nameof(Player));
			// --- Fin Test rcisnero ---

		}

		private void OnCallClick()
		{
			MessageBox.Show("bouton call", "bouton call", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
		}

		private void OnRaiseClick()
		{
			MessageBox.Show("bouton raise", "bouton raise", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
		}
		public void Display()
		{
			//Fonctions à remplacer par les fonctions qui seront implémenter dans IHMGameCallsData
		}

		// --- Test rcisnero ---
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

	}
}
