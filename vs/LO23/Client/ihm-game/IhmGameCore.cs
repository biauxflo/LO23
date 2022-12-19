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
			gameToData.SaveGame();
		}
		/// <summary>
		/// Appel à data pour quitter la partie
		/// Cache la fenêtre de jeu
		/// </summary>
		internal void GameEnded()
		{
			// A decommenter au moment de l'integration de la V2 avec data
			//gameToData.LeaveGame();
			gameWindow.Hide();
		}

		internal void UpdateMessageDisplay(ChatMessage message)
		{

		}

		/** TODO : delete when we get actual game from data */
		internal Game testNewGame()
		{
			GameOptions gameOptions = new GameOptions("name", 102, true, true, 5, 4, 1, 10);
			Guid guid = new Guid();
			Game game = new Game(guid, gameOptions);
			game.pot = 250;
			return game;

		}
		/** ------- */

		internal void UpdateGameDisplay(Game game)
		{
			this.gameViewModel.Game = game;
			if(this.WhoAmI() == game.currentPlayerIndex)
			{
				((GameView)this.gamePage).BT_doubler.IsEnabled = true;
				((GameView)this.gamePage).BT_egaler.IsEnabled = true;
				((GameView)this.gamePage).BT_seCoucher.IsEnabled = true;
			}
			


			/**  Test :
			Game gamme = testNewGame();
			this.gameViewModel.Game = gamme;
			*/
			//if data send gamePublic
			// replace Game attribute by game attribute
		}

		internal void StartReplayDisplay(Game game)
		{

		}

		/// Appel à data pour demander une action (call/rise/fold/allin)
		/// <summary>
		// TODO : replace parameter Action a with TypeAction t
		internal void PlayRound(Action a)
		{
			this.gameToData.PlayRound(a);
		}
		/// </summary>
		// Call to data to get the id of the current user
		internal int WhoAmI()
		{
			//TODO : define WhoAmI 
			return 1;//TODO : replace with : return this.gameToData.WhoAmI();

		}

		// <remarks>
		// To be called at the end of data function "initializeRound()"
		//the fold display is cancelled when a new round starts
		// </remarks>
		internal void NewRoundDisplay()
		{
			((GameView)this.gamePage).Card1.Visibility = Visibility.Visible;
			((GameView)this.gamePage).Card2.Visibility = Visibility.Visible;
			((GameView)this.gamePage).Card3.Visibility = Visibility.Visible;
			((GameView)this.gamePage).Visibility = Visibility.Visible;
			((GameView)this.gamePage).Visibility = Visibility.Visible;

		}
	}
}
		
