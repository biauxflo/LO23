using Client.ihm_main.ViewModels;
using Client.ihm_main.Views;
using Client.ihm_main.Views.Pages;
using Shared.data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Shared.interfaces;

namespace Client.ihm_main
{
    internal class IhmMainCore
    {
        /// <summary>
        /// Vue principale de l'application.
        /// </summary>
        private readonly MainWindow mainWindow = new MainWindow();

        private readonly MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

        #region Déclaration des pages et de leurs viewModels

        /// <summary>
        /// Page de connection.
        /// </summary>
        private readonly Page connectionPage = new ConnectionView();

        /// <summary>
        /// View Model de la page de connexion.
        /// </summary>
        private readonly ConnectionViewModel connectionViewModel = new ConnectionViewModel();

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
		#endregion

        public IhmMainCore()
        {
            gameCreationViewModel = new GameCreationViewModel(this);
            homeViewModel = new HomeViewModel(this);

            //Association des vues et de leur view model
            mainWindow.DataContext = mainWindowViewModel;
            connectionPage.DataContext = connectionViewModel;
            gameCreationPage.DataContext = gameCreationViewModel;
            homePage.DataContext = homeViewModel;

            // Page active de la fenetre
            mainWindowViewModel.ActivePage = homePage;

            mainWindow.Show();
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
        internal void ConnectionFailed()
        {
			connectionViewModel.Reset();
            MessageBox.Show(mainWindow, "Erreur", "Connexion refusée", MessageBoxButton.OK);
        }

        /// <summary>
        /// Connecte l'utilisateur à l'application.
        /// </summary>
        /// <param name="username">Nom de l'utilisateur.</param>
        internal void ConnectionSucceed(User user)
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
        internal void GameListUpdated(List<Game> games)
        {
			ObservableCollection<Game> GameCollection = new ObservableCollection<Game>(games);
			homeViewModel.Games = GameCollection;
        }

        /// <summary>
        /// Lance la partie donnée.
        /// </summary>
        /// <param name="game">Partie à afficher.</param>
        internal void GameLaunched(Game game)
        {
			mainWindow.Hide();
			mainToGame.LaunchGame(game);
        }
    }
}
