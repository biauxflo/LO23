using Client.ihm_main.Views.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Client.ihm_main.ViewModels
{
	/// <summary>
	/// Classe <c>MainWindowViewModel</c> modélise la page principale et implémente INotifyPropertyChanged
	/// </summary>
	internal class MainWindowViewModel : INotifyPropertyChanged
    {
		/// <summary>
		/// Déclarer l'événement
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Page à afficher au sein de la fenêtre.
        /// </summary>
        private Page activePage;

		/// <summary>
		/// Page à afficher au sein de la fenêtre.
		/// </summary>
		public Page ActivePage
        {
            get => activePage;
            set
            {
                activePage = value;
                OnPropertyChanged();
            }
        }

		private bool isTitleBarVisible;
		/// <summary>
		/// <see langword="true"/> si la barre de titre doit être affichée, <see langword="false"/> sinon.
		/// </summary>
		public bool IsTitleBarVisible
		{
			get => isTitleBarVisible;
			set
			{
				isTitleBarVisible = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Barre de titre de l'application.
		/// </summary>
        public Page TitleBar { get; set; }

		private bool isContactsMenuVisible;
		/// <summary>
		/// <see langword="true"/> si la barre des contacts doit être affichée, <see langword="false"/> sinon.
		/// </summary>
		public bool IsContactsMenuVisible
		{
			get => isContactsMenuVisible;
			set
			{
				isContactsMenuVisible = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Barre des contacts de l'application.
		/// </summary>
		public Page ContactsMenu
		{
			get; set;
		}

		public MainWindowViewModel()
        {
        }

		/// <summary>
		/// créer la méthode OnPropertyChanges pour créer un événement.
		/// Le nom du membre appelant sera utilisé comme paramètre
		/// </summary>
		protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
