using Client.ihm_main.ViewModels;
using Client.ihm_main.Views;
using Client.ihm_main.Views.Pages;
using Shared.data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Shared.interfaces;
using System;

namespace Client.ihm_main
{
	internal class IhmMainCore
	{
		/// <summary>
		/// Vue principale de l'application.
		/// </summary>
		private readonly MainWindow mainWindow = new MainWindow();

		private readonly MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

		internal DataToMain dataToMain;

		#region Declaration of pages and view models

		/// <summary>
		/// Page de connection.
		/// </summary>
		private readonly Page connectionPage = new ConnectionView();

		/// <summary>
		/// View Model de la page de connexion.
		/// </summary>
		private readonly ConnectionViewModel connectionViewModel;

		/// <summary>
		/// Page de creation de Partie.
		/// </summary>
		private readonly Page gameCreationPage = new GameCreationView();

		/// <summary>
		/// View Model de creation de Partie.
		/// </summary>
		private readonly GameCreationViewModel gameCreationViewModel;

		/// <summary>
		/// Page d'acceuil une fois connecté.
		/// </summary>
		private readonly Page homePage = new HomeView();

		/// <summary>
		/// View Model de la page d'acceuil.
		/// </summary>
		private readonly HomeViewModel homeViewModel;

		/// <summary>
		/// Page de creation de profil.
		/// </summary>
		private Page profileCreationPage = new ProfilCreationView();

		/// <summary>
		/// View Model de la page de création de profil.
		/// </summary>
		private ProfilCreationViewModel profilCreationViewModel;

		/// <summary>
		/// Barre de titre de l'application.
		/// </summary>
		private Page titleBar = new TitleBarView();

		/// <summary>
		/// View Model de la barre de titre de l'application.
		/// </summary>
		private readonly TitleBarViewModel titleBarViewModel;

        /// <summary>
        /// Page de chargement de sauvegardes.
        /// </summary>
        private readonly Page loadingSavePage = new LoadingView();

        /// <summary>
        /// View Model de la page de chargement de sauvegardes.
        /// </summary>
        private readonly LoadingSaveViewModel loadingSaveViewModel;

		/// <summary>
		/// Page de modification de profil.
		/// </summary>
		private readonly Page profileModificationView = new ProfilModificationView();

		/// <summary>
		/// View Model de la page de modification de profil.
		/// </summary>
		private readonly ProfilModificationViewModel profilModificationViewModel;

		/// <summary>
		/// Menu des contacts en ligne.
		/// </summary>
		private Page contactsMenu = new ContactsView();

		/// <summary>
		/// 
		/// </summary>
		private readonly ContactsViewModel contactsMenuViewModel;

        #endregion

		#region Used Interfaces 

		internal IMainToGame mainToGame;

		internal IMainToDataClient mainToData;

		#endregion

		public IhmMainCore()
		{
			// Instanciation of views models.
			gameCreationViewModel = new GameCreationViewModel(this);
			connectionViewModel = new ConnectionViewModel(this);
			homeViewModel = new HomeViewModel(this);
			profilCreationViewModel = new ProfilCreationViewModel(this);
			titleBarViewModel = new TitleBarViewModel(this);
			loadingSaveViewModel = new LoadingSaveViewModel(this);
			profilModificationViewModel = new ProfilModificationViewModel(this);
			contactsMenuViewModel = new ContactsViewModel();

			// Instanciation of shared interfaces.
			dataToMain = new DataToMain(this);

			// Link views and view model.
			mainWindow.DataContext = mainWindowViewModel;
			connectionPage.DataContext = connectionViewModel;
			gameCreationPage.DataContext = gameCreationViewModel;
			homePage.DataContext = homeViewModel;
			profileCreationPage.DataContext = profilCreationViewModel;
			titleBar.DataContext = titleBarViewModel;
			loadingSavePage.DataContext = loadingSaveViewModel;
			profileModificationView.DataContext = profilModificationViewModel;
			contactsMenu.DataContext = contactsMenuViewModel;

			// Active page on the window
			mainWindowViewModel.ActivePage = connectionPage;
			mainWindowViewModel.TitleBar = titleBar;
			mainWindowViewModel.ContactsMenu = contactsMenu;
		}


		/// <summary>
		/// Renvoie à la page de connexion.
		/// </summary>
		internal void Disconnect()
		{
			mainWindowViewModel.ActivePage = connectionPage;
			mainWindowViewModel.IsTitleBarVisible = false;
			titleBarViewModel.Username = string.Empty;
			mainWindowViewModel.IsContactsMenuVisible = false;
		}

		/// <summary>
		/// Met la page active sur la page de création de partie.
		/// </summary>
		internal void OpenGameCreationPage()
		{
			mainWindowViewModel.ActivePage = gameCreationPage;
			mainWindowViewModel.IsContactsMenuVisible = false;
		}

		/// <summary>
		/// Met la page active sur la page de création de profil.
		/// </summary>
		internal void ShowProfileCreationPage()
		{
			mainWindowViewModel.ActivePage = profileCreationPage;
		}

		/// <summary>
		/// Met la page active sur la page de chargement des sauvegardes.
		/// </summary>
		internal void OpenLoadingSavePage()
		{
			mainWindowViewModel.ActivePage = loadingSavePage;
			mainWindowViewModel.IsContactsMenuVisible = false;
		}

		/// <summary>
		/// Met la page active sur la page d'acceuil.
		/// </summary>
		internal void BackToHomePage()
		{
			mainWindowViewModel.ActivePage = homePage;
			mainWindowViewModel.IsContactsMenuVisible = true;
		}

		/// <summary>
		/// Informe l'utilisateur que la connexion a échoué.
		/// </summary>
		internal void ConnectionFailed(string error)
		{
			connectionViewModel.Reset();
			MessageBox.Show(mainWindow, error, "Erreur", MessageBoxButton.OK);
		}

		/// <summary>
		/// Connecte l'utilisateur à l'application.
		/// </summary>
		/// <param name="username">Nom de l'utilisateur.</param>
		internal void ConnectionSucceed(LightUser user)
		{
			homeViewModel.ConnectedUser = user;
			connectionViewModel.Reset();
			mainWindowViewModel.ActivePage = homePage;
			mainWindowViewModel.IsTitleBarVisible = true;
			titleBarViewModel.Username = user.username;
			mainWindowViewModel.IsContactsMenuVisible = true;
		}

		/// <summary>
		/// Informe l'utilisateur que la connexion a échoué.
		/// </summary>
		internal void UserInfoGetFailed(string error)
		{
			MessageBox.Show(mainWindow, error, "Erreur", MessageBoxButton.OK);
		}

		/// <summary>
		/// Informe de la récupération des informations de l'utilisateur et affiche la page de modification de profil.
		/// </summary>
		/// <param name="user">Utilisateur connecté.</param>
		internal void UserInfoGetSucceed(User user)
		{
			profilModificationViewModel.ConnectedUser = user;
			mainWindowViewModel.ActivePage = profileModificationView;
		}

		/// <summary>
		/// Indique que la partie n'a pas pu être créée.
		/// </summary>
		/// <param name="error">Raison pour laquelle la partie n'a pas pu être créée.</param>
		internal void GameCreationFailed(string error)
		{
			MessageBox.Show(mainWindow, error, "Partie non créée", MessageBoxButton.OK);
		}

		/// <summary>
		/// Met à jour la liste des parties en cours.
		/// </summary>
		/// <param name="game">Liste des parties en cours.</param>
		internal void GameListUpdated(List<LightGame> games)
		{
			ObservableCollection<LightGame> GameCollection = new ObservableCollection<LightGame>(games);
			homeViewModel.Games = GameCollection;
		}

		/// <summary>
		/// Effectue les actions suite à une création de profil réussie.
		/// </summary>
		internal void ProfileCreationSuceed()
		{
			MessageBox.Show(mainWindow, "Profil créé,\nVous pouvez vous connecter", "Profil créé", MessageBoxButton.OK);
			profilCreationViewModel.Reset();
			mainWindowViewModel.ActivePage = connectionPage;
		}

		/// <summary>
		/// Effectue les actions suite à une création de profil échouée.
		/// </summary>
		/// <param name="error">Erreur ayant empeché la création de profil.</param>
		internal void ProfileCreationFailed(string error)
		{
			MessageBox.Show(mainWindow, error, "Profil non créé", MessageBoxButton.OK);
			profilCreationViewModel.Reset();
		}

		/// <summary>
		/// Lance la partie donnée.
		/// </summary>
		/// <param name="game">Partie à afficher.</param>
		internal void GameLaunched(Game game)
		{
			BackToHomePage();
			mainWindow.Hide();
			LaunchGame(game);
		}

		/// <summary>
		/// Lance la partie donnée.
		/// </summary>
		/// <param name="game">Partie à lancer.</param>
		internal void LaunchGame(Game game)
		{
			mainToGame.LaunchGame(game);
		}

		/// <summary>
		/// Demande la création d'une partie.
		/// </summary>
		/// <param name="gameInCreation">Options de la partie à créer.</param>
		internal void TryCreateNewGame(GameOptions gameInCreation)
		{
			mainToData.createNewGame(gameInCreation);
		}

		/// <summary>
		/// Demande l'authentification du couple utilisateur/mot de passe donné.
		/// </summary>
		/// <param name="username">Nom d'utilisateur à tester.</param>
		/// <param name="password">Mot de passe à tester.</param>
		internal void TryAuthenticate(string username, string password)
		{
			mainToData.authenticate(username, password);
		}

		/// <summary>
		/// Demande la connexion à une partie.
		/// </summary>
		/// <param name="id">Id de la partie à laquelle on veut se connecter.</param>
		/// <param name="user">Utilisateur voulant se connecter à la partie.</param>
		internal void TryJoinGame(Guid id, LightUser user)
		{
			mainToData.playGame(id, user);
		}

		/// <summary>
		/// Demande la connexion à une partie en tant que spectateur.
		/// </summary>
		/// <param name="id">Id de la partie à laquelle on veut se connecter.</param>
		/// <param name="user">Utilisateur voulant se connecter à la partie.</param>
		internal void TrySpectateGame(Guid id, LightUser user)
		{
			// TODO : lien Data
			MessageBox.Show(mainWindow, "La fonction n'est pas encore implémantée", "W.I.P", MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Demande la création d'un nouveau profil avec les arguments donnés.
		/// </summary>
		/// <param name="username">Nom d'utilisateur du profil à créer.</param>
		/// <param name="password">Mot de passe du profil à créer.</param>
		/// <param name="firstname">Prénom du profil à créer.</param>
		/// <param name="lastname">Nom de famille du profil à créer.</param>
		/// <param name="age">Age du profil à créer.</param>
		internal void TryCreateProfile(string username, string password, string firstname, string lastname, int age)
		{
			mainToData.registerProfile(username, password, firstname, lastname, age);
		}

		/// <summary>
		/// Demande une modification de profil à data
		/// </summary>
		/// <param name="newUsername">Nouveau nom d'utilisateur ou <see langword="null"/></param>
		/// <param name="newPassword">Nouveau mot de passe ou <see langword="null"/></param>
		/// <param name="id">Id de l'utilisateur connecté</param>
		internal void TryModifyProfile(string newUsername, string newPassword, Guid id)
		{
			// TODO : call data
			MessageBox.Show(mainWindow, "La fonction n'est pas encore implémantée", "W.I.P", MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Demande les informations sur l'utilisateur connecté à Data.
		/// </summary>
		internal void TryGetUserInfo()
		{
			LightUser connectedUser = homeViewModel.ConnectedUser;
			// TODO : call data for User from LightUser
			MessageBox.Show(mainWindow, "La fonction n'est pas encore implémantée", "W.I.P", MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Lance le module IHM-Main.
		/// </summary>
		internal void Run()
		{
			mainWindow.Show();
		}
	}
}
