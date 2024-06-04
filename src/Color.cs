namespace Meep.Tech.Text {

  public static partial class ANSI {

    /// <summary>
    /// Escape codes for simple ANSI colors (4-bit).
    /// </summary>
    public enum Color {

      /// <summary>
      /// Used to reset the color to the default.
      /// </summary>
      Reset = ResetCode,

      /// <summary>
      /// ANSI terminal color: Black
      /// </summary>
      Black = 30,

      /// <summary>
      /// ANSI terminal color: Red
      /// </summary>
      Red = 31,

      /// <summary>
      /// ANSI terminal color: Green
      /// </summary>
      Green = 32,

      /// <summary>
      /// ANSI terminal color: Yellow
      /// </summary>
      Yellow = 33,

      /// <summary>
      /// ANSI terminal color: Blue
      /// </summary>
      Blue = 34,

      /// <summary>
      /// ANSI terminal color: Magenta
      /// </summary>
      Magenta = 35,

      /// <summary>
      /// ANSI terminal color: Cyan
      /// </summary>
      Cyan = 36,

      /// <summary>
      /// ANSI terminal color: White
      /// </summary>
      White = 37,

      /// <summary>
      /// ANSI terminal color: Bright Black (Gray)
      /// </summary>
      BrightBlack = 90,

      /// <summary>
      /// ANSI terminal color: Bright Red
      /// </summary>
      BrightRed = 91,

      /// <summary>
      /// ANSI terminal color: Bright Green
      /// </summary>
      BrightGreen = 92,

      /// <summary>
      /// ANSI terminal color: Bright Yellow
      /// </summary>
      BrightYellow = 93,

      /// <summary>
      /// ANSI terminal color: Bright Blue
      /// </summary>
      BrightBlue = 94,

      /// <summary>
      /// ANSI terminal color: Bright Magenta
      /// </summary>
      BrightMagenta = 95,

      /// <summary>
      /// ANSI terminal color: Bright Cyan
      /// </summary>
      BrightCyan = 96,

      /// <summary>
      /// ANSI terminal color: Bright White
      /// </summary>
      BrightWhite = 97,
    }
  }
}
