using System;
using Shared.data;

namespace Shared.interfaces
{
	public interface IDataToGame
	{
		/// <summary>
		/// Fonction qui met à jour le chat, TODO: vérifier que la paramètre est bon
		/// </summary>
		/// <param name="message">Ajout du message du chat</param>
		void UpdateMessageDisplay(ChatMessage message);

		/// <summary>
		/// Fonction appelé par DataClient qui met à jour la game, TODO: vérifier que la paramètre est bon
		/// </summary>
		/// <param name="game">game avec les modifications</param>
		void UpdateGameDisplay(Game game);

		/// <summary>
		/// Fonction appelé par DataClient qui permet de revoir la partie TODO: vérifier que la paramètre est bon
		/// 
		/// </summary>
		/// <param name="game">game avec les modifications</param>
		void StartReplayDisplay(Game game);

	}
}
