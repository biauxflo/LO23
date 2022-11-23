using Shared.data;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
    internal class ConnectionViewModel
    {
        /// <summary>
        /// Utilisateur essaynt de se connecter.
        /// </summary>
        private User user1 = new User();
        public User User1
        {
            get => user1;
            set
            {
                user1 = value;
            }
        }

        /// <summary>
        /// Utilisateur test (pour verifier que le mecanisme de connexion fonctionne).
        /// </summary>
        private User user2;
        public User User2
        {
            get => user2;
            set
            {
                user2 = value;
            }
        }

        /// <summary>
        /// Commande liée au bouton de connexion.
        /// </summary>
        public ICommand ConnectionCommand { get; set; }

        /// <summary>
        /// Commande liée au bouton "Quitter".
        /// </summary>
        public ICommand QuitCommand { get; set; }

        public ConnectionViewModel()
        {
            user2 = new User(1,"utilisateur", null,"utilisateur123", true, "test", "test", 18);
            ConnectionCommand = new RelayCommand(OnConnectionClick, true);
            QuitCommand = new RelayCommand(OnQuitClick, true);
        }

        /// <summary>
        /// Mécanisme de connexion.
        /// </summary>
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

        /// <summary>
        /// Mécansime de fermeture de l'application.
        /// </summary>
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
    }
}
