using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Client.ihm_game;

namespace Client
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IhmGameCore gameCore;

        private void App_Startup(object sender, StartupEventArgs e)
        {
            gameCore = new IhmGameCore();
        }
    }
}
