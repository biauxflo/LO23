using Client.ihm_main.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.ihm_main.ViewModels
{
    class MainWindowViewModel
    {
        /// <summary>
        /// Page a afficher au sein de la fenêtre
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
