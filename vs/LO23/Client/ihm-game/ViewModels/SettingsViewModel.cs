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


			Display();
		}

		// fonction lié au bouton
		// mécanisme temporaire juste pour tester affichage d'une console de message
		private void OnBackToPlayClick()
		{
			core.BackToGamePage();
		}
		private void OnSaveClick()
		{
			MessageBox.Show("message dans la fenêtre", "nom de la fenêtre", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
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
		private void Display()
		{
			//Fonctions à remplacer par les fonctions qui seront implémenter dans IHMGameCallsData

		}
	}
}
