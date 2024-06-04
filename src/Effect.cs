namespace Meep.Tech.Text {

  public static partial class ANSI {

    /// <summary>
    /// ANSI escape codes for text effects.
    /// </summary>
    public enum Effect {
      Reset = ResetCode,
      Bold = 1,
      Faint = 2,
      Italic = 3,
      Underline = 4,
      Blink = 5,
      FastBlink = 6,
      Reverse = 7,
      Conceal = 8,
      CrossedOut = 9,
      Framed = 51,
      Encircled = 52,
      Overlined = 53,
    }
  }
}
