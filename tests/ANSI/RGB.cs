namespace Meep.Tech.Text.Tests {
    public partial class ANSI {
        public class RGB {
            [Fact]
            public void Random()
              => Assert.NotEqual(Text.ANSI.RGB.Random, Text.ANSI.RGB.Random);
        }
    }
}