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
        private readonly GameCreationViewModel gameCreationViewModel = new GameCreationViewModel();

        #endregion

        public IhmMainCore()
        {
            //Association des vues et de leur view model
            mainWindow.DataContext = mainWindowViewModel;
            connectionPage.DataContext = connectionViewModel;
            gameCreationPage.DataContext = gameCreationViewModel;

            mainWindowViewModel.ActivePage = connectionPage;

            mainWindow.Show();
        }

        /// <summary>
        /// Informe l'utilisateur que la connexion a échoué.
        /// </summary>
        internal void ConnectionFailed()
        {
            // TODO : Mettre en place le vrai mécanisme de connexion
            MessageBox.Show(mainWindow, "Erreur", "Connexion refusée", MessageBoxButton.OK);
        }

        /// <summary>
        /// Connecte l'utilisateur à l'application.
        /// </summary>
        /// <param name="username">Nom de l'utilisateur.</param>
        internal void ConnectionSucceed(string username)
        {
            // TODO : Mettre en place le vrai mécanisme de connexion
            MessageBox.Show(mainWindow, "OK", $"Connexion réussie : Bonjour {username}", MessageBoxButton.OK);
        }
        
        /// <summary>
        /// Indique que la partie n'a pas pu être créée.
        /// </summary>
        /// <param name="error">Raison pour laquelle la partie n'a pas pu être créée.</param>
        internal void GameCreationFailed(string error)
        {
            MessageBox.Show(mainWindow, error, "Partie non créée", MessageBoxButton.OK);
        }

    }
}
