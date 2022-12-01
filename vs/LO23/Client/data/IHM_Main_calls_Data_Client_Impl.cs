using System;
using System.Collections.Generic;
using Client.data;
using Shared.data;
using Shared.interfaces;
using Shared.helpers;
using Shared.constants;
using Newtonsoft.Json.Linq;

public class IHM_Main_calls_Data_Client_Impl : Shared.interfaces.Interface_IHM_Main_calls_Data_Client
{
    Data_Client_ctrl data_client_ctrl;
	public IHM_Main_calls_Data_Client_Impl(Data_Client_ctrl data_client_ctrl)
	{
        this.data_client_ctrl = data_client_ctrl;
    }

     public User authenticate(string login, string password)
    {
		int parser = login.IndexOf('#');
		if(parser < 0)
		{
			throw new Exception("InvalidLoginFormat");
		}
		string username = login.Substring(0, parser);
		string id = login.Substring(parser + 1, login.Length - 1);

		string hash = login + password;

		try
		{
			JArray listUsersJSON = JSONHelper.getJSONFromFile(Constants.USER_STORAGE_PATH);
			List<User> users = listUsersJSON.ToObject<List<User>>();;
			User user = users.Find(u => u.password == hash);

			if(user != null)
			{
				return user;
			}
			throw new Exception("BadCredentials");
		} catch (Exception e)
		{
			throw new Exception("BadCredentials");
		}
    }

    public void createNewGame(int nbPlayers, int nbTokens, bool spectatorsAllowed, bool chatAllowed, int roundMax, int doubleBlind)
    {
        throw new NotImplementedException();
    }

    public void getProfile(int userId)
    {
        throw new NotImplementedException();
    }

    public void logout()
    {
        throw new NotImplementedException();
    }

    public void playGame(int gameId)
    {
        data_client_ctrl.request_PlayGameToComm(gameId);
    }

    public void registerProfile(User user)
    {
        throw new NotImplementedException();
    }

    public List<LightGame> requestSavedGames()
    {
        throw new NotImplementedException();
    }

    public Game requestStartReplay()
    {
        throw new NotImplementedException();
    }

    public void watchGame()
    {
        throw new NotImplementedException();
    }
    /*
Boolean authenticate(logint string, passwordt string)
{
   int i = 0;
   foreach (string line in System.IO.File.ReadLines(@"./user1.txt"))
   {
       string[] lineArray = line.Split(' '); // si le séparateur est un espace
       if ((logint == lineArray[0]) && (passwordt == lineArray[1])) { return True }
       else { return False };
   }

}
User getLigthUser(logint string)
{
   user = getLigthUserdeUser(login); 
}

*/
}
