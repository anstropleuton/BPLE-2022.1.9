using System.IO;

namespace Innovation
{
	public interface IJsonService
	{
		string Serialize<T>(T value);

		string Serialize<T>(T value, bool indented);

		void Serialize<T>(TextWriter writer, T value);

		void Serialize<T>(TextWriter writer, T value, bool indented);

		T Deserialize<T>(string text);

		T Deserialize<T>(TextReader reader);
	}
}
