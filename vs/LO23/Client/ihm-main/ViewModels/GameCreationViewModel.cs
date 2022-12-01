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
		private Game gameInCreation = new Game();
        public Game Partie1
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

            Games.Add(new Game("partie1", 2, 2500, true, true));

            this.core = core;
        }

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

        private void OnCancelClick()
        {
			Partie1 = null;
            core.BackToHomePage();
        }

        private void OnCreationClick()
        {
            // TODO : Mettre en place l'appel au module Data pour recuperer les parties en cours
            string messageBoxText = string.Empty;
            MessageBoxImage icon = MessageBoxImage.None;
            string windowCaption = "Résultat de création de partie";
            MessageBoxButton button = MessageBoxButton.OK;

            if(Games
                .Any(game => game.name == gameInCreation.name))
            {
                messageBoxText = "Nom de Partie déjà existant";
                icon = MessageBoxImage.Error;
            }
            else
            {
                messageBoxText = "Création réussie";
                icon = MessageBoxImage.Information;
            }

            MessageBox.Show(messageBoxText, windowCaption, button, icon, MessageBoxResult.OK);
        }

    }

}
