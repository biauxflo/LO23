
using System.Windows.Controls;

namespace Client.ihm_game.ViewModels
{
    internal class MainWindowViewModel
    {
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
