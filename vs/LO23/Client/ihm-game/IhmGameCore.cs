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
		private GameWindow gameWindow;

		private GameWindowViewModel gameWindowViewModel;

		private Page gamePage = new GameView();

		private GameViewModel gameViewModel;

		internal MainToGame MainToGame;

		internal DataToGame DataToGame;


		public IhmGameCore()
		{
			gameWindow = new GameWindow();
			gameWindowViewModel = new GameWindowViewModel(this);
			gameWindow.DataContext = gameWindowViewModel;
			this.MainToGame = new MainToGame(this);
			this.DataToGame = new DataToGame(this);
		}

		internal void LaunchGame(Game game)
		{
			gameViewModel = new GameViewModel(this, game);
			gamePage.DataContext = gameViewModel;

			gameWindowViewModel.ActivePage = gamePage;
			gameWindow.Show();
		}

		internal void GameEnded()
		{
			gameWindow.Hide();
		}

		internal void UpdateMessageDisplay(ChatMessage message)
		{
			
		}

		internal void UpdateGameDisplay(Game game)
		{
		
		}

		internal void StartReplayDisplay(Game game)
		{

		}

	}
}
