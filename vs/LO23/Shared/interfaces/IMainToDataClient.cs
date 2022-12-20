using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.interfaces
{
    public interface IMainToDataClient
    {
        void registerProfile(User user);
		void registerProfile(string username, string password, string firstname, string lastname, int age);
		void authenticate(string login, string password); // login is username#id
        void logout();
        List<LightGame> requestSavedGames();
        Game requestStartReplay();
       void watchGame();
        void playGame(Guid gameId, LightUser lightUser);
        void createNewGame(GameOptions options);
       void getProfile(Guid userId);
    }
}
