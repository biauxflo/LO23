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
    }
}
