using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
	internal class TitleBarViewModel : INotifyPropertyChanged
	{
        private readonly IhmMainCore core;

        public ICommand HomeCommand { get; set; }
        public ICommand ProfileCheckCommand { get; set; }
		public ICommand DisconnectCommand { get;set; }

		private string username;
		/// <summary>
		/// Nom de l'utilisateur connecté.
		/// </summary>
		public string Username
		{
			get => username;
			set
			{
				username = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

         public TitleBarViewModel(IhmMainCore core)
        {
			ProfileCheckCommand = new RelayCommand(CheckProfil, true);
			HomeCommand = new RelayCommand(HomeClick, true);
			DisconnectCommand = new RelayCommand(Disconnection, true);

			this.core = core;
        }

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void Disconnection()
		{
			core.Disconnect();
		}

		private void CheckProfil()
        {
			core.TryGetUserInfo();
        }

        private void HomeClick()
        {
            core.BackToHomePage();
        }
    }
}
