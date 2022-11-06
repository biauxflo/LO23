using Client.ihm_main.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private bool secondWindowIsNotActive = true;
        public bool SecondWindowIsNotActive
        {
            get => secondWindowIsNotActive;
            set
            {
                secondWindowIsNotActive = value;
                NotifyPropertyChanged("SecondWindowIsNotActive");
            }
        }

        private SecondWindowView secondWindow;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            this.openWindow = new RelayCommand<object>(OpenSecondWindow, SecondWindowIsNotActive);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OpenSecondWindow(object view)
        {
            MainWindow win = (MainWindow)view;
            secondWindow = new SecondWindowView();
            secondWindow.DataContext = new SecondWindowViewModel(win);
            secondWindow.Show();
            win.WindowState = WindowState.Minimized;
        }
    }
}
