using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.data;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Client.ihm_game.ViewModels
{
	internal class SettingsViewModel
	{
		public ICommand BackToPlayCommand
		{
			get; set;
		}
		public ICommand BecomingSpectatorCommand
		{
			get; set;
		}
		public ICommand SaveCommand
		{
			get; set;
		}
		public ICommand QuitCommand
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
		public event PropertyChangedEventHandler PropertyChanged;
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
		private readonly IhmGameCore core;
		public SettingsViewModel(IhmGameCore core, Game game)
		{
			this.core = core;
			this.game = game;
			lightUser = this.core.gameToData.whoAmi();
			player = ToPlayer(lightUser);
			BackToPlayCommand = new RelayCommand(OnBackToPlayClick);
			BecomingSpectatorCommand = new RelayCommand(OnBecomingSpectatorClick);
			SaveCommand = new RelayCommand(OnSaveClick);
			QuitCommand = new RelayCommand(OnQuitClick);
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		/// <summary>
		/// fonction liée au bouton Retour Partie pour quitter les paramètres
		/// </summary>
		private void OnBackToPlayClick()
		{
			core.BackToGamePage();
		}

		/// <summary>
		/// Fonction pour devenir spectateur
		/// </summary>
		private void OnBecomingSpectatorClick()
		{
			GameAction gameAction = new GameAction(Guid.NewGuid(), this.game.id, this.player, 0, new List<Card>(), TypeAction.becomingSpectator);
			this.core.PlayRound(gameAction);
			core.BackToGamePage();
		}

		/// <summary>
		/// fonction liée au bouton Sauvegarder la partie
		/// </summary>
		private void OnSaveClick()
		{
			// Demande la confirmation avant de sauvegarder le jeu.
			MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder la partie ?", "Sauvegarder la partie ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
			if(result == MessageBoxResult.OK)
			{
				core.SaveGame();
			}
		}

		/// <summary>
		/// fonction liée au bouton Quitter
		/// </summary>
		private void OnQuitClick()
		{
			// Récupère la fenêtre principale de l'application.
			Window view = Application.Current.MainWindow;

			// Demande la confirmation avant de fermer le jeu.
			MessageBoxResult result = MessageBox.Show(view, "Voulez-vous quitter la partie ?", "Quitter la partie ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
			if(result == MessageBoxResult.OK)
			{
				core.GameEnded();
			}
		}

		public Player ToPlayer(LightUser lu)
		{
			Player player = game.players.Find(x => x.id == lu.id);
			return player;
		}
	}
}
