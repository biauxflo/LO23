
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ihm_game.ViewModels
{
	internal class GameWindowViewModel : INotifyPropertyChanged
    {
        private Page activePage;

		public event PropertyChangedEventHandler PropertyChanged;

		private readonly IhmGameCore core;

		/// <summary>
		/// Property qui prend comme valeur la page active (GameView ou SettingsView)
		/// </summary>
		public Page ActivePage
        {
            get => activePage;
            set
            {
                activePage = value;
				OnPropertyChanged();
			}
        }

		public ICommand Closing
		{
			get;set;
		}

        public GameWindowViewModel(IhmGameCore core)
        {
			this.core = core;
			Closing = new RelayCommand<CancelEventArgs>(OnWindowClosing);
        }

		private void OnWindowClosing(CancelEventArgs e)
		{
			core.GameEnded();
			e.Cancel = true;
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
