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
                string expected = $"\u001b[38;5;{color}m";
                string actual = Text.ANSI.Escape(color);

                Assert.Equal(expected, actual);
            }
        }
    }
}