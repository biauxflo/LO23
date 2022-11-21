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
        // TODO : Mettre les bons paramètres
        void ConnectionSucceed(string username);

        /// <summary>
        /// Met à jour la liste des parties rejoignable par l'utilisateur.
        /// </summary>
        /// <param name="game">Liste des parties rejoignables.</param>
        void GameListUpdated(string game);

        /// <summary>
        /// Lance une partie.
        /// </summary>
        /// <param name="game">Partie à lancer.</param>
        void GameLaunched(string game);
    }
}
