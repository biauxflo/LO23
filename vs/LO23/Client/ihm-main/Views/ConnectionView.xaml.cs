using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.ihm_main.Views
{
    /// <summary>
    /// Logique d'interaction pour ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : Page
    {
        private ConnectionViewModel ConnectionViewModel = new ConnectionViewModel();

        public ConnectionView()
        {
            InitializeComponent();
        }

        private void BT_Connecter_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = string.Empty;
            MessageBoxImage icon = MessageBoxImage.None;
            string windowCaption = "Résultat de connexion";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxResult result;
            if (ConnectionViewModel.user1 == ConnectionViewModel.user2)
            {
                messageBoxText = "Connexion réussie";
                icon = MessageBoxImage.Information;
            }
            else
            {
                messageBoxText = "Votre indentifiant ou votre mot de passe est incorrect";
                icon = MessageBoxImage.Error;
            }
            result = MessageBox.Show(messageBoxText, windowCaption, button, icon, MessageBoxResult.Yes);
        }
    }
}
