using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ihm_game.ViewModels;
using Client.ihm_game.Views;
using Client.ihm_game.Views.Pages;
using System.Windows;
using System.Windows.Controls;
using Shared.data;

namespace Client.ihm_game
{
    internal class IhmGameCore
    {

		// interface et page avec viewModel 
        private readonly GameWindow mainWindow = new GameWindow();

		private readonly MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();


		private readonly Page gamePage = new GameView();

		private readonly GameViewModel gameViewModel;


		public IhmGameCore()
		{
			gameViewModel = new GameViewModel(this);
			mainWindow.DataContext = mainWindowViewModel;
			gamePage.DataContext = gameViewModel;

			mainWindowViewModel.ActivePage = gamePage;
			mainWindow.Show();



		}
		internal void LaunchGame(Game game)
		{
			
		}



	}
}
