using Client.ihm_main.DTO;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
    internal class GameCreationViewModel
    {
        public ObservableCollection<Game> Games { get; set; }

        //Partie en cours de création
        private Game partie1 = new Game();
        public Game Partie1
        {
            get => partie1;
            set
            {
                partie1 = value;
            }
        }

        /// <summary>
        /// Commande liée au bouton de creation.
        /// </summary>
        public ICommand CreationCommand { get; set; }

        /// <summary>
        /// Commande liée au bouton "Annuler".
        /// </summary>
        public ICommand CancelCommand { get; set; }

        private readonly IhmMainCore core;

        public GameCreationViewModel(IhmMainCore core)
        {
            Games = new ObservableCollection<Game>();

            CreationCommand = new RelayCommand(OnCreationClick, true);
            CancelCommand = new RelayCommand(OnCancelClick, true);

            Games.Add(new Game("partie1", 2, 2500, true, true));

            this.core = core;
        }

        private void OnCancelClick()
        {
            core.BackToHomePage();
        }

        private void OnCreationClick()
        {
            // TODO : Mettre en place l'appel au module Data pour recuperer les parties en cours
            string messageBoxText = string.Empty;
            MessageBoxImage icon = MessageBoxImage.None;
            string windowCaption = "Résultat de création de partie";
            MessageBoxButton button = MessageBoxButton.OK;

            if(Games
                .Any(game => game.Nom == partie1.Nom))
            {
                messageBoxText = "Nom de Partie déjà existant";
                icon = MessageBoxImage.Error;
            }
            else
            {
                messageBoxText = "Création réussie";
                icon = MessageBoxImage.Information;
            }

            MessageBox.Show(messageBoxText, windowCaption, button, icon, MessageBoxResult.OK);
        }

    }

}
