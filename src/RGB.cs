namespace Meep.Tech.Text {

    public static partial class ANSI {
        /// <summary>
        /// A full 24-bit ANSI color composed of RGB(Red, Green, and Blue) components.
        /// </summary>
        public readonly struct RGB {
            #region Private Fields
            private static readonly Lazy<Random> _rng = new();
            #endregion

            #region Predefined Colors
            /// <summary>
            /// Predefined RGB color: Black (0, 0, 0)
            /// </summary>
            public static readonly RGB Black = (0, 0, 0);

            /// <summary>
            /// Predefined RGB color: Red (255, 0, 0)
            /// </summary>
            public static readonly RGB Red = (255, 0, 0);

            /// <summary>
            /// Predefined RGB color: Green (0, 255, 0)
            /// </summary>
            public static readonly RGB Green = (0, 255, 0);

            /// <summary>
            /// Predefined RGB color: Blue (0, 0, 255)
            /// </summary>
            public static readonly RGB Blue = (0, 0, 255);

            /// <summary>
            /// Predefined RGB color: Yellow (255, 255, 0)
            /// </summary>
            public static readonly RGB Yellow = (255, 255, 0);

            /// <summary>
            /// Predefined RGB color: Magenta (255, 0, 255)
            /// </summary>
            public static readonly RGB Magenta = (255, 0, 255);

            /// <summary>
            /// Predefined RGB color: Cyan (0, 255, 255)
            /// </summary>
            public static readonly RGB Cyan = (0, 255, 255);

            /// <summary>
            /// Predefined RGB color: White (255, 255, 255)
            /// </summary>
            public static readonly RGB White = (255, 255, 255);

            /// <summary>
            /// Predefined RGB color: Purple (128, 0, 128)
            /// </summary>
            public static readonly RGB Purple = (128, 0, 128);

            /// <summary>
            /// Predefined RGB color: Orange (255, 165, 0)
            /// </summary>
            public static readonly RGB Orange = (255, 165, 0);

            /// <summary>
            /// Predefined RGB color: Pink (255, 192, 203)
            /// </summary>
            public static readonly RGB Pink = (255, 192, 203);

            /// <summary>
            /// Predefined RGB color: Brown (165, 42, 42)
            /// </summary>
            public static readonly RGB Brown = (165, 42, 42);

            /// <summary>
            /// Predefined RGB color: Gray (128, 128, 128)
            /// </summary>
            public static readonly RGB Gray = (128, 128, 128);
            #endregion

            /// <summary>
            /// Get a random RGB color.
            /// </summary>
            public static RGB Random => new(
                (byte)_rng.Value.Next(0, 256),
                (byte)_rng.Value.Next(0, 256),
                (byte)_rng.Value.Next(0, 256)
            );

            /// <summary>
            /// The red component of the color.
            /// </summary>
            public byte R { get; init; } = 0;

            /// <summary>
            /// The green component of the color.
            /// </summary>
            public byte G { get; init; } = 0;

            /// <summary>
            /// The blue component of the color.
            /// </summary>
            public byte B { get; init; } = 0;

            /// <summary>
            /// Get a slightly lighter version of the color. (20%)
            /// </summary>
            public RGB Lighter
                => Lerp(this, White, 0.2);

            /// <summary>
            /// Get a much brighter version of the color. (50%)
            /// </summary>
            public RGB Brighter
                => Lerp(this, White, 0.5);

            /// <summary>
            /// Get a slightly darker version of the color. (20%)
            /// </summary>
            public RGB Darker
                => Lerp(this, Black, 0.2);

            /// <summary>
            /// Get a much dimmer version of the color. (50%)
            /// </summary>
            public RGB Dimmer
                => Lerp(this, Black, 0.5);

            /// <summary>
            /// Create a new RGB color.
            /// </summary>
            /// <param name="r">The red component of the color.</param>
            /// <param name="g">The green component of the color.</param>
            /// <param name="b">The blue component of the color.</param>
            public RGB(byte r, byte g, byte b)
                => (R, G, B) = (r, g, b);

            /// <summary>
            /// Deconstruct the color into its components.
            /// </summary>
            /// <param name="r">The red component of the color.</param>
            /// <param name="g">The green component of the color.</param>
            /// <param name="b">The blue component of the color.</param>
            public readonly void Deconstruct(out byte r, out byte g, out byte b) {
                r = R;
                g = G;
                b = B;
            }

            /// <summary>
            /// Used to lighten this color using linear interpolation.
            /// </summary>
            /// <param name="scale">A value between 0 and 1 used to scale the lightening effect.</param>
            /// <returns>A new color that is lightened from the original using the scale.</returns>
            public RGB Lighten(double scale = 0.1)
                => Lerp(this, White, scale);

            /// <summary>
            /// Used to darken this color using linear interpolation.
            /// </summary>
            /// <param name="scale">A value between 0 and 1 used to scale the darkening effect.</param>
            /// <returns>A new color that is darkened from the original using the scale.</returns>
            public RGB Darken(double scale = 0.1)
                => Lerp(this, Black, scale);

            /// <summary>
            /// Used to mix this color with another color using linear interpolation.
            /// </summary>
            /// <param name="other">The other color to mix with.</param>
            /// <param name="scale">
            ///     A double between 0 and 1 representing how the resulting mix is scaled;
            ///     <list type="bullet">
            ///         <item>0.5 is a 50-50 mix</item>
            ///         <item>0 is max for the current color</item>
            ///         <item>1 is max for the other color</item>
            ///     </list>
            /// </param>
            /// <returns>A new color that is a mix of this color and the other color.</returns>
            public RGB Mix(RGB other, double scale = 0.5)
                => Lerp(this, other, scale);

            /// <summary>
            /// Linearly interpolates between two colors.
            /// </summary>
            /// <param name="a">The first color to interpolate from.</param>
            /// <param name="b">The second color to interpolate to.</param>
            /// <param name="scale">
            ///     A value between 0 and 1 used to scale the interpolation.
            ///     <list type="bullet">
            ///         <item>0 is max for the first color</item>
            ///         <item>1 is max for the second color</item>
            ///         <item>0.5 is a 50-50 mix</item>
            ///     </list>
            /// </param>
            /// <returns></returns>
            public static RGB Lerp(RGB a, RGB b, double scale = 1)
                => new(
                    (byte)(a.R + ((b.R - a.R) * scale)),
                    (byte)(a.G + ((b.G - a.G) * scale)),
                    (byte)(a.B + ((b.B - a.B) * scale))
                );

            /// <summary>
            /// Implicitly convert a tuple of bytes to an RGB color.
            /// </summary>
            /// <param name="rgb">A tuple of bytes representing the color using Red, Green, and Blue values.</param>
            public static implicit operator RGB((byte r, byte g, byte b) rgb)
                => new(rgb.r, rgb.g, rgb.b);

            /// <summary>
            /// Implicitly convert a tuple of ints to an RGB color.
            /// </summary>
            /// <param name="rgb">A tuple of ints representing the color using Red, Green, and Blue values.</param>
            public static implicit operator RGB((int r, int g, int b) rgb)
                => new((byte)rgb.r, (byte)rgb.g, (byte)rgb.b);

            /// <summary>
            /// Implicitly convert an RGB color to a tuple of bytes representing the color using Red, Green, and Blue values.
            /// </summary>
            public static implicit operator (byte r, byte g, byte b)(RGB rgb)
                => (rgb.R, rgb.G, rgb.B);

            /// <summary>
            /// Implicitly convert an RGB color to a tuple of ints representing the color using Red, Green, and Blue values.
            /// </summary>
            public static implicit operator (int r, int g, int b)(RGB rgb)
                => (rgb.R, rgb.G, rgb.B);

            /// <summary>
            /// Implicitly convert an ANSI Console color to an RGB color.
            /// </summary>
            public static implicit operator RGB(Color color)
                => color switch {
                    Color.Reset => White,
                    Color.Black => Black,
                    Color.Red => Red,
                    Color.Green => Green,
                    Color.Blue => Blue,
                    Color.Yellow => Yellow,
                    Color.Magenta => Magenta,
                    Color.Cyan => Cyan,
                    Color.White => White,
                    Color.BrightBlack => Gray,
                    Color.BrightRed => Red.Brighter,
                    Color.BrightGreen => Green.Brighter,
                    Color.BrightBlue => Blue.Brighter,
                    Color.BrightYellow => Yellow.Brighter,
                    Color.BrightMagenta => Magenta.Brighter,
                    Color.BrightCyan => Cyan.Brighter,
                    Color.BrightWhite => White.Brighter,
                    _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Unknown ANSI Console color.")
                };
        }
    }
}
