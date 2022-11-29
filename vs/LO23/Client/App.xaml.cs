using Client.ihm_main;
using Client.data;
using Client.comm;
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
        /// <summary>
        /// Controleur principal de l'IHM-Main
        /// </summary>
        private IhmMainCore mainCore;

        /// <summary>
        /// Interface de communication de Data vers IHM-Main
        /// </summary>
        private DataToMain dataToMain;

        private IhmGameCore gameCore;
        private DataCore dataCore;

        private void App_Startup(object sender, StartupEventArgs e)
        {
			dataToMain = new DataToMain(mainCore);
            this.dataCore = new DataCore();
            CommClient cli = new CommClient();
			cli.Start("127.0.0.1", 10000);
			cli.DataToComm.announceUser(new Shared.data.User(
				1, "","", "", true, "Test", "Test", 12));

			gameCore = new IhmGameCore();

			mainCore = new IhmMainCore();
		}
    }
}
