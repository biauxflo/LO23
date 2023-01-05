using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ihm_main.ViewModels
{
    internal class ProfilCreationViewModel : INotifyPropertyChanged
    {
		/// <summary>
		/// Controleur du module IHM-Main.
		/// </summary>
		private readonly IhmMainCore core;

		/// <summary>
		/// Pseudonyme de l'utilisateur à créer.
		/// </summary>
		private string username = string.Empty;
		public string Username
		{
			get => username;
			set
			{
				username = value;
				OnPropertyChanged("UsernameIsEmpty");
				OnPropertyChanged("");
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// <see langword="true"/> si le champ username est vide, <see langword="false"/> sinon.
		/// </summary>
		public bool UsernameIsEmpty => Username == string.Empty;

		/// <summary>
		/// Nom de famille de l'utilisateur à créer.
		/// </summary>
		private string lastname = string.Empty;
		public string Lastname
		{
			get => lastname;
			set
			{
				lastname = value;
				OnPropertyChanged();

			}
		}

		/// <summary>
		/// Prénom de l'utilisateur à créer.
		/// </summary>
		private string firstname = string.Empty;
		public string Firstname
		{
			get => firstname;
			set
			{
				firstname = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Mot de passe de l'utilisateur à créer.
		/// </summary>
		private string password = string.Empty;
		private string mask = string.Empty;
		public string Password
		{
			get => mask;
			set
			{
				if(value.Length > mask.Length)
				{
					password += value.Substring(mask.Length);
				}
				else
				{
					password = password.Substring(0, value.Length);
				}
				mask = "";
				foreach(char c in password)
				{
					mask += "*";
				}
				OnPropertyChanged();
				OnPropertyChanged("ConfirmationIsFalse");
				OnPropertyChanged("PasswordIsEmpty");
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// <see langword="true"/> si le champ password est vide, <see langword="false"/> sinon.
		/// </summary>
		public bool PasswordIsEmpty => Password == string.Empty;

		/// <summary>
		/// Confirmation du mot de passe de l'utilisateur à créer.
		/// </summary>
		private string confirmPassword = string.Empty;
		private string confirmMask = string.Empty;
		public string ConfirmPassword
		{
			get => confirmMask;
			set
			{
				if(value.Length > confirmMask.Length)
				{
					confirmPassword += value.Substring(confirmMask.Length);
				}
				else
				{
					confirmPassword = confirmPassword.Substring(0, value.Length);
				}
				confirmMask = "";
				foreach(char c in confirmPassword)
				{
					confirmMask += "*";
				}
				OnPropertyChanged();
				OnPropertyChanged("ConfirmationIsFalse");
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Age de l'utilisateur à créer.
		/// </summary>
		private int age = 0;
		public int Age
		{
			get => age;
			set
			{
				age = value;
				OnPropertyChanged();
				OnPropertyChanged("AgeUnder18");
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// <see langword="true"/> si l'âge est inférieur à 18, <see langword="false"/> sinon.
		/// </summary>
		public bool AgeUnder18 => Age < 18;

		/// <summary>
		/// Change la visibilité du champ de mot de passe ou non.
		/// </summary>
		private bool seePassword = false;
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
		/// Change la visibilité du champ de confirmation de mot de passe ou non.
		/// </summary>
		private bool seeConfirmPassword = false;
		public bool SeeConfirmPassword
		{
			get => seeConfirmPassword;
			set
			{
				seeConfirmPassword = value;
				if(value)
				{
					confirmMask = confirmPassword;
				}
				else
				{
					confirmMask = "";
					if(confirmPassword != string.Empty)
					{
						foreach(char c in confirmPassword)
						{
							confirmMask += "*";
						}
					}
				}
				OnPropertyChanged();
				OnPropertyChanged("ConfirmPassword");
			}
		}

		/// <summary>
		/// <see langword="true"/> si le mot de passe et sa confirmation sont égaux, <see langword="false"/> sinon.
		/// </summary>
		public bool ConfirmationIsFalse => confirmPassword != password;

		/// <summary>
		/// Commande du bouton "Créer".
		/// </summary>
		public RelayCommand CreationCommand { get; set; }

		/// <summary>
		/// Commande du bouton "Annuler"
		/// </summary>
        public ICommand CancelCommand { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public ProfilCreationViewModel(IhmMainCore core)
        {
			// Bind command to Execute and CanExecute
            CreationCommand = new RelayCommand(OnCreationClick, CreationCanExecute);
            CancelCommand = new RelayCommand(OnCancelClick, true);

            this.core = core;
        }

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		/// <summary>
		/// Remet à zéro les valeurs de l'objet.
		/// </summary>
		internal void Reset()
		{
			Username = string.Empty;
			Firstname = string.Empty;
			Lastname = string.Empty;
			Password = string.Empty;
			ConfirmPassword = string.Empty;
			Age = 1;
			SeeConfirmPassword = false;
			SeePassword = false;
		}

		/// <summary>
		/// Calcule si une demande de création de partie peut-être demandée.
		/// </summary>
		/// <returns><see langword="true"/>Si on peut lancer la demande de création de partie, <see langword="false"/> sinon.</returns>
		private bool CreationCanExecute()
		{
			return ConfirmationIsFalse && Username != string.Empty && Age <= 18 && Password != string.Empty;
		}

		/// <summary>
		/// Logique lors de l'annulation d'une création de profil.
		/// </summary>
		private void OnCancelClick()
        {
			// Calls the core to go back to connection page.
            core.Disconnect();
			this.Reset();
        }

		/// <summary>
		/// Logique de création d'un profil.
		/// </summary>
        private void OnCreationClick()
        {
			// Calls the core to try the creation of this profile.
			core.TryCreateProfile(Username, Password, Firstname, Lastname, Age);
        }
    }
}
