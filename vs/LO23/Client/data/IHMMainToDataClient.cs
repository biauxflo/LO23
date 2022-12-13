using System;
using System.Collections.Generic;
using Client.data;
using Shared.data;
using Shared.interfaces;
using Shared.helpers;
using Shared.constants;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

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

			this.authenticate(user);
		}
		catch (Exception e)
		{
			this.data_client_ctrl.SendConnectionFailedToMain("BadCredentials");
		}
    }

	private void authenticate(User user)
	{
		if(user != null)
		{
			this.data_client_ctrl.ask_announceUser(User.ToLightUser(user));
			this.data_client_ctrl.CurrentUser = user;
			this.data_client_ctrl.SendConnectionSucceedToMain(user);
		}
		else
		{
			this.data_client_ctrl.SendConnectionFailedToMain("BadCredentials");
		}
	}

	public void createNewGame(GameOptions options)
    {
		this.data_client_ctrl.sendCreateNewGame(options);
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
		this.data_client_ctrl.request_PlayGameToComm(gameId, lightUser);
    }

    public void registerProfile(User user)
    {
		if(File.Exists(Constants.USER_STORAGE_PATH))
		{
			// Some Users already exist
			try
			{
				JArray listUsersJSON = JSONHelper.getJSONFromFile(Constants.USER_STORAGE_PATH);
				List<User> users = listUsersJSON.ToObject<List<User>>();

				// We check if the user already exists locally
				if(users.Exists(u => u.username == user.username || u.id == user.id))
				{
					this.data_client_ctrl.SendConnectionFailedToMain("PlayerAlreadyExists");
				}
				else
				{
					// If not, we add the user
					users.Add(user);
					JSONHelper.writeUsersListToJSONFile(users);
					this.authenticate(user);
				}
			}
			catch(Exception e)
			{
				this.data_client_ctrl.SendConnectionFailedToMain("FileCorrupted");
			}
		}
		else
		{
			// No users already saved
			List<User> users = new List<User>
			{
				user
			};
			JSONHelper.writeUsersListToJSONFile(users);
			this.authenticate(user);
		}

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
