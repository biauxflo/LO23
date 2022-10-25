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

        public SecondWindowViewModel()
        {
            this.closeWindow = new RelayCommand<object>(CloseSecondWindow, true);
        }

        private void CloseSecondWindow(object view)
        {
            SecondWindowView window = (SecondWindowView)view;
            window.Close();
        }
    }
}
