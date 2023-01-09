﻿using System;
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
