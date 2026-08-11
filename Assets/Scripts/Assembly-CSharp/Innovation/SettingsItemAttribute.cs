using System;

namespace Innovation
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class SettingsItemAttribute : Attribute
	{
		public string Name { get; private set; }

		public SettingsItemAttribute(string name)
		{
			Name = name;
		}
	}
}
