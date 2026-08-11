using System;
using System.Globalization;
using Newtonsoft.Json;
using UnityEngine;

namespace Innovation
{
	[JsonConverter(typeof(HexColorConverter))]
	public readonly struct HexColor
	{
		private readonly uint m_rgba;

		public byte R => (byte)((m_rgba >> 24) & 0xFF);

		public byte G => (byte)((m_rgba >> 16) & 0xFF);

		public byte B => (byte)((m_rgba >> 8) & 0xFF);

		public byte A => (byte)(m_rgba & 0xFF);

		public uint RGBA => m_rgba;

		public static HexColor Clear => default(HexColor);

		public static HexColor White => new HexColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		public static HexColor Black => new HexColor(0, 0, 0, byte.MaxValue);

		public static HexColor Red => new HexColor(byte.MaxValue, 0, 0, byte.MaxValue);

		public static HexColor Green => new HexColor(0, byte.MaxValue, 0, byte.MaxValue);

		public static HexColor Blue => new HexColor(0, 0, byte.MaxValue, byte.MaxValue);

		public HexColor(byte r, byte g, byte b)
			: this(r, g, b, byte.MaxValue)
		{
		}

		public HexColor(byte r, byte g, byte b, byte a)
		{
			m_rgba = (uint)((r << 24) | (g << 16) | (b << 8) | a);
		}

		public HexColor(uint rgba)
		{
			m_rgba = rgba;
		}

		public override bool Equals(object other)
		{
			if (other is HexColor other2)
			{
				return Equals(other2);
			}
			return false;
		}

		public bool Equals(HexColor other)
		{
			return m_rgba == other.m_rgba;
		}

		public override int GetHashCode()
		{
			return (int)m_rgba;
		}

		public override string ToString()
		{
			return ToString(includeAlpha: true);
		}

		public string ToString(bool includeAlpha)
		{
			if (includeAlpha)
			{
				return "#" + m_rgba.ToString("X8", CultureInfo.InvariantCulture);
			}
			return "#" + (m_rgba >> 8).ToString("X6", CultureInfo.InvariantCulture);
		}

		public static HexColor Parse(string text)
		{
			if (TryParse(text, out var result))
			{
				return result;
			}
			throw new FormatException();
		}

		public static bool TryParse(string text, out HexColor result)
		{
			if (!string.IsNullOrEmpty(text) && text.StartsWith('#'))
			{
				uint result3;
				if (text.Length == 7)
				{
					if (uint.TryParse(text.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result2))
					{
						result = new HexColor((result2 << 8) | 0xFF);
						return true;
					}
				}
				else if (text.Length == 9 && uint.TryParse(text.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result3))
				{
					result = new HexColor(result3);
					return true;
				}
			}
			result = default(HexColor);
			return false;
		}

		public static bool operator ==(HexColor left, HexColor right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(HexColor left, HexColor right)
		{
			return !(left == right);
		}

		public static explicit operator Color32(HexColor color)
		{
			return new Color32(color.R, color.G, color.B, color.A);
		}

		public static explicit operator Color(HexColor color)
		{
			return (Color32)color;
		}

		public static explicit operator HexColor(Color32 color)
		{
			return new HexColor(color.r, color.g, color.b, color.a);
		}

		public static explicit operator HexColor(Color color)
		{
			return (HexColor)(Color32)color;
		}
	}
}
