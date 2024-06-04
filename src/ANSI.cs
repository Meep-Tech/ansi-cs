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
        /// Simple Text Colors; by name.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, Color> Colors
            = new Dictionary<string, Color>() {
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

        /// <summary>
        /// Simple Background Colors; by name.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, Bg> Backgrounds
            = new Dictionary<string, Bg>() {
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

        /// <summary>
        /// Effects; by name.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, Effect> Effects
            = new Dictionary<string, Effect>() {
                { "default", Effect.Reset },
                { "bold", Effect.Bold },
                { "faint", Effect.Faint },
                { "italic", Effect.Italic },
                { "underline", Effect.Underline },
                { "blink", Effect.Blink },
                { "slow-blink", Effect.Blink },
                { "slowblink", Effect.Blink },
                { "fastblink", Effect.FastBlink },
                { "fast-blink", Effect.FastBlink },
                { "fast blink", Effect.FastBlink },
                { "reverse", Effect.Reverse },
                { "conceal", Effect.Conceal },
                { "crossout", Effect.CrossedOut },
                { "cross-out", Effect.CrossedOut },
                { "crossedout", Effect.CrossedOut },
                { "crossed-out", Effect.CrossedOut },
                { "crossed out", Effect.CrossedOut },
                { "framed", Effect.Framed },
                { "encircled", Effect.Encircled },
                { "overlined", Effect.Overlined },
            }.AsReadOnly();

        /// <summary>
        /// Remove all ANSI escape codes from a string.
        /// </summary>
        public static string Clear(string text)
            => _GetEscapeRegEx()
                .Replace(text, "");

        /// <summary>
        /// Add the ANSI escape code to Reset all styles.
        /// </summary>
        public static string Reset(string text = null!)
            => text is not null
                ? $"{ResetEscape}{text}"
                : ResetEscape;

        /// <summary>
        /// Add the ANSI escape code to set the text color using simple 4-bit colors.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string AddColor(string text, Color color, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(color)}{text}"
                : $"{Escape(color)}{text}{Reset()}";

        /// <summary>
        /// Add the ANSI escape code to set the text color using a full RGB color.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string AddColor(string text, RGB color, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(color)}{text}"
                : $"{Escape(color)}{text}{Reset()}";

        /// <summary>
        /// Add the ANSI escape code to set the background color using simple 4-bit colors.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string AddBg(string text, Bg bg, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(bg)}{text}"
                : $"{Escape(bg)}{text}{Reset()}";

        /// <summary>
        /// Add the ANSI escape code to set the background color using simple 4-bit colors.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string AddBg(string text, Color bg, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{EscapeBg(bg)}{text}"
                : $"{EscapeBg(bg)}{text}{Reset()}";

        /// <summary>
        /// Add the ANSI escape code to set the background color using full RGB colors.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string AddBg(string text, RGB bg, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{EscapeBg(bg)}{text}"
                : $"{EscapeBg(bg)}{text}{Reset()}";

        /// <summary>
        /// Add the ANSI escape code to set the text effect.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string AddEffect(string text, Effect effect, bool thenReset = true)
            => (!thenReset || text.EndsWith(Reset()))
                ? $"{Escape(effect)}{text}"
                : $"{Escape(effect)}{text}{Reset()}";

        /// <summary>
        /// Add the ANSI escape code for bold text.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string Bold(string text, bool thenReset = true)
            => AddEffect(text, Effect.Bold, thenReset);

        /// <summary>
        /// Add the ANSI escape code for italic text.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string Italic(string text, bool thenReset = true)
            => AddEffect(text, Effect.Italic, thenReset);

        /// <summary>
        /// Add the ANSI escape code for underlined text.
        /// </summary>
        /// <params><inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)" path="/param"/></params>
        public static string Underline(string text, bool thenReset = true)
            => AddEffect(text, Effect.Underline, thenReset);

        /// <summary>
        /// Add the ANSI escape codes to a piece of text for the given style options.
        /// </summary>
        /// <param name="text">The text to style.</param>
        /// <param name="color">The foreground/text color to set.</param>
        /// <param name="bg">The background color to set.</param>
        /// <param name="effect">The text effect to set.</param>
        /// <param name="thenReset">Whether to reset the styling after the given text (helpful for chaining).</param>
        public static string Stylize(
            string text,
            Color? color = null!,
            Bg? bg = null!,
            Effect? effect = null!,
            bool thenReset = true
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
            if(thenReset) {
                sb.Append(Reset());
            }

            return sb.ToString();
        }

        /// <inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)"/>
        public static string Stylize(
            string message,
            RGB color,
            Bg? bg = null!,
            Effect? effect = null!,
            bool thenReset = true
        ) {
            StringBuilder sb = new(Escape(color));

            if(bg is not null) {
                sb.Append(Escape(bg.Value));
            }

            if(effect is not null) {
                sb.Append(Escape(effect.Value));
            }

            sb.Append(message);
            if(thenReset) {
                sb.Append(Reset());
            }

            return sb.ToString();
        }

        /// <inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)"/>
        public static string Stylize(
            string message,
            Color? color = null!,
            RGB? bg = null!,
            Effect? effect = null!,
            bool thenReset = true
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
            if(thenReset) {
                sb.Append(Reset());
            }

            return sb.ToString();
        }

        /// <inheritdoc cref="Stylize(string, Color?, Bg?, Effect?, bool)"/>
        public static string Stylize(
            string message,
            RGB? color,
            RGB? bg,
            Effect? effect = null!,
            bool thenReset = true
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
            if(thenReset) {
                sb.Append(Reset());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Indent a block of text by a given amount.
        /// </summary>
        /// <param name="text">The text to indent.</param>
        /// <param name="amount">The amount of indentation to add.</param>
        /// <param name="indent">The indentation to use.</param>
        /// <param name="initial">Whether to indent the first line (appended to start of the text). (Defaults to true).</param>
        /// <param name="newline">Whether to add a newline before the text (and before optional initial indent). (Defaults to true).</param>
        public static string Indent(string text, int amount = 1, char indent = '\t', bool initial = true, bool newline = true)
            => Indent(text, amount, indent.ToString(), initial, newline);

        /// <inheritdoc cref="Indent(string, int, char, bool, bool)"/>
        public static string Indent(string text, int amount = 1, string indent = "\t", bool initial = true, bool newline = true) {
            string indents = string.Concat(Enumerable.Repeat(indent, amount));
            return $"{(newline ? '\n' : "")}{(initial ? indents : "")}{text.Replace("\n", indents)}";
        }

        /// <summary>
        /// Dedent a block of text by a given amount.
        /// </summary>
        /// <params><inheritdoc cref="Indent(string, int, char, bool, bool)"/></params>
        public static string Dedent(string text, int amount = 1, string indent = "\t|  ")
            => Regex.Replace(text, $"^({indent}){{{amount}}}", "", RegexOptions.Multiline);

        /// <summary>
        /// Remove all indentation from a block of text.
        /// </summary>
        /// <params><inheritdoc cref="Indent(string, int, char, bool, bool)"/></params>
        public static string Undent(string text)
            => _GetRemoveAllIndentRegex().Replace(text, "");

        /// <summary>
        /// Get the ANSI escape code for a given text/foreground color.
        /// </summary>
        public static string Escape(Color color)
            => Escape((int)color);

        /// <summary>
        /// Get the ANSI escape code for a given background color.
        /// </summary>
        public static string Escape(Bg bg)
            => Escape((int)bg);

        /// <summary>
        /// Get the ANSI escape code for a given text effect.
        /// </summary>
        public static string Escape(Effect effect)
            => Escape((int)effect);

        /// <summary>
        /// Get the ANSI escape for a given code and optional arguments.
        /// </summary>
        public static string Escape(int code, params int[] args)
            => args.Length > 0
                ? $"{EscapePrefix}{code};{string.Join(";", args)}{EscapeSuffix}"
                : $"{EscapePrefix}{code}{EscapeSuffix}";

        /// <summary>
        /// Get the ANSI escape codes for coloring text and backgrounds.
        /// </summary>
        /// <param name="color">The text color.</param>
        /// <param name="bg">The background color.</param>
        public static string Escape(Color? color = null!, Color? bg = null!)
            => color is not null && bg is not null
                ? $"{Escape(color.Value)}{EscapeBg(bg.Value)}"
                : color is not null
                    ? Escape(color.Value)
                    : bg is not null
                        ? EscapeBg(bg.Value)
                        : "";

        /// <inheritdoc cref="Escape(Color?, Color?)"/>
        public static string Escape(RGB? color = null!, RGB? bg = null!)
            => color is not null && bg is not null
                ? $"{Escape(color)}{Escape(bg)}"
                : color is not null
                    ? Escape(color.Value)
                    : bg is not null
                        ? EscapeBg(bg.Value)
                        : "";

        /// <summary>
        /// Get the ANSI escape code for a given RGB color.
        /// </summary>
        /// <param name="color">The color to escape.</param>
        /// <param name="asBg">Whether to escape as a background color.</param>
        public static string Escape(RGB color, bool asBg = false)
            => asBg
                ? EscapeBg(color)
                : Escape(color);

        /// <inheritdoc cref="Escape(RGB, bool)"/>
        public static string Escape(RGB color)
            => Escape(38, 2, color.R, color.G, color.B);

        /// <summary>
        /// Get the ANSI escape code for a given background color.
        /// </summary>
        /// <param name="color">The color to escape (as text background).</param>
        public static string EscapeBg(Color color)
            => Escape((int)color + 10);

        /// <summary>
        /// Get the ANSI escape code for a given background color.
        /// </summary>
        /// <param name="color">The color to escape (as text background).</param>
        public static string EscapeBg(RGB color)
            => Escape(48, 2, color.R, color.G, color.B);

        #region Generated Regex

        [GeneratedRegex(@"\e\[\d+m")]
        private static partial Regex _GetEscapeRegEx();

        [GeneratedRegex(@"^\s+", RegexOptions.Multiline)]
        private static partial Regex _GetRemoveAllIndentRegex();

        #endregion
    }
}
