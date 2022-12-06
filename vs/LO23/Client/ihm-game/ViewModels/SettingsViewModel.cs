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


namespace Client.ihm_game.ViewModels
{
	internal class SettingsViewModel
	{
		public ICommand BackToPlayCommand
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
		private readonly IhmGameCore core;
		public SettingsViewModel(IhmGameCore core)
		{
			this.core = core;
			BackToPlayCommand = new RelayCommand(OnBackToPlayClick);
			SaveCommand = new RelayCommand(OnSaveClick);
			QuitCommand = new RelayCommand(OnQuitClick);
		}

		// fonction lié au bouton Retour Partie pour quitter les paramètres
		private void OnBackToPlayClick()
		{
			core.BackToGamePage();
		}
		private void OnSaveClick()
		{
			// Demande la confirmation avant de sauvegarder le jeu.
			MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder la partie ?", "Sauvegarder la partie ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
			if(result == MessageBoxResult.OK)
			{
				core.SaveGame();
			}
		}
		private void OnQuitClick()
		{
			// Récupère la fenêtre principale de l'application.
			Window view = Application.Current.MainWindow;

			// Demande la confirmation avant de fermer le jeu.
			MessageBoxResult result = MessageBox.Show(view, "Voulez-vous quitter la partie ?", "Quitter la partie ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

			if(result == MessageBoxResult.OK)
			{
				// TODO appel à Data void leaveGame()
				view.Close();
			}
			// TODO appel de IHM-Main ?
		}
	}
}
