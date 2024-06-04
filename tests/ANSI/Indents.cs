namespace Meep.Tech.Text.Tests {
    public partial class ANSI {
        public class Indents {

            public static class Examples {
                public static readonly string[] Inputs
                    = [
                        "hello",
                        "hello world",
                        "hello\nworld",
                        "\nhello",
                        "\nhello world",
                        "\nhello\nworld",
                        "hello\n\tworld",
                        "\nhello\n\tworld",
                        "\n\thello\n\tworld",
                        "\thello",
                        "\thello\nworld",
                        "\thello\n\tworld",
                        "\t\nhello",
                    ];

                public static readonly string[] Indents
                    = [
                        " ",
                        "\t",
                        "  ",
                        "    ",
                    ];
            }

            public static IEnumerable<object[]> GetIndentPermutations() {
                foreach(string example in Examples.Inputs) {
                    foreach(string indent in Examples.Indents) {
                        for(int count = 1; count < 5; count++) {
                            yield return new object[] { example, indent, count };
                        }
                    }
                }
            }

            [Theory]
            [MemberData(nameof(GetIndentPermutations))]
            public void Indent(string text, string type, int count) {
                string result = Text.ANSI.Indent(text, count, type);
                string indent = string.Concat(Enumerable.Repeat(type, count));

                if(text.StartsWith('\n')) {
                    Assert.StartsWith("\n" + indent + "\n", result);
                }
                else {
                    Assert.StartsWith("\n", result);
                }

                if(text.StartsWith(indent)) {
                    Assert.StartsWith("\n" + indent + indent, result);
                }
                else {
                    Assert.StartsWith("\n" + indent, result);
                }

                int lineCount = 1;
                foreach(string line in result.Split('\n').Skip(1)) {
                    Assert.StartsWith(string.Concat(Enumerable.Repeat(type, count)), line);
                    lineCount++;
                }

                Assert.Equal(text.Split('\n').Length + 1, lineCount);
            }

            [Theory]
            [MemberData(nameof(GetIndentPermutations))]
            public void Indent_Inline(string text, string type, int count) {
                string result = Text.ANSI.Indent(text, count, type, newline: false, initial: false);

                if(!text.StartsWith('\n')) {
                    Assert.False(result.StartsWith("\n"));
                }
                else {
                    Assert.False(result.StartsWith("\n\n"));
                }

                string indent = string.Concat(Enumerable.Repeat(type, count));
                if(text.StartsWith(indent)) {
                    Assert.False(result.StartsWith(indent + indent));
                }
                else {
                    Assert.False(result.StartsWith(indent));
                }

                int lineCount = 1;
                foreach(string line in result.Split('\n').Skip(1)) {
                    Assert.StartsWith(string.Concat(Enumerable.Repeat(type, count)), line);
                    lineCount++;
                }
                Assert.Equal(text.Split('\n').Length, lineCount);
            }

            [Theory]
            [MemberData(nameof(GetIndentPermutations))]
            public void Indent_NoNewline(string text, string type, int count) {
                string result = Text.ANSI.Indent(text, count, type, newline: false);

                if(!text.StartsWith('\n')) {
                    Assert.False(result.StartsWith("\n"));
                }
                else {
                    Assert.False(result.StartsWith("\n\n"));
                }

                int lineCount = 0;
                foreach(string line in result.Split('\n')) {
                    Assert.StartsWith(string.Concat(Enumerable.Repeat(type, count)), line);
                    lineCount++;
                }

                Assert.Equal(text.Split('\n').Length, lineCount);
            }

            [Theory]
            [MemberData(nameof(GetIndentPermutations))]
            public void Indent_NoInitial(string text, string type, int count) {
                string result = Text.ANSI.Indent(text, count, type, initial: false);

                if(text.StartsWith('\n')) {
                    Assert.StartsWith("\n\n", result);
                }
                else {
                    Assert.StartsWith("\n", result);
                }

                string indent = string.Concat(Enumerable.Repeat(type, count));
                if(text.StartsWith(indent)) {
                    Assert.False(result.StartsWith(indent + indent));
                }
                else {
                    Assert.False(result.StartsWith(indent));
                }

                Assert.False(result.StartsWith(string.Concat(Enumerable.Repeat(type, count))));

                int lineCount = 2;
                foreach(string line in result.Split('\n').Skip(2)) {
                    Assert.StartsWith(string.Concat(Enumerable.Repeat(type, count)), line);
                    lineCount++;
                }

                Assert.Equal(text.Split('\n').Length + 1, lineCount);
            }
        }
    }
}