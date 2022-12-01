using GalaSoft.MvvmLight.Command;
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
		private User connectedUser;
		public User ConnectedUser
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
        public ObservableCollection<Game> Games
        {
            get;
            set;
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
            Games = new ObservableCollection<Game>
            {
                new Game("partie 1", 1),
                new Game("partie 2", 1),
                new Game("partie 3", 1),
                new Game("partie 4", 1),
                new Game("partie 5", 1),
                new Game("partie 6", 1)
            };

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
            if(obj.GetType() != typeof(Game))
            {
                return;
            }
            else
            {
                Game game = (Game)obj;
				// TODO : Appel Data
				// TODO : A supprimer à l'integration
				core.mainToGame.LaunchGame(game);
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
