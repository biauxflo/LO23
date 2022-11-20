using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Client.data
{
    internal class DataCore
    {
        /// <summary>
        /// Vue principale de l'application.
        /// </summary>
        private readonly MainWindow mainWindow = new MainWindow();
        

        public DataCore()
        {
            //Association des vues et de leur view model
           
            Console.WriteLine("Running DataCore class");
        }

    }
}