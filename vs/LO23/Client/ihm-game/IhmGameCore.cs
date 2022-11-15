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

namespace Client.ihm_game
{
    internal class IhmGameCore
    {
        private readonly MainWindow mainWindow = new MainWindow();


        private readonly MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();


        private readonly Page gamePage = new GameView();

        private readonly GameViewModel gameViewModel = new GameViewModel();


        public IhmGameCore()
        {
            mainWindow.DataContext = mainWindowViewModel;
            gamePage.DataContext = gameViewModel;

            mainWindowViewModel.ActivePage = gamePage;

            mainWindow.Show();

        }


    }
}
