using System;
using Shared.data;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ihm_main.ViewModels
{
    internal class GameCreationViewModel : INotifyPropertyChanged
    {
        private readonly IhmMainCore core;

        /// <summary>
		/// Partie en cours de création.
		/// </summary>
		private GameOptions gameInCreation = new GameOptions(String.Empty, 2000, true, true, 100, 4, 0, 10);
        public GameOptions GameInCreation
        {
            get => gameInCreation;
            set
            {
                gameInCreation = value;
				OnPropertyChanged();
            }
        }

		public ObservableCollection<Game> Games { get; set; }

        /// <summary>
        /// Commande liée au bouton de creation.
        /// </summary>
        public ICommand CreationCommand { get; set; }

        /// <summary>
        /// Commande liée au bouton "Annuler".
        /// </summary>
        public ICommand CancelCommand { get; set; }


		public event PropertyChangedEventHandler PropertyChanged;

        public GameCreationViewModel(IhmMainCore core)
        {
            Games = new ObservableCollection<Game>();

            CreationCommand = new RelayCommand(OnCreationClick, true);
            CancelCommand = new RelayCommand(OnCancelClick, true);

            this.core = core;
        }

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

        private void OnCancelClick()
    {
			GameInCreation = new GameOptions(String.Empty, 2000, true, true, 100, 4, 0, 10);
            core.BackToHomePage();
        }

        private void OnCreationClick()
        {
			core.CreateNewGame(GameInCreation);
        }
    }
}
