using System;
using Shared.data;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ihm_main.ViewModels
{
	/// <summary>
	/// Classe <c>GameCreationViewModel</c> modélise la page de création de parties et implémente INotifyPropertyChanged
	/// </summary>
	internal class GameCreationViewModel : INotifyPropertyChanged

    {
		/// <summary>
		/// Core principal du module IhmMain.
		/// </summary>
		private readonly IhmMainCore core;

        /// <summary>
		/// Partie en cours de création.
		/// </summary>
		private GameOptions gameInCreation = new GameOptions(String.Empty, 2000, true, true, 100, 4, 0, 10);
		/// <summary>
		/// Partie en cours de création.
		/// </summary>
		public GameOptions GameInCreation
        {
            get => gameInCreation;
            set
            {
                gameInCreation = value;
				OnPropertyChanged();
            }
        }

		/// <summary>
		/// Liste des parties accessibles.
		/// </summary>
		public ObservableCollection<Game> Games { get; set; }

        /// <summary>
        /// Commande liée au bouton de creation.
        /// </summary>
        public ICommand CreationCommand { get; set; }

        /// <summary>
        /// Commande liée au bouton "Annuler".
        /// </summary>
        public ICommand CancelCommand { get; set; }

		/// <summary>
		/// Déclarer l'événement
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Page à afficher au sein de la fenêtre.
		/// </summary>
		public GameCreationViewModel(IhmMainCore core)
        {
            Games = new ObservableCollection<Game>();

            CreationCommand = new RelayCommand(OnCreationClick, true);
            CancelCommand = new RelayCommand(OnCancelClick, true);

            this.core = core;
        }
		/// <summary>
		/// créer la méthode OnPropertyChanges pour créer un événement.
		/// Le nom du membre appelant sera utilisé comme paramètre
		/// </summary>
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
		/// <summary>
		/// Commande liée au bouton "Annuler"
		/// Renvoie à l'accueil
		/// </summary>
		private void OnCancelClick()
    {
			GameInCreation = new GameOptions(String.Empty, 2000, true, true, 100, 4, 0, 10);
            core.BackToHomePage();
        }

		/// <summary>
		/// Ouvre l'interface de création de partie.
		/// </summary>
		private void OnCreationClick()
        {
			core.TryCreateNewGame(GameInCreation);
        }
    }
}
