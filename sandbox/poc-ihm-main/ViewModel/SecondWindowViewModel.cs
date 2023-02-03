using Client.ihm_main.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ihm_main.ViewModel
{
    class SecondWindowViewModel
    {
        private ICommand closeWindow;
        public ICommand CloseWindow
        {
            get => closeWindow;
            set
            {
                closeWindow = value;
            }
        }

        private ICommand onClosing;
        public ICommand OnClosing
        {
            get => onClosing;
            set
            {
                onClosing = value;
            }
        }

        private MainWindowViewModel parentViewModel;
        
        private MainWindow parentWindow;
        public MainWindow ParentWindow
        {
            get => parentWindow;
            set
            {
                parentWindow = value;
                parentViewModel = (MainWindowViewModel)parentWindow.DataContext;
            }
        }

        public SecondWindowViewModel(MainWindow win)
        {
            this.closeWindow = new RelayCommand<object>(CloseSecondWindow, true);
            this.onClosing = new RelayCommand(OnWindowClosing, true);
            ParentWindow = win;
            parentViewModel.SecondWindowIsNotActive = false;
        }

        private void CloseSecondWindow(object view)
        {
            SecondWindowView window = (SecondWindowView)view;
            window.Close();
            OnWindowClosing();
        }

        private void OnWindowClosing()
        {
            parentWindow.WindowState = WindowState.Normal;
            parentViewModel.SecondWindowIsNotActive = true;
        }
    }
}
