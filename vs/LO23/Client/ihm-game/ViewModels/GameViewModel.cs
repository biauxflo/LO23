using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;





namespace Client.ihm_game.ViewModels
{
    internal class GameViewModel
    {
        // pour ajouter un bouton, dans le xaml section bouton-> Command="{Binding Path=ParamCommand}"
        // exemple: <Button Name="BT_parameter"  Grid.Row="0" Grid.Column="0"  BorderThickness="0" Background="#a2aebb" Command="{Binding Path=ParamCommand}">
        public ICommand ParamCommand { get; set; }

        public GameViewModel()
        {
            ParamCommand = new RelayCommand(OnParamClick);
        }
        
        // fonction lié au bouton
        // mécanisme temporaire juste pour tester affichage d'une console de message
        private void OnParamClick()
        {
            // exemple de fenêtre warning 
            /* MessageBoxResult result = MessageBox.Show("message dans la fenêtre","nom de la fenêtre", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if(result == MessageBoxResult.OK)
             {

             } */
            MessageBox.Show("message dans la fenêtre", "nom de la fenêtre", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

    }
}
