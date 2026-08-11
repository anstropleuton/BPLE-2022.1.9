namespace Innovation
{
	public interface ICoreService
	{
		void Write(object arg);

		void Write(params object[] args);

		void WriteLine(object arg);

		void WriteLine(params object[] args);

		void Clear();

		void EnableFeedback();

		void DisableFeedback();

		void Feedback(object arg);

		void Feedback(params object[] args);

		object GetSettingsValue(string name);

		void SetSettingsValue(string name, object value);

		void ResetSettings();

		object GetUserSettingsValue(string name);

		void SetUserSettingsValue(string name, object value);

		void ResetUserSettings();

		void SaveUserSettings();

		GameState GetGameState();
	}
}
