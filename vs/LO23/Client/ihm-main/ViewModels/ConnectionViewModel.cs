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
		private readonly IhmMainCore core;

		/// <summary>
		/// Utilisateur essaynt de se connecter.
		/// </summary>
		private string username;
		public string Username
		{
			get => username;
			set
			{
				username = value;
				OnPropertyChanged();
			}
		}

		private string password;
		public string Password
		{
			get => password;
			set
			{
				password = value;
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

        public ConnectionViewModel(IhmMainCore core)
        {
			this.core = core;

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
			Username = string.Empty;
			Password = string.Empty;
		}

        /// <summary>
        /// Mécanisme de connexion.
        /// </summary>
        private void OnConnectionClick()
        {
			// TODO : Mettre en place l'appel au module Data
			core.TryAuthenticate(Username, Password);

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
