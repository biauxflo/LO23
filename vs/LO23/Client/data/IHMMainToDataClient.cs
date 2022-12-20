using System;
using System.Collections.Generic;
using Client.data;
using Shared.data;
using Shared.interfaces;
using Shared.helpers;
using Shared.constants;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class IHMMainToDataClient : Shared.interfaces.IMainToDataClient
{
    DataClientCore data_client_ctrl;
	public IHMMainToDataClient(DataClientCore data_client_ctrl)
	{
        this.data_client_ctrl = data_client_ctrl;
    }

    public void authenticate(string login, string password)
    {
		try
		{
			JArray listUsersJSON = JSONHelper.getJSONFromFile(Constants.USER_STORAGE_PATH);
			List<User> users = listUsersJSON.ToObject<List<User>>();
			User user = users.Find(u => u.username == login && u.password == password);

			if(user != null)
			{
				data_client_ctrl.ask_announceUser(User.ToLightUser(user));
				data_client_ctrl.CurrentUser = user;
                data_client_ctrl.SendConnectionSucceedToMain(user);
			} else {
                data_client_ctrl.SendConnectionFailedToMain("BadCredentials");
            }
		} catch (Exception e)
		{
			data_client_ctrl.SendConnectionFailedToMain("BadCredentials");
		}
    }

    public void createNewGame(GameOptions options)
    {
		data_client_ctrl.sendCreateNewGame(options);
    }

    public void getProfile(Guid userId)
    {
        throw new NotImplementedException();
    }

    public void logout()
    {
        throw new NotImplementedException();
    }

    public void playGame(Guid gameId, LightUser lightUser)
    {
        data_client_ctrl.request_PlayGameToComm(gameId, lightUser);
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
}
