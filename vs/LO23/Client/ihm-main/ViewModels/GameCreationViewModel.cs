using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ihm_main.DTO;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
    internal class GameCreationViewModel
    {
        //Partie en cours de création

        private Game partie1;
        public Game Partie1
        {
            get => partie1;
            set
            {
                partie1 = value;
            }
        }

        private Game partie2;
        public Game Partie2
        {
            get => partie2;
            set
            {
                partie2 = value;
            }
        }

        /// <summary>
        /// Commande liée au bouton de connexion.
        /// </summary>
        public ICommand CreationCommand { get; set; }

        /// <summary>
        /// Commande liée au bouton "Quitter".
        /// </summary>
        public ICommand QuitCommand { get; set; }
        public GameCreationViewModel() {
            partie2 = new Game("Partie123", 2,2,true,true);
            CreationCommand = new RelayCommand(OnConnectionClick, true);
            QuitCommand = new RelayCommand(OnQuitClick, true);
           
        }

        private void OnQuitClick()
        {
            // Récupère la fenêtre principale de l'application.
            Window view = Application.Current.MainWindow;

            // Demande la confirmation avant de fermer l'application.
            MessageBoxResult result = MessageBox.Show(view, "Voulez-vous quitter l'application ?", "Quitter l'application ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if(result == MessageBoxResult.OK)
            {
                view.Close();
            }
        }

        private void OnConnectionClick()
        {
            // TODO : Mettre en place l'appel au module Data
            string messageBoxText = string.Empty;
            MessageBoxImage icon = MessageBoxImage.None;
            string windowCaption = "Résultat de création de partie";
            MessageBoxButton button = MessageBoxButton.OK;

            if(partie1.Equals(partie2))
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
