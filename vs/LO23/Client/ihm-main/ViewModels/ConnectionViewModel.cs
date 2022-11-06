using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client.ihm_main.DTO;
using Client.ihm_main.Views;
using GalaSoft.MvvmLight.CommandWpf;

namespace Client.ihm_main.ViewModels
{
    class ConnectionViewModel
   {
        private User user1 = new User();
        public User User1
        {
            get => user1;
            set
            {
                user1 = value;
            }
        }

        private User user2;
        public User User2
        {
            get => user2;
            set
            {
                user2 = value;
            }
        }

        public ICommand ConnectionCommand { get; set; }

        public ICommand QuitCommand { get; set; }

        public ConnectionViewModel()
        {
            user2 = new User("utilisateur", "utilisateur123");
            ConnectionCommand = new RelayCommand(OnConnectionClick, true);
            QuitCommand = new RelayCommand(OnQuitClick, true);
        }

        private void OnConnectionClick()
        {
            // TODO : Mettre en place l'appel au module Data
            string messageBoxText = string.Empty;
            MessageBoxImage icon = MessageBoxImage.None;
            string windowCaption = "Résultat de connexion";
            MessageBoxButton button = MessageBoxButton.OK;

            if(user2.Equals(user1))
            {
                messageBoxText = "Connexion réussie";
                icon = MessageBoxImage.Information;
            }
            else
            {
                messageBoxText = "Votre indentifiant ou votre mot de passe est incorrect";
                icon = MessageBoxImage.Error;
            }

            MessageBox.Show(messageBoxText, windowCaption, button, icon, MessageBoxResult.OK);
        }

        private void OnQuitClick()
        {
            Window view = Application.Current.MainWindow;
            
            var result = MessageBox.Show(view, "Voulez-vous quitter l'application ?", "Quitter l'application ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                view.Close();
            }
        }
    }
}
