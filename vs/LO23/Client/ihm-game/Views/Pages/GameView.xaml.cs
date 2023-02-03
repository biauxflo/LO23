using System.Windows;
using System.Windows.Controls;

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

		private void OnCallClick(object sender, RoutedEventArgs e)
		{
		}

		private void OnFoldClick(object sender, RoutedEventArgs e)
		{
		}

		private void OnRaiseClick(object sender, RoutedEventArgs e)
		{
		}

		private void OnDefausserClick(object sender, RoutedEventArgs e)
		{
			this.BT_defausser.IsEnabled = false;
			this.BT_garderMain.IsEnabled = false;
		}

		private void OnGarderMainClick(object sender, RoutedEventArgs e)
		{
			this.BT_defausser.IsEnabled = false;
			this.BT_garderMain.IsEnabled = false;
		}
	}
}
