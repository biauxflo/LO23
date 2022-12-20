using Shared.data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.interfaces
{
    public interface IDataToMain
    {
        /// <summary>
        /// Informe l'utilisateur que la connexion a échoué.
        /// </summary>
        void ConnectionFailed(string error);

        /// <summary>
        /// Connecte l'utilisateur à l'application.
        /// </summary>
        /// <param name="username">Nom de l'utilisateur.</param>
        void ConnectionSucceed(LightUser user);

        /// <summary>
        /// Met à jour la liste des parties rejoignable par l'utilisateur.
        /// </summary>
        /// <param name="game">Liste des parties rejoignables.</param>
        void GameListUpdated(List<LightGame> games);

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

		/// <summary>
		/// Indique qu'une demande de création de profil a réussie.
		/// </summary>
		void ProfileCreationSucceed();

		/// <summary>
		/// Indique qu'une demande de création de profil a échouée.
		/// </summary>
		/// <param name="error">Erreur ayant fait échoué la création de profil.</param>
		void ProfileCreatioFailed(string error);

	}
}
