using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
	internal class TitleBarViewModel
    {
        private readonly IhmMainCore core;

        public ICommand HomeCommand { get; set; }
        public ICommand ProfileCheckCommand { get; set; }
		public ICommand DisconnectCommand { get;set; }

         public TitleBarViewModel(IhmMainCore core)
        {
			ProfileCheckCommand = new RelayCommand(CheckProfil, true);
			HomeCommand = new RelayCommand(HomeClick, true);
			DisconnectCommand = new RelayCommand(Disconnection, true);

			this.core = core;
        }

		private void Disconnection()
		{
			core.Disconnect();
		}

		private void CheckProfil()
        {
			core.ShowProfilePage();
        }

        private void HomeClick()
        {
            core.BackToHomePage();
        }
    }
}
