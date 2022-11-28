using Shared.data;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ihm_main.ViewModels
{
    internal class ConnectionViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Utilisateur essaynt de se connecter.
        /// </summary>
        private User connectionUser = new User();
        public User ConnectionUser
        {
            get => connectionUser;
            set
            {
                connectionUser = value;
				OnPropertyChanged();
            }
        }

        /// <summary>
        /// Utilisateur test (pour verifier que le mecanisme de connexion fonctionne).
        /// </summary>
		// TODO : Supprimer lors de l'integration
        private User user2;
		public User User2
        {
            get => user2;
            set
            {
                user2 = value;
				OnPropertyChanged();
            }
        }
		public event PropertyChangedEventHandler PropertyChanged;

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

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		/// <summary>
		/// Remet à zéro le formulaire de connexion.
		/// </summary>
		internal void Reset()
		{
			ConnectionUser.username = string.Empty;
			ConnectionUser.password = string.Empty;
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

            if(user2.Equals(ConnectionUser))
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
