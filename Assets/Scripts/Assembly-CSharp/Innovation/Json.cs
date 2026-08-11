using System.IO;

namespace Innovation
{
	public static class Json
	{
		public static IJsonService Service { get; set; }

		public static string Serialize<T>(T value)
		{
			return Service.Serialize(value);
		}

		public static string Serialize<T>(T value, bool indented)
		{
			return Service.Serialize(value, indented);
		}

		public static void Serialize<T>(TextWriter writer, T value)
		{
			Service.Serialize(writer, value);
		}

		public static void Serialize<T>(TextWriter writer, T value, bool indented)
		{
			Service.Serialize(writer, value, indented);
		}

		public static T Deserialize<T>(string text)
		{
			return Service.Deserialize<T>(text);
		}

		public static T Deserialize<T>(TextReader reader)
		{
			return Service.Deserialize<T>(reader);
		}

		public static string 序列化<T>(T value)
		{
			return Serialize(value);
		}

		public static string 序列化<T>(T value, bool indented)
		{
			return Serialize(value, indented);
		}

		public static void 序列化<T>(TextWriter writer, T value)
		{
			Serialize(writer, value);
		}

		public static void 序列化<T>(TextWriter writer, T value, bool indented)
		{
			Serialize(writer, value, indented);
		}

		public static T 反序列化<T>(string text)
		{
			return Deserialize<T>(text);
		}

		public static T 反序列化<T>(TextReader reader)
		{
			return Deserialize<T>(reader);
		}
	}
}
