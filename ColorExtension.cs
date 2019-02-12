//<author>Nicholas Irwin</author>
//<company> nonPareil Institute</company>
//<copyright file ="ColorExtension.cs" All Rights Reserved
//</copyright>
//<date>1/24/2018</date>

namespace npScripts
{
	using UnityEngine;

	/// <summary>
	/// A set of extensions to the Color class.
	/// </summary>
	public static class ColorExtension
	{
		#region Methods
		public static void ConvertColor(this Color c, int r, int g, int b, float a = 1f)
		{
			c.r = r / 255f;
			c.g = g / 255f;
			c.b =b /255f;
			c.a = a;
		}
		/// <summary>
		/// Returns an alphaed version of the given color.
		/// </summary>
		/// <param name="c">The <see cref="Color"/></param>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color AlphaColor(Color c, float alpha)
		{
			Color cx = c;
			cx.a = alpha;
			return cx;
		}

		/// <summary>
		/// Returns the color black with the given alpha.
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Black(float alpha)
		{
			return AlphaColor(Color.black, alpha);
		}

		/// <summary>
		/// Returns the color blue with the given alpha.
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Blue(float alpha)
		{
			return AlphaColor(Color.blue, alpha);
		}

		/// <summary>
		/// Returns the color cyan with the given alpha.
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Cyan(float alpha)
		{
			return AlphaColor(Color.cyan, alpha);
		}

		/// <summary>
		/// Returns the color green with the given alpha.
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Green(float alpha)
		{
			return AlphaColor(Color.green, alpha);
		}

		/// <summary>
		/// Returns the color orange. (51, 76.5, 102)
		/// </summary>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Orange()
		{
			Color color = new Color(0.2F, 0.3F, 0.4F);
			return color;
		}

		/// <summary>
		/// Returns the color orange with the given alpha. (51, 76.5, 102)
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Orange(float alpha)
		{
			return AlphaColor(ColorExtension.Orange(), alpha);
		}

		/// <summary>
		/// Returns the color red with the given alpha.
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Red(float alpha)
		{
			return AlphaColor(Color.red, alpha);
		}

		/// <summary>
		/// Returns the color white with the given alpha.
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color White(float alpha)
		{
			return AlphaColor(Color.white, alpha);
		}

		/// <summary>
		/// Returns the color yellow with the given alpha.
		/// </summary>
		/// <param name="alpha">The <see cref="float"/></param>
		/// <returns>The <see cref="Color"/></returns>
		public static Color Yellow(float alpha)
		{
			return AlphaColor(Color.yellow, alpha);
		}

		#endregion
	}
}
