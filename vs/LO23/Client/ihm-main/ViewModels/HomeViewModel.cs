using Client.ihm_main.DTO;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
    internal class HomeViewModel
    {
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

        public HomeViewModel()
        {
            Games = new ObservableCollection<Game>();

            Games.Add(new Game("partie 1",1,1));
            Games.Add(new Game("partie 2",1,2));
            Games.Add(new Game("partie 3",1,3));
            Games.Add(new Game("partie 4", 1, 4));
            Games.Add(new Game("partie 5", 1, 5));
            Games.Add(new Game("partie 6", 1, 6));

            GameCreationCommand = new RelayCommand(CreateNewGame, true);
            GameLaunchingCommand = new RelayCommand<object>(LaunchGame, true);
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
                // TODO : Appel IHM-Game
            }
        }

        /// <summary>
        /// Ouvre l'interface de création de partie.
        /// </summary>
        private void CreateNewGame()
        {
            // TODO : Changer l'écran 
        }
    }
}
