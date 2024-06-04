namespace Meep.Tech.Text {

  public static partial class ANSI {

    /// <summary>
    /// ANSI escape codes for background colors.
    /// </summary>
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
  }
}
