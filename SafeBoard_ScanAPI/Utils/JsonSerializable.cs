using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SafeBoard_ScanAPI.Utils;

namespace SimpleSecureChatAPI.Utils
{
    public abstract class JsonSerializable<T>
	{
		public override string ToString()
		{
			return Serialize();
		}

		public static T Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, GetSettings());
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject(this, GetSettings());
		}

		private static JsonSerializerSettings GetSettings()
		{
			var settings = new JsonSerializerSettings();
			settings.NullValueHandling = NullValueHandling.Include;
			settings.ContractResolver = new RequireObjectPropertiesContractResolver();
			settings.Converters.Add(new StringEnumConverter());

			return settings;
		}
	}
}
