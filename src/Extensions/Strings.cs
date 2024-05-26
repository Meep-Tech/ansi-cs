namespace Meep.Tech.Text {
    public static class ANSIStringExtensions {
        public static string Color(this string text, ANSI.Color color)
            => ANSI.AddColor(text, color);

        public static string Color(this string text, ANSI.RGB color)
            => ANSI.AddColor(text, color);

        public static string Bg(this string text, ANSI.Bg bg)
                => ANSI.AddBG(text, bg);

        public static string Effect(this string text, ANSI.Effect effect)
            => ANSI.AddEffect(text, effect);

        public static string Style(
            this string text,
            ANSI.Color? color = null!,
            ANSI.Bg? bg = null!,
            ANSI.Effect? effect = null!
        ) => ANSI.Stylize(text, color, bg, effect);

        public static string Style(
            this string text,
            ANSI.RGB color,
            ANSI.Bg? bg = null!,
            ANSI.Effect? effect = null!
        ) => ANSI.Stylize(text, color, bg, effect);

        public static string Style(
            this string text,
            ANSI.Color? color = null!,
            ANSI.RGB? bg = null!,
            ANSI.Effect? effect = null!
        ) => ANSI.Stylize(text, color, bg, effect);

        public static string Style(
            this string text,
            ANSI.RGB? color,
            ANSI.RGB? bg,
            ANSI.Effect? effect = null!
        ) => ANSI.Stylize(text, color, bg, effect);

        public static string Strip(this string text)
            => ANSI.Clear(text);

        public static string Reset(this string text)
            => ANSI.Reset(text);
    }
}
