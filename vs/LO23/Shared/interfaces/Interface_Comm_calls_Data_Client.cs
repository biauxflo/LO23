using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.data;

namespace Shared.interfaces
{
	public interface Interface_Comm_calls_Data_Client
	{
		
		void setGamesAndUsers(List<LightUser> listUsers, List<LightGame> listGame );
		void setLoggedOut();
		void updateMessages(ChatMessage message);
		void updateGame(Game game);
		void setGame(Game game);
		LightUser getProfile();
		void getProfileReturn(LightUser user);
		void removeUserFromListUsers(LightUser user);
		void addUserToListUsers(LightUser user);
		void removeGameFromListGames(LightGame game);
		void addGameToListGames(LightGame game);




	}
}

