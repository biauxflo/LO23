using Shared.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.interfaces
{
    public interface Interface_IHM_Main_calls_Data_Client
    {
        void registerProfile(User user);
        User authenticate(string login, string password); // login is username#id
        void logout();
        List<LightGame> requestSavedGames();
        Game requestStartReplay();
       void watchGame();
        void playGame(int GameId);
        void createNewGame(GameOptions options);
       void getProfile(int userId);
    }
}
