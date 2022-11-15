using Client.data;
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
        private DataCore dataCore;

        private void App_Startup(object sender, StartupEventArgs e)
        {
            dataCore = new DataCore();
            CommClient cli = new CommClient();
			cli.start("127.0.0.1", 10000);
			cli.announceUser("Bonjour");
        }
    }
}
