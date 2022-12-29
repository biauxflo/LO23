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

		private void OnCallClick(object sender, RoutedEventArgs e)
		{
			/* TODO : delete when test is finished
			this.BT_egaler.IsEnabled = false;
			this.BT_doubler.IsEnabled = false;
			this.BT_seCoucher.IsEnabled = false;
			*/
		}

		private void OnFoldClick(object sender, RoutedEventArgs e)
		{
			/* TODO : delete when test is finished
			this.BT_egaler.IsEnabled = false;
			this.BT_doubler.IsEnabled = false;
			this.BT_seCoucher.IsEnabled = false;
			this.Card1.Visibility= Visibility.Hidden;
			this.Card2.Visibility = Visibility.Hidden;
			this.Card3.Visibility = Visibility.Hidden;
			this.Card4.Visibility = Visibility.Hidden;
			this.Card5.Visibility = Visibility.Hidden;
			*/
		}

		private void OnRaiseClick(object sender, RoutedEventArgs e)
		{
			/* TODO : delete when test is finished
			this.BT_egaler.IsEnabled = false;
			this.BT_doubler.IsEnabled = false;
			this.BT_seCoucher.IsEnabled = false;
			*/
		}

	}
}
