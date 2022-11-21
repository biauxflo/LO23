using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Client.ihm_main.ViewModels
{
    internal class MainWindowViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Page à afficher au sein de la fenêtre.
        /// </summary>
        private Page activePage;
        public Page ActivePage
        {
            get => activePage;
            set
            {
                activePage = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
