namespace Meep.Tech.Text {

    public static partial class ANSI {

        /// <summary>
        /// ANSI escape codes for text effects.
        /// </summary>
        public enum Effect {

            /// <summary>
            /// Used to reset the effect to the default.
            /// </summary>
            Reset = ResetCode,

            /// <summary>
            /// Used to make text bold.
            /// </summary>
            Bold = 1,

            /// <summary>
            /// Used to make text faint.
            /// </summary>
            Faint = 2,

            /// <summary>
            /// Used to make text italic.
            /// </summary>
            Italic = 3,

            /// <summary>
            /// Used to make text underlined.
            /// </summary>
            Underline = 4,

            /// <summary>
            /// Used to make text blink slowly.
            /// </summary>
            Blink = 5,

            /// <summary>
            /// Used to make text blink rapidly.
            /// </summary>
            FastBlink = 6,

            /// <summary>
            /// Used to reverse the foreground and background colors.
            /// </summary>
            Reverse = 7,

            /// <summary>
            /// Used to conceal text.
            /// </summary>
            Conceal = 8,

            /// <summary>
            /// Used to cross out text.
            /// </summary>
            CrossedOut = 9,

            /// <summary>
            /// Used to make text bold and underlined.
            /// </summary>
            Framed = 51,

            /// <summary>
            /// Used to make text bold and overlined.
            /// </summary>
            Encircled = 52,

            /// <summary>
            /// Used to make text bold, underlined, and overlined.
            /// </summary>
            Overlined = 53,
        }
    }
}
