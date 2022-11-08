using Client.ihm_main.ViewModels;
using Client.ihm_main.Views;
using Client.ihm_main.Views.Pages;
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
        }
    }
}
