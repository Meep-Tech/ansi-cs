# Meep.Tech.ANSI
ANSI Text Styling and Formatting helpers, extensions, and constants for C#.
Used to color and styleize strings for terminal and console output using ANSI text escapes.

## Text Styling
### Colors
Colors can be applied to either the foreground or background of text using the `[Add]Color` and `[Add]Bg` [methods](#methods) respectively. Arguments for color can be provided in two ways: As a [Simple Color](#simple-colors) or [RGB Color](#rgb-colors).

#### Simple Colors (3/4-bit Colors)
The `ANSI.Color` enum provides access to the standard list of [3 and 4-bit ANSI colors](https://en.wikipedia.org/wiki/ANSI_escape_code#3-bit_and_4-bit).
This list includes the bright versions of each standard color as well as 0 to use as a Reset Code.

#### RGB Colors
##### 24-bit RGB Colors
[24-bit ANSI RGB colors](https://en.wikipedia.org/wiki/ANSI_escape_code#24-bit) can be built using the `ANSI.RGB` struct. This struct provides a static method named `From24Bit` as well as a constructor that each take three byte values for Red, Green, and Blue respectively in order to produce the 24-bit RGB color value. The struct defenition also includes several static pre-defined colors and helper methods to lighten, darken, and adjust them.

##### 8-bit RGB Colors
**NOT YET IMPLEMENTED!!!**
<s>[8-bit ANSI RGB colors](https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit) can be built into an `ANSI.RGB` struct using the `ANSI.RGB.From8Bit` method or by using the `ANSI.RGB` constructor with a single byte value.<s>

### Effects
Various ANSI effects are also included using the `ANSI.Effect` enum. These effects can be applied to text using the `[Add]Effect` [method](#methods).

### Indentation
The `ANSI.Indent` and `ANSI.Dedent` methods can be used to apply and remove indentation from text respectively. These methods are useful for applying uniform indentation to multiline blocks of text.

## Methods and Properties
### Text Styling Methods
All stylization functions are available statically via the [ANSI](./src/ANSI.cs "Static Styling Members") class or as extension methods for `char`, `string` types via the [ANSIStringExtensions](./src/Extensions/Strings.cs "String Extensions") and [ANSICharExtensions](./src/Extensions/Chars.cs "Char Extensions") classes respectively.

> **Note**: Some methods in the `ANSI` class are prefixed with `Add`, while the matching extension methods are named without the prefix.
>
>> *Ex: `ANSI.AddColor` vs `"text".Color`*
>>
| Method        | Description                                                                   |
|---------------|-------------------------------------------------------------------------------|
| [Add]Color    | Used to apply a 'foreground' color to the text characters themselves.         |
| [Add]Bg       | Used to apply a 'background' color to highlight the terminal behind the text. |
| [Add]Effect   | Used to apply a text effect to the text characters.                           |
| Stylize/Style | Used to apply color, bg, and effects all at once.)                            |
| Reset         | Used to reset the text style to the terminal default.                         |
| Clear         | Used to clear all text styling from the text.                                 |
| Indent        | Used to apply an indentation to some potentially-multiline text.              |
| Dedent        | Used to remove an indentation from some potentially-multiline text.           |
| Bold          | Used to apply a bold effect to the text.                                      |
| Italic        | Used to apply an italic effect to the text.                                   |
| Underline     | Used to apply an underline effect to the text.                                |

### RGB Color Helper Members
| Method   | Description                                                               |
|----------|---------------------------------------------------------------------------|
| Lighten  | Used to lighten a color by a given %                                      |
| Darken   | Used to darken a color by a given %.                                      |
| Lighter  | Used to lighten a color by 20%.                                           |
| Darker   | Used to darken a color by 20%.                                            |
| Dimmer   | Used to dim a color by 50%.                                               |
| Brighter | Used to brighten a color 50%                                              |
| Mix      | Used to blend two colors together. (Statically accessable via `RGB.Lerp`) |
| Random   | Used to generate a random color on the fly.                               |
