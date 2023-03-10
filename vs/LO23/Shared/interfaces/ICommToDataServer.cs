using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.interfaces
{
    public interface ICommToDataServer
    {
		(List<LightUser>, List<LightGame>) registerUser(LightUser lightUser);
        void removeUser(Guid playerId);
		List<int> getPlayersOfGame(Game game);
		Game addPlayerToGame(Guid playerId);
        Game addUserToGame(LightUser user, Guid gameId);
		Game createNewGame(GameOptions options);
		void uneregisterUser(Guid playerId);
		Game applyActionToPlayer(GameAction gameAction);
		Game removePlayerToGame(Guid playerId, Guid gameId);
		(List<LightUser>, List<LightGame>) getUsersAndGames();
	}
}
