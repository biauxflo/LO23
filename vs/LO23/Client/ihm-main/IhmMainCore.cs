using Client.ihm_main.Views;
using Client.ihm_main.Views.Pages;
using Client.ihm_main.ViewModels;
using System.Windows.Controls;
using System.Windows;

namespace Client.ihm_main
{
    class IhmMainCore
    {
        /// <summary>
        /// Vue principale de l'application
        /// </summary>
        private readonly MainWindow mainWindow = new MainWindow();

        private MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

#region Déclaration des pages et de leurs viewModels

        /// <summary>
        /// Page de connection
        /// </summary>
        private readonly Page connectionPage = new ConnectionView();

        /// <summary>
        /// View Model de la page de connexion
        /// </summary>
        private ConnectionViewModel connectionViewModel= new ConnectionViewModel();

#endregion



        public IhmMainCore()
        {
            //Association des vues et de leur view model
            mainWindow.DataContext = mainWindowViewModel;
            connectionPage.DataContext = connectionViewModel;

            mainWindowViewModel.ActivePage = connectionPage;

            mainWindow.Show();
        }

        /// <summary>
        /// Informe l'utilisateur que la connexion a échoué.
        /// </summary>
        public void ConnectionFailed()
        {
            // TODO : Mettre en place le vrai mécanisme de connexion
            MessageBox.Show((Window)mainWindow, "Erreur", "Connexion refusée", MessageBoxButton.OK);
        }

        /// <summary>
        /// Connecte l'utilisateur à l'application.
        /// </summary>
        /// <param name="username">Nom de l'utilisateur</param>
        public void ConnectionSucceed(string username)
        {
            // TODO : Mettre en place le vrai mécanisme de connexion
            MessageBox.Show((Window)mainWindow, "OK", $"Connexion réussie : Bonjour {username}" , MessageBoxButton.OK);
        }
    }
}
