using Shared.data;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ihm_main.ViewModels
{
	/// <summary>
	/// Classe <c>ConnectionViewModel</c> modélise la page de connexion et implémente INotifyPropertyChanged
	/// </summary>
	internal class ConnectionViewModel : INotifyPropertyChanged
    {
		/// <summary>
		/// Core principal du module IhmMain.
		/// </summary>
		private readonly IhmMainCore core;

		/// <summary>
		/// Utilisateur essayant de se connecter.
		/// </summary>
		private string username;
		/// <summary>
		/// Utilisateur essayant de se connecter.
		/// </summary>
		public string Username
		{
			get => username;
			set
			{
				username = value;
				OnPropertyChanged();
			}
		}
		/// <summary>
		/// Entrée du mot de passe de connection
		/// </summary>
		private string password;
		/// <summary>
		/// Entrée du mot de passe de connection
		/// </summary>
		public string Password
		{
			get => password;
			set
			{
				password = value;
				OnPropertyChanged();
			}
		}
		/// <summary>
		/// Déclarer l'événement
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Commande liée au bouton de connexion.
        /// </summary>
        public ICommand ConnectionCommand { get; set; }

        /// <summary>
        /// Commande liée au bouton "Quitter".
        /// </summary>
        public ICommand QuitCommand { get; set; }

		/// <summary>
		/// Commande liée au bouton "Créer un profil".
		/// </summary>
		public ICommand CreateProfileCommand { get; set; }

		public ConnectionViewModel(IhmMainCore core)
        {
			this.core = core;

            ConnectionCommand = new RelayCommand(OnConnectionClick, true);
            QuitCommand = new RelayCommand(OnQuitClick, true);
			CreateProfileCommand = new RelayCommand(OnProfileCreationClick, true);
        }

		/// <summary>
		/// Crée la méthode OnPropertyChanges pour créer un événement.
		/// Le nom du membre appelant sera utilisé comme paramètre
		/// </summary>
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

        /// <summary>
		/// Remet à zéro le formulaire de connexion.
		/// </summary>
		internal void Reset()
		{
			Username = string.Empty;
			Password = string.Empty;
		}

		/// <summary>
		/// Appelle la page de création de profil.
		/// </summary>
		private void OnProfileCreationClick()
		{
			core.ShowProfileCreationPage();
		}

        /// <summary>
        /// Mécanisme de connexion.
        /// </summary>
        private void OnConnectionClick()
        {
			core.TryAuthenticate(Username, Password);
		}

		/// <summary>
		/// Mécansime de fermeture de l'application.
		/// </summary>
		private void OnQuitClick()
		{
            // Get current MainWindow.
            Window view = Application.Current.MainWindow;

            // Ask for confirmation before closing the client.
            MessageBoxResult result = MessageBox.Show(view, "Voulez-vous quitter l'application ?", "Quitter l'application ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

			if(result == MessageBoxResult.OK)
			{
                view.Close();
            }
        }
    }
}
