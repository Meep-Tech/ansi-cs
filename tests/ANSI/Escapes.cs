namespace Meep.Tech.Text.Tests {
    public partial class ANSI {
        public class Escapes {

            [Fact]
            public void Reset() {
                const string expected = "\u001b[0m";
                string actual = Text.ANSI.ResetEscape;

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0, 0, 0)]
            [InlineData(128, 0, 128)]
            [InlineData(1, 2, 3)]
            [InlineData(255, 255, 255)]
            public void RGB(byte r, byte g, byte b) {
                string expected = $"\u001b[38;2;{r};{g};{b}m";
                string actual = Text.ANSI.Escape(new Text.ANSI.RGB(r, g, b));

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(Text.ANSI.Color.Black)]
            [InlineData(Text.ANSI.Color.Red)]
            [InlineData(Text.ANSI.Color.Green)]
            [InlineData(Text.ANSI.Color.Yellow)]
            [InlineData(Text.ANSI.Color.Blue)]
            [InlineData(Text.ANSI.Color.Magenta)]
            [InlineData(Text.ANSI.Color.Cyan)]
            [InlineData(Text.ANSI.Color.White)]
            [InlineData(Text.ANSI.Color.BrightBlack)]
            [InlineData(Text.ANSI.Color.BrightRed)]
            [InlineData(Text.ANSI.Color.BrightGreen)]
            [InlineData(Text.ANSI.Color.BrightYellow)]
            [InlineData(Text.ANSI.Color.BrightBlue)]
            [InlineData(Text.ANSI.Color.BrightMagenta)]
            [InlineData(Text.ANSI.Color.BrightCyan)]
            [InlineData(Text.ANSI.Color.BrightWhite)]
            public void Color(Text.ANSI.Color color) {
                string code = ((int)color).ToString();
                string expected = $"\u001b[{code}m";
                string actual = Text.ANSI.Escape(color);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(Text.ANSI.Bg.Black)]
            [InlineData(Text.ANSI.Bg.Red)]
            [InlineData(Text.ANSI.Bg.Green)]
            [InlineData(Text.ANSI.Bg.Yellow)]
            [InlineData(Text.ANSI.Bg.Blue)]
            [InlineData(Text.ANSI.Bg.Magenta)]
            [InlineData(Text.ANSI.Bg.Cyan)]
            [InlineData(Text.ANSI.Bg.White)]
            [InlineData(Text.ANSI.Bg.BrightBlack)]
            [InlineData(Text.ANSI.Bg.BrightRed)]
            [InlineData(Text.ANSI.Bg.BrightGreen)]
            [InlineData(Text.ANSI.Bg.BrightYellow)]
            [InlineData(Text.ANSI.Bg.BrightBlue)]
            [InlineData(Text.ANSI.Bg.BrightMagenta)]
            [InlineData(Text.ANSI.Bg.BrightCyan)]
            [InlineData(Text.ANSI.Bg.BrightWhite)]
            public void Bg(Text.ANSI.Bg bg) {
                string code = ((int)bg).ToString();
                string expected = $"\u001b[{code}m";
                string actual = Text.ANSI.Escape(bg);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(Text.ANSI.Color.Black)]
            [InlineData(Text.ANSI.Color.Red)]
            [InlineData(Text.ANSI.Color.Green)]
            [InlineData(Text.ANSI.Color.Yellow)]
            [InlineData(Text.ANSI.Color.Blue)]
            [InlineData(Text.ANSI.Color.Magenta)]
            [InlineData(Text.ANSI.Color.Cyan)]
            [InlineData(Text.ANSI.Color.White)]
            [InlineData(Text.ANSI.Color.BrightBlack)]
            [InlineData(Text.ANSI.Color.BrightRed)]
            [InlineData(Text.ANSI.Color.BrightGreen)]
            [InlineData(Text.ANSI.Color.BrightYellow)]
            [InlineData(Text.ANSI.Color.BrightBlue)]
            [InlineData(Text.ANSI.Color.BrightMagenta)]
            [InlineData(Text.ANSI.Color.BrightCyan)]
            [InlineData(Text.ANSI.Color.BrightWhite)]
            public void Bg_Color(Text.ANSI.Color color) {
                string code = ((int)color + 10).ToString();
                string expected = $"\u001b[{code}m";
                string actual = Text.ANSI.EscapeBg(color);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0, 0, 0)]
            [InlineData(128, 0, 128)]
            [InlineData(1, 2, 3)]
            [InlineData(255, 255, 255)]
            public void Bg_RGB(byte r, byte g, byte b) {
                string expected = $"\u001b[48;2;{r};{g};{b}m";
                string actual = Text.ANSI.EscapeBg(new Text.ANSI.RGB(r, g, b));

                Assert.Equal(expected, actual);
            }


            [Theory]
            [InlineData(Text.ANSI.Effect.Bold)]
            [InlineData(Text.ANSI.Effect.Faint)]
            [InlineData(Text.ANSI.Effect.Italic)]
            [InlineData(Text.ANSI.Effect.Underline)]
            [InlineData(Text.ANSI.Effect.Blink)]
            [InlineData(Text.ANSI.Effect.FastBlink)]
            [InlineData(Text.ANSI.Effect.Reverse)]
            [InlineData(Text.ANSI.Effect.Conceal)]
            [InlineData(Text.ANSI.Effect.CrossedOut)]
            [InlineData(Text.ANSI.Effect.Framed)]
            [InlineData(Text.ANSI.Effect.Encircled)]
            [InlineData(Text.ANSI.Effect.Overlined)]
            public void Effect(Text.ANSI.Effect style) {
                string code = ((int)style).ToString();
                string expected = $"\u001b[{code}m";
                string actual = Text.ANSI.Escape(style);

                Assert.Equal(expected, actual);
            }
        }
    }
}