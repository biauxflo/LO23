using GalaSoft.MvvmLight.Command;
using System;
using Shared.data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
	/// <summary>
	/// Classe <c>HomeViewModel</c> modélise la page d'accueil et implémente INotifyPropertyChanged
	/// </summary>
	internal class HomeViewModel : INotifyPropertyChanged
    {
        /// <summary>
		/// Core principal du module IhmMain.
		/// </summary>
        private readonly IhmMainCore core;
        
		/// <summary>
		/// Utilisateur crée avec le formulaire de connexion.
		/// </summary>
		private LightUser connectedUser;
		/// <summary>
		/// Utilisateur crée avec le formulaire de connexion.
		/// </summary>
		public LightUser ConnectedUser
		{
			get => connectedUser;
			set
    {
				connectedUser = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
        /// Liste des parties accessibles.
        /// </summary>
		private ObservableCollection<LightGame> games;
		/// <summary>
		/// Liste des parties accessibles.
		/// </summary>
		public ObservableCollection<LightGame> Games
        {
            get => games;
			set
        {
				games = value;
				OnPropertyChanged();
			}
        }

        /// <summary>
        /// Commande liée au bouton de création de partie.
        /// </summary>
        public ICommand GameCreationCommand { get; set; }

        /// <summary>
        /// Commande liée au bouton de lancement d'une partie.
        /// </summary>
        public ICommand GameLaunchingCommand { get; set; }

		/// <summary>
		/// Commande liée au bouton de lancement d'une partie.
		/// </summary>
		public ICommand GameSpectatingCommand { get; set; }

		/// <summary>
		/// Commande liée au bouton pour revoir une sauvegarde
		/// </summary>
		public ICommand LoadingSaveCommand { get; set; }

        /// <summary>
        /// Déclarer l'événement
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


		/// <summary>
		/// Page à afficher au sein de la fenêtre.
		/// </summary>
		public HomeViewModel(IhmMainCore core)
        {
            GameCreationCommand = new RelayCommand(CreateNewGame, true);
            GameLaunchingCommand = new RelayCommand<object>(LaunchGame, true);
            LoadingSaveCommand = new RelayCommand(LoadSave, true);
			GameSpectatingCommand = new RelayCommand<object>(SpectateGame, true);

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
        /// Lance la partie donnée en paramètre.
        /// </summary>
        /// <param name="obj">Partie à lancer.</param>
        private void LaunchGame(object obj)
        {
            if(obj.GetType() != typeof(LightGame))
            {
                return;
            }
            else
            {
                LightGame game = (LightGame)obj;
				core.TryJoinGame(game.id, ConnectedUser);
            }
        }

        /// <summary>
        /// Ouvre l'interface de création de partie.
        /// </summary>
        private void CreateNewGame()
        {
            core.OpenGameCreationPage();
        }

        ///<summary>
        /// Ouvre la page de gestion de sauvegarde.
        /// </summary>
        private void LoadSave()
        {
            core.OpenLoadingSavePage();
        }

		/// <summary>
		/// Demande la possibilité de spectate une partie en cours.
		/// </summary>
		/// <param name="obj">La partie voulue.</param>
		private void SpectateGame(object obj)
		{
			if(obj.GetType() != typeof(LightGame))
			{
				return;
			}
			else
			{
				LightGame game = (LightGame)obj;
				core.TrySpectateGame(game.id, ConnectedUser);
			}
		}
	}
}
