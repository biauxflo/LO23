using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.ihm_game.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView : Page
    {
        public GameView()
        {
            InitializeComponent();
        }

		private void OnClickCard(object sender, RoutedEventArgs e)
		{
			ToggleButton button = (ToggleButton)sender;
			MessageBox.Show(button.Name, "Test Button Card", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

		}
	}
}
