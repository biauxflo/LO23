using GalaSoft.MvvmLight.Command;
using Shared.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Client.ihm_main.ViewModels
{
	internal class LoadingSaveViewModel : INotifyPropertyChanged
	{
		private readonly IhmMainCore core;

		/// <summary>
		/// Liste des parties accessibles.
		/// </summary>
		public ObservableCollection<Game> Saves
		{
			get;
			set;
		}

		public ObservableCollection<Player> Players
		{
			get;
			set;
		}

		private Game selectedGame;


		public Game SelectedSave
		{
			get => selectedGame;
			set
			{
				selectedGame = value;
				Players = new ObservableCollection<Player>(value.players);
				OnPropertyChanged();
				DeleteCommand.RaiseCanExecuteChanged();
				LoadCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Commande liée au bouton de lancement d'une sauvegarde
		/// </summary>
		public RelayCommand LoadCommand
		{
			get; set;
		}

		/// <summary>
		/// Commande liée au bouton de supression.
		/// </summary>
		public RelayCommand DeleteCommand
		{
			get; set;
		}

		/// <summary>
		/// Commande liée au bouton "Retour".
		/// </summary>
		public ICommand BackCommand
		{
			get; set;
		}


		/// <summary>
		/// Commande liée au bouton de selection d'une sauvegarde.
		/// </summary>
		public ICommand SelectSave
		{
			get; set;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		
		public LoadingSaveViewModel(IhmMainCore core)
		{
			Saves = new ObservableCollection<Game>
			{
				new Game(Guid.NewGuid(), new GameOptions("Partie sauvegardée IHM-Main", 2000, true, true, 100, 8, 2, 1,20)),
				new Game(Guid.NewGuid(), new GameOptions("Partie sauvegardée IHM-Game", 2000, true, true, 100, 8, 2, 1,20)),
				new Game(Guid.NewGuid(), new GameOptions("Partie sauvegardée Communication", 2000, true, true, 100, 8, 2, 1,20)),
				new Game(Guid.NewGuid(), new GameOptions("Partie sauvegardée Data", 2000, true, true, 100, 8, 2, 1,20)),
			};

			Saves[0].players = new List<Player>
			{
				new Player(new LightUser(Guid.NewGuid(), "Gauthier", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Florestan", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Juliette", string.Empty),0),
			};

			Saves[1].players = new List<Player>
			{
				new Player(new LightUser(Guid.NewGuid(), "Eliott", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Ricardo", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Annaelle", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Marwane", string.Empty),0),
			};

			Saves[2].players = new List<Player>
			{
				new Player(new LightUser(Guid.NewGuid(), "Louis", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Talitha", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Anthony", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Corentin", string.Empty),0),
			};

			Saves[3].players = new List<Player>
			{
				new Player(new LightUser(Guid.NewGuid(), "Jules", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Nina", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Boris", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Sylvain", string.Empty),0),
				new Player(new LightUser(Guid.NewGuid(), "Pierre", string.Empty),0),
			};

			LoadCommand = new RelayCommand(LoadingGame, IsSaveSelected);
			BackCommand = new RelayCommand(OnBackClick, true);
			DeleteCommand = new RelayCommand(OnDeleteClick, IsSaveSelected);
			SelectSave = new RelayCommand<object>(OnSaveSelected, true);

			this.core = core;
		}

		/// <summary>
		/// créer la méthode OnPropertyChanges pour créer un événement.
		/// Le nom du membre appelant sera utilisé comme paramètre
		/// </summary>
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void OnSaveSelected(object obj)
		{
			if(obj != null && obj.GetType() == typeof(Game))
			{
				SelectedSave = (Game)obj;
			}
		}

		private bool IsSaveSelected()
		{
			return SelectedSave != null;
		}

		/// <summary>
		/// Lance la sauvegarde donnée en paramètre.
		/// </summary>
		/// <param name="obj">Sauvegarde à lancer.</param>
		private void LoadingGame()
		{
			// TODO : Call IHM-Game
		}

		private void OnBackClick()
		{
			core.BackToHomePage();
		}
		private void OnDeleteClick()
		{
			// TODO : Call Data
		}
	}
}

