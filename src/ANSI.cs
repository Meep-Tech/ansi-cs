using System.Text;
using System.Text.RegularExpressions;

namespace Meep.Tech.Text {

    /// <summary>
    /// Utilities and Constants for ANSI escape codes.
    /// </summary>
    public static partial class ANSI {
        /// <summary>
        /// The ANSI code for resetting all styles.
        /// </summary>
        public const int ResetCode
            = 0;

        /// <summary>
        /// The ANSI Escape for resetting all styles.
        /// </summary>
        public static string ResetEscape
            => Escape(ResetCode);

        /// <summary>
        /// The prefix for ANSI Escape codes.
        /// </summary>
        public static string EscapePrefix
            => "\e[";

        /// <summary>
        /// The suffix for ANSI Escape codes.
        /// </summary>
        public static string EscapeSuffix
            => "m";

        /// <summary>
        /// The regex pattern for matching ANSI Escape codes.
        /// </summary>
        public static Regex EscapePattern
            => _GetEscapeRegEx();

        /// <summary>
        /// Simple ANSI colors (8-bit).
        /// </summary>
        public enum Color {
            Reset = ResetCode,
            Black = 30,
            Red = 31,
            Green = 32,
            Yellow = 33,
            Blue = 34,
            Magenta = 35,
            Cyan = 36,
            White = 37,
            BrightBlack = 90,
            BrightRed = 91,
            BrightGreen = 92,
            BrightYellow = 93,
            BrightBlue = 94,
            BrightMagenta = 95,
            BrightCyan = 96,
            BrightWhite = 97,
        }

        /// <summary>
        /// A full 24-bit RGB ANSI color.
        /// </summary>
        public readonly struct RGB {
            #region Private Fields
            private static readonly Lazy<Random> _rng = new();
            #endregion

            #region Predefined Colors
            public static readonly RGB Black = (0, 0, 0);
            public static readonly RGB Red = (255, 0, 0);
            public static readonly RGB Green = (0, 255, 0);
            public static readonly RGB Blue = (0, 0, 255);
            public static readonly RGB Yellow = (255, 255, 0);
            public static readonly RGB Magenta = (255, 0, 255);
            public static readonly RGB Cyan = (0, 255, 255);
            public static readonly RGB White = (255, 255, 255);
            public static readonly RGB Purple = (128, 0, 128);
            public static readonly RGB Orange = (255, 165, 0);
            public static readonly RGB Pink = (255, 192, 203);
            public static readonly RGB Brown = (165, 42, 42);
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

            public RGB(byte r, byte g, byte b)
                => (R, G, B) = (r, g, b);

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

            public static implicit operator RGB((byte r, byte g, byte b) rgb)
                => new(rgb.r, rgb.g, rgb.b);

            public static implicit operator RGB((int r, int g, int b) rgb)
                => new((byte)rgb.r, (byte)rgb.g, (byte)rgb.b);

            public static implicit operator (byte r, byte g, byte b)(RGB rgb)
                => (rgb.R, rgb.G, rgb.B);
        }

        public static readonly IReadOnlyDictionary<string, Color> Colors = new Dictionary<string, Color>() {
            { "default", Color.Reset },
            { "black", Color.Black },
            { "red", Color.Red },
            { "green", Color.Green },
            { "yellow", Color.Yellow },
            { "blue", Color.Blue },
            { "magenta", Color.Magenta },
            { "cyan", Color.Cyan },
            { "white", Color.White },
            { "grey", Color.BrightBlack },
            { "gray", Color.BrightBlack }
        }.AsReadOnly();

        public enum Bg {
            Reset = ResetCode,
            Black = 40,
            Red = 41,
            Green = 42,
            Yellow = 43,
            Blue = 44,
            Magenta = 45,
            Cyan = 46,
            White = 47,
            BrightBlack = 100,
            BrightRed = 101,
            BrightGreen = 102,
            BrightYellow = 103,
            BrightBlue = 104,
            BrightMagenta = 105,
            BrightCyan = 106,
            BrightWhite = 107,
        }

        public static readonly IReadOnlyDictionary<string, Bg> Backgrounds = new Dictionary<string, Bg>() {
            { "default", Bg.Reset },
            { "black", Bg.Black },
            { "red", Bg.Red },
            { "green", Bg.Green },
            { "yellow", Bg.Yellow },
            { "blue", Bg.Blue },
            { "magenta", Bg.Magenta },
            { "cyan", Bg.Cyan },
            { "white", Bg.White },
            { "grey", Bg.BrightBlack },
            { "gray", Bg.BrightBlack },
        }.AsReadOnly();

        public enum Effect {
            Reset = ResetCode,
            Bold = 1,
            Faint = 2,
            Italic = 3,
            Underline = 4,
            Blink = 5,
            FastBlink = 6,
            Reverse = 7,
            Conceal = 8,
            CrossedOut = 9,
            Framed = 51,
            Encircled = 52,
            Overlined = 53,
        }

        public static readonly IReadOnlyDictionary<string, Effect> Effects = new Dictionary<string, Effect>() {
            { "default", Effect.Reset },
            { "bold", Effect.Bold },
            { "faint", Effect.Faint },
            { "italic", Effect.Italic },
            { "underline", Effect.Underline },
            { "blink", Effect.Blink },
            { "fastblink", Effect.FastBlink },
            { "fast-blink", Effect.FastBlink },
            { "fast blink", Effect.FastBlink },
            { "reverse", Effect.Reverse },
            { "conceal", Effect.Conceal },
            { "crossedout", Effect.CrossedOut },
            { "crossed-out", Effect.CrossedOut },
            { "crossed out", Effect.CrossedOut },
            { "framed", Effect.Framed },
            { "encircled", Effect.Encircled },
            { "overlined", Effect.Overlined },
        }.AsReadOnly();

        public static string Clear(string text) {
            Regex pattern = _GetEscapeRegEx();
            return pattern.Replace(text, "");
        }

        public static string Reset(string text = null!)
            => text is not null
                ? $"{Escape(ResetCode)}{text ?? ""}"
                : ResetEscape;

        public static string AddColor(string text, Color color, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(color)}{text}"
                : $"{Escape(color)}{text}{Reset()}";

        public static string AddColor(string text, RGB color, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(color)}{text}"
                : $"{Escape(color)}{text}{Reset()}";

        public static string AddBg(string text, Bg bg, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(bg)}{text}"
                : $"{Escape(bg)}{text}{Reset()}";

        public static string AddEffect(string text, Effect effect, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(effect)}{text}"
                : $"{Escape(effect)}{text}{Reset()}";

        public static string Bold(string text, bool thenReset = true)
            => AddEffect(text, Effect.Bold, thenReset);

        public static string Italic(string text, bool thenReset = true)
            => AddEffect(text, Effect.Italic, thenReset);

        public static string Underline(string text, bool thenReset = true)
            => AddEffect(text, Effect.Underline, thenReset);

        public static string Stylize(
            string text,
            Color? color = null!,
            Bg? bg = null!,
            Effect? effect = null!
        ) {
            StringBuilder sb = new();
            if(color is not null) {
                sb.Append(Escape(color.Value));
            }

            if(bg is not null) {
                sb.Append(Escape(bg.Value));
            }

            if(effect is not null) {
                sb.Append(Escape(effect.Value));
            }

            sb.Append(text);
            sb.Append(Reset());

            return sb.ToString();
        }

        public static string Stylize(
            string message,
            RGB color,
            Bg? bg = null!,
            Effect? effect = null!
        ) {
            StringBuilder sb = new(Escape(color));

            if(bg is not null) {
                sb.Append(Escape(bg.Value));
            }

            if(effect is not null) {
                sb.Append(Escape(effect.Value));
            }

            sb.Append(message);
            sb.Append(Reset());

            return sb.ToString();
        }

        public static string Stylize(
            string message,
            Color? color = null!,
            RGB? bg = null!,
            Effect? effect = null!
        ) {
            StringBuilder sb = new();
            if(color is not null) {
                sb.Append(Escape(color.Value));
            }

            if(bg is not null) {
                sb.Append(Escape(bg.Value));
            }

            if(effect is not null) {
                sb.Append(Escape(effect.Value));
            }

            sb.Append(message);
            sb.Append(Reset());

            return sb.ToString();
        }

        public static string Stylize(
            string message,
            RGB? color,
            RGB? bg,
            Effect? effect = null!
        ) {
            StringBuilder sb = new();
            if(color is not null) {
                sb.Append(Escape(color.Value));
            }

            if(bg is not null) {
                sb.Append(Escape(bg.Value));
            }

            if(effect is not null) {
                sb.Append(Escape(effect.Value));
            }

            sb.Append(message);
            sb.Append(Reset());

            return sb.ToString();
        }

        public static string Indent(string text, int amount, char indent = '\t', bool initial = true, bool newline = true)
            => Indent(text, amount, indent.ToString(), initial, newline);

        public static string Indent(string text, int amount, string indent = "\t", bool initial = true, bool newline = true) {
            string indents = string.Concat(Enumerable.Repeat(indent, amount));
            return $"{(newline ? '\n' : "")}{(initial ? indents : "")}{text.Replace("\n", indents)}";
        }

        public static string Dedent(string text, int? amount = null, string indent = "\t|  ")
            => amount is null
                ? _GetRemoveAllIndentRegex().Replace(text, "")
                : Regex.Replace(text, $"^({indent}){{{amount}}}", "", RegexOptions.Multiline);

        public static string Escape(Color color)
            => Escape((int)color);

        public static string Escape(Bg bg)
            => Escape((int)bg);

        public static string Escape(Effect effect)
            => Escape((int)effect);

        public static string Escape(int code)
            => Escape(code.ToString());

        public static string Escape(string code)
            => $"{EscapePrefix}{code}{EscapeSuffix}";

        public static string Escape(Color? color = null!, Color? bg = null!)
            => color is not null && bg is not null
                ? $"{Escape(color.Value)}{EscapeBg(bg.Value)}"
                : color is not null
                    ? Escape(color.Value)
                    : bg is not null
                        ? EscapeBg(bg.Value)
                        : "";

        public static string Escape(RGB? color = null!, RGB? bg = null!)
            => color is not null && bg is not null
                ? $"{Escape(color)}{Escape(bg)}"
                : color is not null
                    ? Escape(color.Value)
                    : bg is not null
                        ? EscapeBg(bg.Value)
                        : "";

        public static string Escape(RGB color, bool asBg = false)
            => asBg
                ? EscapeBg(color)
                : Escape(color);

        public static string Escape(RGB color)
            => $"{EscapePrefix}38;2;{color.R};{color.G};{color.B}{EscapeSuffix}";

        public static string EscapeBg(Color color)
            => Escape((int)color + 10);

        public static string EscapeBg(RGB color)
            => $"{EscapePrefix}48;2;{color.R};{color.G};{color.B}{EscapeSuffix}";

        #region Generated Regex

        [GeneratedRegex(@"\e\[\d+m")]
        private static partial Regex _GetEscapeRegEx();

        [GeneratedRegex(@"^\s+", RegexOptions.Multiline)]
        private static partial Regex _GetRemoveAllIndentRegex();

        #endregion
    }
}
