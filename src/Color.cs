namespace Meep.Tech.Text {

  public static partial class ANSI {
    /// <summary>
    /// Escape codes for simple ANSI colors (4-bit).
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
  }
}
