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
        Boolean authenticate(String login, String mdp);
        void logout();
        List<LightGame> requestSavedGames();
        Game requestStartReplay();
       void watchGame();
        void playGame(int GameId);
        void createNewGame(int nbPlayers, int nbTokens, Boolean spectatorsAllowed, Boolean chatAllowed, int roundMax, int doubleBlind);
       void getProfile(int userId);
    }
}
