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
using Shared.interfaces;

namespace Client.ihm_game
{
    internal class IhmGameCore
    {

		// interface et page avec viewModel 
		private GameWindow gameWindow;

		private GameWindowViewModel gameWindowViewModel;

		private Page gamePage = new GameView();

		private Page settingsPage = new SettingsView();

		private GameViewModel gameViewModel;

		private SettingsViewModel settingsViewModel;

		internal MainToGame MainToGame;

		internal IGameToData gameToData;

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

		/// <summary>
		/// Met la page active sur la page de jeu.
		/// </summary>
		internal void BackToGamePage()
		{
			gameWindowViewModel.ActivePage = gamePage;
		}

		/// <summary>
		/// Met la page active sur la page de paramètre.
		/// </summary>
		internal void GoToSettingsPage()
		{
			settingsViewModel = new SettingsViewModel(this);
			settingsPage.DataContext = settingsViewModel;
			gameWindowViewModel.ActivePage = settingsPage;
		}
		/// <summary>
		/// Appel à data pour sauvegarder la partie
		/// </summary>
		internal void SaveGame()
		{
			gameToData.saveGame();
		}
		/// <summary>
		/// Appel à data pour quitter la partie
		/// Cache la fenêtre de jeu
		/// </summary>
		internal void GameEnded()
		{
			gameToData.leaveGame();
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
