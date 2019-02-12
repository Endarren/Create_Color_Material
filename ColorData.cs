using npScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
namespace npScripts
{
	[System.Serializable]
	public class ColorData : iXML, IComparable<ColorData>
	{
		public enum ColorDataType { Hex, RGB, HSL, HSV }
		public ColorDataType colorType;
		public string name;
		public string hex;
		public float red;
		public float green;
		public float blue;

		public float hue;

		public float saturationHSL;
		public float light;

		public float saturationHSV;
		public float value;

		public Color CreateColor()
		{
			Color c = new Color();
			switch (colorType)
			{
				case ColorDataType.RGB:
					c = new Color(red/100f, green / 100f, blue / 100f);
					break;
				case ColorDataType.HSV:
					c = new Color();
					c = Color.HSVToRGB(hue, saturationHSV, value);
					break;
				case ColorDataType.HSL:
					c = HSLToRGBColor(this);
					break;
				case ColorDataType.Hex:
					ColorUtility.TryParseHtmlString("#" + hex, out c);
					break;
			}
			return c;
		}
		public static Color HSLToRGBColor(ColorData hsbColor)
		{
			float r = hsbColor.light;
			float g = hsbColor.light;
			float b = hsbColor.light;
			if (hsbColor.saturationHSL != 0)
			{
				float max = hsbColor.light;
				float dif = hsbColor.light * hsbColor.saturationHSL;
				float min = hsbColor.light - dif;

				float h = hsbColor.hue * 360f;

				if (h < 60f)
				{
					r = max;
					g = h * dif / 60f + min;
					b = min;
				}
				else if (h < 120f)
				{
					r = -(h - 120f) * dif / 60f + min;
					g = max;
					b = min;
				}
				else if (h < 180f)
				{
					r = min;
					g = max;
					b = (h - 120f) * dif / 60f + min;
				}
				else if (h < 240f)
				{
					r = min;
					g = -(h - 240f) * dif / 60f + min;
					b = max;
				}
				else if (h < 300f)
				{
					r = (h - 240f) * dif / 60f + min;
					g = min;
					b = max;
				}
				else if (h <= 360f)
				{
					r = max;
					g = min;
					b = -(h - 360f) * dif / 60 + min;
				}
				else
				{
					r = 0;
					g = 0;
					b = 0;
				}
			}

			return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b));
		}

		public void ReadWikipediaColorLine (string line)
		{
			string [] splits = line.Split(new char[]{ '\t'});
			name = splits [0];
			hex = splits [1].Substring(1);
			if (splits [2].Length > 2)
				red = float.Parse(splits [2].Remove(splits [2].Length - 2));
			else
			{
				red = float.Parse(splits [2].Remove(splits [2].Length - 1));
			}
			if (splits [3].Length > 2)
				green = float.Parse(splits [3].Remove(splits [3].Length - 2));
			else
			{
				green = float.Parse(splits [3].Remove(splits [3].Length - 1));
			}

			if (splits [4].Length > 2)
				blue = float.Parse(splits [4].Remove(splits [4].Length - 2));
			else
				blue = float.Parse(splits [4].Remove(splits [4].Length - 1));

			if (splits [5].Length > 2)
				hue = float.Parse(splits [5].Remove(splits [5].Length - 2));
			else
				hue = float.Parse(splits [5].Remove(splits [5].Length - 1));

			if (splits [6].Length > 2)
				saturationHSL = float.Parse(splits [6].Remove(splits [6].Length - 2));
			else
				saturationHSL = float.Parse(splits [6].Remove(splits [6].Length - 1));

			if (splits [7].Length > 2)
				light = float.Parse(splits [7].Remove(splits [7].Length - 2));
			else
				light = float.Parse(splits [7].Remove(splits [7].Length - 1));

			if (splits [8].Length > 2)
				saturationHSV = float.Parse(splits [8].Remove(splits [8].Length - 2));
			else
				saturationHSV = float.Parse(splits [8].Remove(splits [8].Length - 1));

			if (splits [9].Length > 2)
				value = float.Parse(splits [9].Remove(splits [9].Length - 2));
			else
				value = float.Parse(splits [9].Remove(splits [9].Length - 1));
			/*
			for (int i = 0; i < splits.Length; i++)
			{
				Debug.Log(splits [i]);
			}*/
		}
		public void ReadFromXML(XmlReader reader)
		{
			name = reader.GetAttribute("name");
			hex = reader.GetAttribute("hex");
			red = float.Parse(reader.GetAttribute("red"));
			green = float.Parse(reader.GetAttribute("green"));
			blue = float.Parse(reader.GetAttribute("blue"));

			hue = float.Parse(reader.GetAttribute("hue"));

			saturationHSL = float.Parse(reader.GetAttribute("saturationHSL"));
			light = float.Parse(reader.GetAttribute("light"));

			saturationHSV = float.Parse(reader.GetAttribute("saturationHSV"));
			value = float.Parse(reader.GetAttribute("value"));
		}

		public void WriteToXML(XmlWriter writer)
		{
			writer.WriteStartElement("Color");
			writer.WriteAttributeString("name", name);
			writer.WriteAttributeString("hex", hex);

			writer.WriteAttributeString("red", red.ToString());
			writer.WriteAttributeString("green", green.ToString());
			writer.WriteAttributeString("blue", blue.ToString());

			writer.WriteAttributeString("hue", hue.ToString());

			writer.WriteAttributeString("saturationHSL", saturationHSL.ToString());
			writer.WriteAttributeString("light", light.ToString());

			writer.WriteAttributeString("saturationHSV", saturationHSV.ToString());
			writer.WriteAttributeString("value", value.ToString());
			writer.WriteEndElement();
		}

		public string GetElementName()
		{
			return "Color";
		}
		public override string ToString()
		{
			return name;
		}

		public int CompareTo(ColorData other)
		{
			return name.CompareTo(other.name);
		}
	}
}