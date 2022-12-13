using Client.ihm_main.ViewModels;
using Client.ihm_main.Views;
using Client.ihm_main.Views.Pages;
using Shared.data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Shared.interfaces;
using System;

namespace Client.ihm_main
{
    internal class IhmMainCore
    {
        /// <summary>
        /// Vue principale de l'application.
        /// </summary>
        private readonly MainWindow mainWindow = new MainWindow();

        private readonly MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

		internal DataToMain dataToMain;


        #region Déclaration des pages et de leurs viewModels

        /// <summary>
        /// Page de connection.
        /// </summary>
        private readonly Page connectionPage = new ConnectionView();

		/// <summary>
		/// View Model de la page de connexion.
		/// </summary>
		private readonly ConnectionViewModel connectionViewModel;

        /// <summary>
        /// Page de creation de Partie.
        /// </summary>
        private readonly Page gameCreationPage = new GameCreationView();

        /// <summary>
        /// View Model de creation de Partie.
        /// </summary>
        private readonly GameCreationViewModel gameCreationViewModel;

        /// <summary>
        /// Page d'acceuil une fois connecté.
        /// </summary>
        private readonly Page homePage = new HomeView();

        /// <summary>
        /// View Model de la page d'acceuil.
        /// </summary>
        private readonly HomeViewModel homeViewModel;

		#endregion

		#region Interfaces des autres modules

		internal IMainToGame mainToGame;

		internal IMainToDataClient mainToData;

		#endregion

		public IhmMainCore()
        {
			// Instanciation des views models.
            gameCreationViewModel = new GameCreationViewModel(this);
			connectionViewModel = new ConnectionViewModel(this);
			homeViewModel = new HomeViewModel(this);

			// Instanciation des interfaces exposées.
			dataToMain = new DataToMain(this);

            // Association des vues et de leur view model.
            mainWindow.DataContext = mainWindowViewModel;
            connectionPage.DataContext = connectionViewModel;
            gameCreationPage.DataContext = gameCreationViewModel;
            homePage.DataContext = homeViewModel;

            // Page active de la fenetre.
            mainWindowViewModel.ActivePage = connectionPage;
        }

		/// <summary>
		/// Met la page active sur la page de création de partie.
		/// </summary>
		internal void OpenGameCreationPage()
        {
            mainWindowViewModel.ActivePage = gameCreationPage;
        }

        /// <summary>
        /// Met la page active sur la page d'acceuil.
        /// </summary>
        internal void BackToHomePage()
        {
            mainWindowViewModel.ActivePage = homePage;
        }

        /// <summary>
        /// Informe l'utilisateur que la connexion a échoué.
        /// </summary>
        internal void ConnectionFailed(string error)
        {
			connectionViewModel.Reset();
            MessageBox.Show(mainWindow, error, "Erreur", MessageBoxButton.OK);
        }

        /// <summary>
        /// Connecte l'utilisateur à l'application.
        /// </summary>
        /// <param name="username">Nom de l'utilisateur.</param>
        internal void ConnectionSucceed(LightUser user)
        {
			homeViewModel.ConnectedUser = user;
            mainWindowViewModel.ActivePage = homePage;
        }

        /// <summary>
        /// Indique que la partie n'a pas pu être créée.
        /// </summary>
        /// <param name="error">Raison pour laquelle la partie n'a pas pu être créée.</param>
        internal void GameCreationFailed(string error)
        {
            MessageBox.Show(mainWindow, error, "Partie non créée", MessageBoxButton.OK);
        }


        /// <summary>
        /// Met à jour la liste des parties en cours.
        /// </summary>
        /// <param name="game">Liste des parties en cours.</param>
        internal void GameListUpdated(List<LightGame> games)
        {
			ObservableCollection<LightGame> GameCollection = new ObservableCollection<LightGame>(games);
			homeViewModel.Games = GameCollection;
        }

        /// <summary>
        /// Lance la partie donnée.
        /// </summary>
        /// <param name="game">Partie à afficher.</param>
        internal void GameLaunched(Game game)
        {
			// TODO : FIX
			//mainWindow.Hide
			LaunchGame(game);
        }

		internal void LaunchGame(Game game)
		{
			mainToGame.LaunchGame(game);
		}

		internal void CreateNewGame(GameOptions gameInCreation)
		{
			mainToData.createNewGame(gameInCreation);
		}

		internal void TryAuthenticate(string username, string password)
		{
			mainToData.authenticate(username, password);
		}

		internal void TryJoinGame(Guid id, LightUser user)
		{
			mainToData.playGame(id, user);
		}

		internal void Run()
		{
			// Afficher la fenêtre.
			mainWindow.Show();
		}
	}
}
