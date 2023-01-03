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
using Shared.helpers;
using Shared.data;
using Shared.constants;
using Shared.comm.messages;

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
        //private DataToMain dataToMain;

        private IhmGameCore gameCore;
        private DataClientCore dataCore;
		private CommClient commCore;


		//private MainToGame mainToGame;
        private void App_Startup(object sender, StartupEventArgs e)
        {
			gameCore = new IhmGameCore();
			mainCore = new IhmMainCore();
			dataCore = new DataClientCore();
            commCore = new CommClient();

			mainCore.mainToData = dataCore.implInterfaceForMain;
			commCore.CommToData = dataCore.implInterfaceForComm;
			dataCore.interfaceFromComm = commCore.DataToComm;
			dataCore.interfaceFromMain = mainCore.dataToMain;
			mainCore.mainToGame = gameCore.MainToGame;
			gameCore.gameToData = dataCore.implInterfaceForGame;
			dataCore.interfaceFromGame = gameCore.DataToGame;
			//FIXME: implement interfaces with IHM Main

			commCore.Start("127.0.0.1", 10000);

			mainCore.Run();
		}
	}
}
