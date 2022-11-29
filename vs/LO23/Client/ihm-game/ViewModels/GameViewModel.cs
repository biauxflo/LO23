using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.data;
using System.Collections.ObjectModel;





namespace Client.ihm_game.ViewModels
{
	internal class GameViewModel
	{
		// pour ajouter un bouton, dans le xaml section bouton-> Command="{Binding Path=ParamCommand}"
		// exemple: <Button Name="BT_parameter"  Grid.Row="0" Grid.Column="0"  BorderThickness="0" Background="#a2aebb" Command="{Binding Path=ParamCommand}">
		public ICommand ParamCommand
		{
			get; set;
		}

		public ICommand FoldCommand
		{
			get; set;
		}

		public ICommand CallCommand
		{
			get; set;
		}

		public ICommand RaiseCommand
		{
			get; set;
		}

		public Game game
		{
			get; set;
		}

		//private readonly IhmGameCore core;

		public GameViewModel(IhmGameCore core) 
		{
			//this.core = core;
			ParamCommand = new RelayCommand(OnParamClick);

			FoldCommand = new RelayCommand(OnFoldClick);

			CallCommand = new RelayCommand(OnCallClick);

			RaiseCommand = new RelayCommand(OnRaiseClick);



			game = new Game(1, 2);
			game.pot = 200;
			Display();
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

		private void OnFoldClick()
		{
			MessageBox.Show("bouton fold", "bouton fold", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
		}

		private void OnCallClick()
		{
			MessageBox.Show("bouton call", "bouton call", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
		}

		private void OnRaiseClick()
		{
			MessageBox.Show("bouton raise", "bouton raise", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
		}
		private void Display()
		{
			//Fonctions à remplacer par les fonctions qui seront implémenter dans IHMGameCallsData

		}
	}
}
