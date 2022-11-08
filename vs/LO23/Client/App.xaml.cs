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
        private ihmGameCore gameCore;

        /// <summary>
        /// Interface de communication de Data vers IHM-Main
        /// </summary>


        private void App_Startup(object sender, StartupEventArgs e)
        {
            gameCore = new ihmGameCore();
            
        }
    }
}
