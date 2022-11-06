using Client.ihm_main;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Controleur principal de l'IHM-Main
        /// </summary>
        private IhmMainCore mainCore;
        
        /// <summary>
        /// Interface de communication de Data vers IHM-Main
        /// </summary>
        private DataToMain dataToMain;

        void App_Startup(object sender, StartupEventArgs e)
        {
            mainCore = new IhmMainCore();
            dataToMain = new DataToMain(mainCore);
        }
    }
}
