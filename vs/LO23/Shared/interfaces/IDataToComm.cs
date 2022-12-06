using System;
using Shared.data;
namespace Shared.interfaces
{
	public interface IDataToComm
	{
		/// <summary>
		///
		/// </summary>
		void announceUser(LightUser user);

		/// <summary>
		///
		/// </summary>
		void unannounce(Guid userId);

		/// <summary>
		///
		/// </summary>
		void sendMessage(ChatMessage chatMsg);

		/// <summary>
		///
		/// </summary>
		void requestStopGame(Guid gameId, Guid playerId);

		/// <summary>
		///
		/// </summary>
		void requestLeaveGame(Guid gameId, Guid playerId);

		/// <summary>
		///
		/// </summary>
		void requestPlayRound(GameAction gameAction, Guid playerId);

		/// <summary>
		///
		/// </summary>
		void requestWatchGame(Guid gameId, Guid playerId);

		/// <summary>
		///
		/// </summary>
		void requestPlayGame(Guid gameId, LightUser lightUser);

		/// <summary>
		///
		/// </summary>
		void createNewGame(GameOptions gameOptions);

		/// <summary>
		///
		/// </summary>
		void getProfile(Guid playerId);
	}
}
