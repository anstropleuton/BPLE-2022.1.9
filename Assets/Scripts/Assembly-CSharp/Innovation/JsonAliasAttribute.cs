using System;

namespace Innovation
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class JsonAliasAttribute : Attribute
	{
		public string Alias { get; set; }

		public JsonAliasAttribute(string alias)
		{
			Alias = alias;
		}
	}
}
