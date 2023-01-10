using System;
using System.Collections.Generic;
using Shared.data;

namespace Shared.interfaces
{
	public interface ICommToDataClient
	{
		void setGamesAndUsers(List<LightUser> listUsers, List<LightGame> listGame );
		void setLoggedOut();
		void updateMessages(ChatMessage message);
		void updateGame(Game game);
		void setGame(Game game);
		void getProfileReturn(LightUser user);

		/// <summary>
		/// Ajoute l'utilisateur actuel du client � la partie.
		/// Permet de lancer la partie � la suite de la demande de cr�ation.
		/// </summary>
		/// <param name="game">Partie cr��e et � rejoindre.</param>
		void AddClientToThisGame(Game game);
		void removeUserFromListUsers(LightUser user);
		void addUserToListUsers(LightUser user);
		void removeGameFromListGames(LightGame game);
		void addGameToListGames(LightGame game);
	}
}

