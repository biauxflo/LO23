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
		/// <summary>
		/// Page à afficher au sein de la fenêtre.
		/// </summary>
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
