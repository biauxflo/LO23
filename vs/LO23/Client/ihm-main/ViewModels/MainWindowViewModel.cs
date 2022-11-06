using System.Windows.Controls;

namespace Client.ihm_main.ViewModels
{
    internal class MainWindowViewModel
    {
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
            }
        }

        public MainWindowViewModel()
        {
        }
    }
}
