using Client.ihm_main.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ihm_main.ViewModel
{
    class MainWindowViewModel
    {
        private ICommand openWindow;
        public ICommand OpenWindow
        {
            get => openWindow;
            set
            {
                openWindow = value;
            }
        }

        private SecondWindowView secondWindow;

        public MainWindowViewModel()
        {
            this.openWindow = new RelayCommand(OpenSecondWindow, true);
        }

        private void OpenSecondWindow()
        {
            secondWindow = new SecondWindowView();
            secondWindow.DataContext = new SecondWindowViewModel();
            secondWindow.Show();
        }
    }
}
