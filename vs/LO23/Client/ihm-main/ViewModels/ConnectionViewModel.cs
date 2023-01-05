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
				ConnectionCommand.RaiseCanExecuteChanged();
			}
		}

		private string password = string.Empty;
		private string mask = string.Empty;
		/// <summary>
		/// Entrée du mot de passe de connection
		/// </summary>
		public string Password
		{
			get => mask;
			set
			{
				// if a char is added
				if(value.Length > mask.Length)
				{
					// we add the char to the password
					password += value.Substring(mask.Length);
				}
				else
				{
					// else we only take the remaining chars
					password = password.Substring(0, value.Length);
				}

				// mask creation
				mask = "";
				foreach(char c in password)
				{
					mask += "*";
				}
				OnPropertyChanged();
				ConnectionCommand.RaiseCanExecuteChanged();
			}
		}

		private bool seePassword = false;
		/// <summary>
		/// Change la visibilité du champ de mot de passe ou non.
		/// </summary>
		public bool SeePassword
		{
			get => seePassword;
			set
			{
				seePassword = value;
				if(value)
				{
					mask = password;
				}
				else
				{
					mask = "";
					if(password != string.Empty)
					{
						foreach(char c in password)
						{
							mask += "*";
						}
					}
				}
				OnPropertyChanged();
				OnPropertyChanged("Password");
			}
		}

		/// <summary>
		/// Déclarer l'événement
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Commande liée au bouton de connexion.
		/// </summary>
		public RelayCommand ConnectionCommand
		{
			get; set;
		}

		/// <summary>
		/// Commande liée au bouton "Quitter".
		/// </summary>
		public ICommand QuitCommand
		{
			get; set;
		}

		/// <summary>
		/// Commande liée au bouton "Créer un profil".
		/// </summary>
		public ICommand CreateProfileCommand
		{
			get; set;
		}

		public ConnectionViewModel(IhmMainCore core)
		{
			this.core = core;

			ConnectionCommand = new RelayCommand(OnConnectionClick, CanConnect);
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
			core.TryAuthenticate(Username, password);
		}

		/// <summary>
		/// Informe si la connexion peut être essayée.
		/// </summary>
		/// <returns><see langword="true"/> si on peut lancer la tentative de connexion, <see langword="false"/> sinon.</returns>
		private bool CanConnect()
		{
			return password != string.Empty && Username != string.Empty;
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
