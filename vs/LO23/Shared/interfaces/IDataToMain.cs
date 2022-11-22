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
        /// Indique qu'une demande de création de partie n'a pas pu être créée.
        /// </summary>
        /// <param name="error">Erreur lors de la création.</param>
        void GameCreationFailed(string error);
    }
}
