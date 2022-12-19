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
				OnPropertyChanged();
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

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
		public string Password
		{
			get => password;
			set
			{
				password = value;
				OnPropertyChanged();
				OnPropertyChanged("ConfirmationIsRight");
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Confirmation du mot de passe de l'utilisateur à créer.
		/// </summary>
		private string confirmPassword = string.Empty;
		public string ConfirmPassword
		{
			get => confirmPassword;
			set
			{
				confirmPassword = value;
				OnPropertyChanged();
				OnPropertyChanged("ConfirmationIsRight");
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Age de l'utilisateur à créer.
		/// </summary>
		private int age = 0;
		public string Age
		{
			get => age.ToString();
			set
			{
				if(int.TryParse(value, out int toInt))
				{
					age = toInt;
					OnPropertyChanged();
				}
				else
				{
					MessageBox.Show("La valeur d'age entrée n'est pas un nombre entier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				CreationCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// <see langword="true"/> si le mot de passe et sa confirmation sont égaux, false sinon.
		/// </summary>
		public bool ConfirmationIsRight => ConfirmPassword == Password;

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
			Age = "0";
		}

		/// <summary>
		/// Calcule si une demande de création de partie peut-être demandée.
		/// </summary>
		/// <returns><see langword="true"/>Si on peut lancer la demande de création de partie, <see langword="false"/> sinon.</returns>
		private bool CreationCanExecute()
		{
			return ConfirmationIsRight && Username != string.Empty && Age != "0" && Password != string.Empty;
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
			core.TryCreateProfile(Username, Password, Firstname, Lastname, int.Parse(Age));
        }
    }
}
