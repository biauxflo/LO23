using Client.ihm_main.DTO;
using Client.ihm_main.ViewModels;
using Client.ihm_main.Views;
using Client.ihm_main.Views.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

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
        /// Page de connection
        /// </summary>
        private readonly Page connectionPage = new ConnectionView();

        internal void GameListUpdated(Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// View Model de la page de connexion
        /// </summary>
        private readonly ConnectionViewModel connectionViewModel = new ConnectionViewModel();

        /// <summary>
        /// Page d'acceuil une fois connecté.
        /// </summary>
        private readonly Page homePage = new HomeView();

        /// <summary>
        /// View Model de la page d'acceuil.
        /// </summary>
        private readonly HomeViewModel homeViewModel = new HomeViewModel();

        #endregion

        public IhmMainCore()
        {
            //Association des vues et de leur view model
            mainWindow.DataContext = mainWindowViewModel;
            connectionPage.DataContext = connectionViewModel;
            homePage.DataContext = homeViewModel;

            // Page active de la fenetre
            mainWindowViewModel.ActivePage = connectionPage;

            mainWindow.Show();
        }

        /// <summary>
        /// Informe l'utilisateur que la connexion a échoué.
        /// </summary>
        public void ConnectionFailed()
        {
            // TODO : Mettre en place le vrai mécanisme de connexion
            MessageBox.Show(mainWindow, "Erreur", "Connexion refusée", MessageBoxButton.OK);
        }

        /// <summary>
        /// Connecte l'utilisateur à l'application.
        /// </summary>
        /// <param name="username">Nom de l'utilisateur.</param>
        public void ConnectionSucceed(string username)
        {
            // TODO : Mettre en place le vrai mécanisme de connexion
            MessageBox.Show(mainWindow, "OK", $"Connexion réussie : Bonjour {username}", MessageBoxButton.OK);
            mainWindowViewModel.ActivePage = homePage;
        }

        /// <summary>
        /// Met à jour la liste des parties en cours.
        /// </summary>
        /// <param name="game">Liste des parties en cours.</param>
        internal void GameListUpdated(string game)
        {
            // TODO : Passer à homeViewModel et creationViewModel la nouvelle liste
        }

        /// <summary>
        /// Lance la partie donnée.
        /// </summary>
        /// <param name="game">Partie à afficher.</param>
        internal void GameLaunched(string game)
        {
            // TODO : Cacher la main window
            // TODO : Appel IHM-Game
        }
    }

}
