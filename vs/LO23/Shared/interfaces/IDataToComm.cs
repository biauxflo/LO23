using System;
using Shared.data;
namespace Shared.interfaces
{
	public interface IDataToComm
	{
		/// <summary>
		/// Declares to the server a new connected user.
		/// </summary>
		/// <param name="user">User</param>
		void announceUser(LightUser user);

		/// <summary>
		/// Declares to the server that a user is disconnected.
		/// </summary>
		/// <param name="userId">Id of the disconnected user</param>
		void unannounce(Guid userId);

		/// <summary>
		/// Sends a message to the chat.
		/// </summary>
		/// <param name="chatMsg">Message</param>
		void sendMessage(ChatMessage chatMsg);

		/// <summary>
		/// Asks to the server to end a game.
		/// </summary>
		/// <param name="gameId">Game to terminate</param>
		void requestStopGame(Guid gameId);

		/// <summary>
		/// A player leaves a game.
		/// </summary>
		/// <param name="gameId">Game to quit</param>
		/// <param name="playerId">New player</param>
		void requestLeaveGame(Guid gameId, Guid playerId);

		/// <summary>
		/// Plays an action of the game.
		/// </summary>
		/// <param name="gameAction">Action to do</param>
		void requestPlayRound(GameAction gameAction);

		/// <summary>
		/// A player gets a game to watch.
		/// </summary>
		/// <param name="gameId">Game to watch</param>
		/// <param name="playerId">Viewer</param>
		void requestWatchGame(Guid gameId, Guid playerId);

		/// <summary>
		/// A player joins a game.
		/// </summary>
		/// <param name="gameId">Game to join</param>
		/// <param name="lightUser">New player</param>
		void requestPlayGame(Guid gameId, LightUser lightUser);

		/// <summary>
		/// Creates a new game.
		/// </summary>
		/// <param name="gameOptions">Options to create the game</param>
		void createNewGame(GameOptions gameOptions);

		/// <summary>
		/// Gets a user's profile.
		/// </summary>
		/// <param name="playerId">User to get profile from</param>
		void getProfile(Guid playerId);
	}
}
