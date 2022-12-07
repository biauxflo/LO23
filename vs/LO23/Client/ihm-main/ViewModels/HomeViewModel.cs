using GalaSoft.MvvmLight.Command;
using System;
using Shared.data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
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

		public event PropertyChangedEventHandler PropertyChanged;

        public HomeViewModel(IhmMainCore core)
        {
            GameCreationCommand = new RelayCommand(CreateNewGame, true);
            GameLaunchingCommand = new RelayCommand<object>(LaunchGame, true);

            this.core = core;
        }

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
    }
}
