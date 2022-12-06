using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.data;
using System.Runtime.CompilerServices;
using Shared.constants;

namespace Shared.helpers
{
	public class JSONHelper
	{
		public static JArray getJSONFromFile(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					JObject object1 = JObject.Parse(File.ReadAllText(path));
					JArray array1 = (JArray)object1["users"];
					return array1;
				} catch (Exception e)
				{
					throw new Exception("FailedToParseFileToJSON");
				}
			}
			throw new Exception("FileDoesntExist");
		}

		public static void writeObjectToJSONFile(object obj, string path)
		{
			string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
			File.WriteAllText(path, jsonString);
		}

		public static void writeUsersListToJSONFile (List<User> users)
		{
			Dictionary<string, List<User>> dict = new Dictionary<string, List<User>>
			{
				{ "users", users }
			};
			writeObjectToJSONFile(dict, Constants.USER_STORAGE_PATH);
		}
	}
}
