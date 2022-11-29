using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.interfaces
{
    public interface Interface_Comm_calls_Data_Server
    {
        LightUser getUser();
        List<LightUser> registerUser(LightUser lightUser);
        void removeUser(int playerId);
		List<int> getPlayersOfGame(Game game);
		Game addPlayerToGame(int playerId);
        Game addUserToGame(LightUser user, int gameId);

		//Game createNewGame(GameOptions options);
		void uneregisterUser(int playerId);
	

    }
}
