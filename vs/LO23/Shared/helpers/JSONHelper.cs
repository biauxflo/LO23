using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
					JArray array1 = (JArray)object1["d"];
					return array1;
				} catch (Exception e)
				{
					throw new Exception("FailedToParseFileToJSON");
				}
			}
			throw new Exception("FileDoesntExist");
		}
	}
}
