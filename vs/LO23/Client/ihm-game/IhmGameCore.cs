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

			// display the correct buttons at the initialisation of the game and all buttons are disable by default
			((GameView)this.gamePage).BT_defausser.Visibility = Visibility.Hidden;
			((GameView)this.gamePage).BT_garderMain.Visibility = Visibility.Hidden;
			if(game.status == GameStatus.running)
			{
				if(game.currentPhase.typePhase == TypePhase.bet1 || game.currentPhase.typePhase == TypePhase.bet2)
				{
					((GameView)this.gamePage).BT_doubler.Visibility = Visibility.Visible;
					((GameView)this.gamePage).IntTB_Bet.Visibility = Visibility.Visible;
					((GameView)this.gamePage).BT_egaler.Visibility = Visibility.Visible;
					((GameView)this.gamePage).TBlock_call.Visibility = Visibility.Visible;
					((GameView)this.gamePage).BT_seCoucher.Visibility = Visibility.Visible;
					if(this.WhoAmI().id == game.players[game.currentPlayerIndex].id)
					{
						((GameView)this.gamePage).BT_doubler.IsEnabled = true;
						((GameView)this.gamePage).IntTB_Bet.IsEnabled = true;
						((GameView)this.gamePage).BT_egaler.IsEnabled = true;
						((GameView)this.gamePage).BT_seCoucher.IsEnabled = true;
					}
					else
					{
						((GameView)this.gamePage).BT_doubler.IsEnabled = false;
						((GameView)this.gamePage).IntTB_Bet.IsEnabled = false;
						((GameView)this.gamePage).BT_egaler.IsEnabled = false;
						((GameView)this.gamePage).BT_seCoucher.IsEnabled = false;
					}
				}
				else
				{
					if(game.currentPhase.typePhase == TypePhase.draw)// = échanger
					{
						((GameView)this.gamePage).BT_defausser.Visibility = Visibility.Visible;
						((GameView)this.gamePage).BT_garderMain.Visibility = Visibility.Visible;

						((GameView)this.gamePage).BT_doubler.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).IntTB_Bet.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).BT_egaler.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).TBlock_call.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).BT_seCoucher.Visibility = Visibility.Hidden;

						if(this.WhoAmI().id == game.players[game.currentPlayerIndex].id)
						{
							((GameView)this.gamePage).BT_defausser.IsEnabled = true;
							((GameView)this.gamePage).BT_garderMain.IsEnabled = true;
						}
						else
						{
							((GameView)this.gamePage).BT_defausser.IsEnabled = false;
							((GameView)this.gamePage).BT_garderMain.IsEnabled = false;
						}

					}

					else
					{
						if(game.currentPhase.typePhase == TypePhase.reveal)
						{

							((GameView)this.gamePage).BT_defausser.Visibility = Visibility.Hidden;
							((GameView)this.gamePage).BT_garderMain.Visibility = Visibility.Hidden;

							((GameView)this.gamePage).BT_doubler.Visibility = Visibility.Hidden;
							((GameView)this.gamePage).IntTB_Bet.Visibility = Visibility.Hidden;
							((GameView)this.gamePage).BT_egaler.Visibility = Visibility.Hidden;
							((GameView)this.gamePage).TBlock_call.Visibility = Visibility.Hidden;
							((GameView)this.gamePage).BT_seCoucher.Visibility = Visibility.Hidden;

						}
					}
				}
			}
			else
			{
				((GameView)this.gamePage).BT_doubler.IsEnabled = false;
				((GameView)this.gamePage).BT_egaler.IsEnabled = false;
				((GameView)this.gamePage).BT_seCoucher.IsEnabled = false;
				((GameView)this.gamePage).TBlock_call.IsEnabled = false;
			}
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
			gameToData.LeaveGame();
			gameWindow.Hide();
		}

		internal void UpdateMessageDisplay(ChatMessage message)
		{

		}

		/** TODO : delete when we get actual game from data */
		/**internal Game testNewGame()
		{
			GameOptions gameOptions = new GameOptions("name", 102, true, true, 5, 4, 1, 10);
			Guid guid = Guid.NewGuid();
			Game game = new Game(guid, gameOptions);
			game.pot = 250;
			return game;

		}*/
		/** ------- */
		/// <summary>
		/// Mise à jour de l'affichage du jeu 
		/// </summary>
		internal void UpdateGameDisplay(Game game)
		{
			this.gameViewModel.Game = game;
			Player player = game.players.Find(x => x.id == this.WhoAmI().id);
			gameViewModel.UpdateGame(game);
			if(player != null)
			{
				if(player.isFolded)
				{
					((GameView)this.gamePage).Card1.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).Card2.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).Card3.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).Card4.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).Card5.Visibility = Visibility.Hidden;
				}
				else
				{
					((GameView)this.gamePage).Card1.Visibility = Visibility.Visible;
					((GameView)this.gamePage).Card2.Visibility = Visibility.Visible;
					((GameView)this.gamePage).Card3.Visibility = Visibility.Visible;
					((GameView)this.gamePage).Card4.Visibility = Visibility.Visible;
					((GameView)this.gamePage).Card5.Visibility = Visibility.Visible;
				}
			}

			if(game.currentPhase.typePhase == TypePhase.bet1 || game.currentPhase.typePhase == TypePhase.bet2)
			{
				((GameView)this.gamePage).BT_doubler.Visibility = Visibility.Visible;
				((GameView)this.gamePage).IntTB_Bet.Visibility = Visibility.Visible;
				((GameView)this.gamePage).BT_egaler.Visibility = Visibility.Visible;
				((GameView)this.gamePage).TBlock_call.Visibility = Visibility.Visible;
				((GameView)this.gamePage).BT_seCoucher.Visibility = Visibility.Visible;

				((GameView)this.gamePage).BT_defausser.Visibility = Visibility.Hidden;
				((GameView)this.gamePage).BT_garderMain.Visibility = Visibility.Hidden;

				if(this.WhoAmI().id == game.players[game.currentPlayerIndex].id)
				{
					((GameView)this.gamePage).BT_doubler.IsEnabled = true;
					((GameView)this.gamePage).IntTB_Bet.IsEnabled = true;
					((GameView)this.gamePage).BT_egaler.IsEnabled = true;
					((GameView)this.gamePage).BT_seCoucher.IsEnabled = true;
				}
				else
				{
					((GameView)this.gamePage).BT_doubler.IsEnabled = false;
					((GameView)this.gamePage).IntTB_Bet.IsEnabled = false;
					((GameView)this.gamePage).BT_egaler.IsEnabled = false;
					((GameView)this.gamePage).BT_seCoucher.IsEnabled = false;
				}

				if(game.players.Count(p => !p.isFolded) <= 1)
				{
					((GameView)this.gamePage).BT_seCoucher.IsEnabled = false;
				}
			}

			else
			{
				if(game.currentPhase.typePhase == TypePhase.draw)// = échanger
				{
					((GameView)this.gamePage).BT_defausser.Visibility = Visibility.Visible;
					((GameView)this.gamePage).BT_garderMain.Visibility = Visibility.Visible;

					((GameView)this.gamePage).BT_doubler.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).IntTB_Bet.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).BT_egaler.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).TBlock_call.Visibility = Visibility.Hidden;
					((GameView)this.gamePage).BT_seCoucher.Visibility = Visibility.Hidden;

					if(this.WhoAmI().id == game.players[game.currentPlayerIndex].id)
					{
						((GameView)this.gamePage).BT_defausser.IsEnabled = true;
						((GameView)this.gamePage).BT_garderMain.IsEnabled = true;
					}
					else
					{
						((GameView)this.gamePage).BT_defausser.IsEnabled = false;
						((GameView)this.gamePage).BT_garderMain.IsEnabled = false;
					}

				}

				else
				{
					if(game.currentPhase.typePhase == TypePhase.reveal)
					{

						((GameView)this.gamePage).BT_defausser.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).BT_garderMain.Visibility = Visibility.Hidden;

						((GameView)this.gamePage).BT_doubler.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).IntTB_Bet.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).BT_egaler.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).TBlock_call.Visibility = Visibility.Hidden;
						((GameView)this.gamePage).BT_seCoucher.Visibility = Visibility.Hidden;

					}
				}

			}
		}

		internal void StartReplayDisplay(Game game)
		{

		}

		/// Appel à data pour demander une action (call/rise/fold/allin)
		/// <summary>
		internal void PlayRound(GameAction a)
		{
			this.gameToData.PlayRound(a);
		}
		/// </summary>
		// Call to data to get the id of the current user
		internal LightUser WhoAmI()
		{
			return this.gameToData.whoAmi();

		}

	}
}
		
