using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Innovation
{
	public class JsonAliasConverter<T> : JsonConverter<T> where T : new()
	{
		private static Dictionary<string, string> s_aliasMap;

		public override bool CanWrite => false;

		static JsonAliasConverter()
		{
			s_aliasMap = new Dictionary<string, string>();
			PropertyInfo[] properties = typeof(T).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				object[] customAttributes = propertyInfo.GetCustomAttributes(inherit: true);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					if (customAttributes[j] is JsonAliasAttribute jsonAliasAttribute)
					{
						s_aliasMap.Add(jsonAliasAttribute.Alias, propertyInfo.Name);
					}
				}
			}
		}

		public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JObject jObject = JObject.Load(reader);
			JObject jObject2 = new JObject();
			foreach (JProperty item in jObject.Properties())
			{
				if (s_aliasMap.TryGetValue(item.Name, out var value))
				{
					jObject2.Add(value, item.Value);
				}
				else
				{
					jObject2.Add(item.Name, item.Value);
				}
			}
			T val = new T();
			serializer.Populate(jObject2.CreateReader(), val);
			return val;
		}

		public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
