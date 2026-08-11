using System;

namespace Innovation
{
	public readonly struct PartTypeWrapper : IEquatable<PartTypeWrapper>
	{
		private readonly bool m_hasValue;

		private readonly PartTypeCode m_value;

		public bool HasValue => m_hasValue;

		public PartTypeCode Value => m_value;

		public static PartTypeWrapper None { get; } = default(PartTypeWrapper);

		public static PartTypeWrapper Unknown { get; } = new PartTypeWrapper(PartTypeCode.Unknown);

		public static PartTypeWrapper All { get; } = new PartTypeWrapper(PartTypeCode.All);

		public PartTypeWrapper(PartTypeCode value)
			: this(hasValue: true, value)
		{
		}

		private PartTypeWrapper(bool hasValue, PartTypeCode value)
		{
			m_hasValue = hasValue;
			m_value = value;
		}

		public override bool Equals(object other)
		{
			if (other is PartTypeWrapper other2)
			{
				return Equals(other2);
			}
			return false;
		}

		public bool Equals(PartTypeWrapper other)
		{
			if (m_hasValue == other.m_hasValue)
			{
				return m_value == other.m_value;
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (!m_hasValue)
			{
				return 0;
			}
			return m_value.GetHashCode();
		}

		public override string ToString()
		{
			return m_value.ToString();
		}

		public static PartTypeWrapper Parse(string text)
		{
			return Parse(text, ignoreCase: true);
		}

		public static PartTypeWrapper Parse(string text, bool ignoreCase)
		{
			return new PartTypeWrapper((PartTypeCode)Enum.Parse(typeof(PartTypeCode), text, ignoreCase));
		}

		public static bool TryParse(string text, out PartTypeWrapper result)
		{
			return TryParse(text, ignoreCase: true, out result);
		}

		public static bool TryParse(string text, bool ignoreCase, out PartTypeWrapper result)
		{
			object result2;
			bool flag = Enum.TryParse(typeof(PartTypeCode), text, ignoreCase, out result2);
			result = (flag ? new PartTypeWrapper((PartTypeCode)result2) : default(PartTypeWrapper));
			return flag;
		}

		public static implicit operator PartTypeWrapper(PartTypeCode value)
		{
			return new PartTypeWrapper(value);
		}

		public static implicit operator PartTypeWrapper(int value)
		{
			return new PartTypeWrapper((PartTypeCode)value);
		}

		public static implicit operator PartTypeWrapper(string text)
		{
			return Parse(text);
		}
	}
}
