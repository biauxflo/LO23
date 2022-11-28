using Shared.data;
using System.Collections.Generic;

namespace Shared.interfaces
{
    public interface IDataToMain
    {
        /// <summary>
        /// Informe l'utilisateur que la connexion a échoué.
        /// </summary>
        void ConnectionFailed();

        /// <summary>
        /// Connecte l'utilisateur à l'application.
        /// </summary>
        /// <param name="username">Nom de l'utilisateur.</param>
        void ConnectionSucceed(User user);

        /// <summary>
        /// Met à jour la liste des parties rejoignable par l'utilisateur.
        /// </summary>
        /// <param name="game">Liste des parties rejoignables.</param>
        void GameListUpdated(List<Game> games);

        /// <summary>
        /// Lance une partie.
        /// </summary>
        /// <param name="game">Partie à lancer.</param>
        void GameLaunched(Game game);

        /// <summary>
        /// Indique qu'une demande de création de partie n'a pas pu être créée.
        /// </summary>
        /// <param name="error">Erreur lors de la création.</param>
        void GameCreationFailed(string error);
    }
}
