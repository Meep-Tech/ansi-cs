# Meep.Tech.ANSI

ANSI Text Styling and Formatting helpers, extensions, and constants for C#.
Used to color and stylize strings for terminal and console output using ANSI text escapes.

## Installation

This package can be installed via NuGet using the following command:

```dotnetcli
dotnet add package Meep.Tech.ANSI
```

## Styling Features

This library uses ANSI escape codes and whitespace to enable easy and simple styling of text output for many standard consoles and terminals.

### Colors

Colors can be applied to either the foreground or background of text using the `[Add]Color` and `[Add]Bg` [methods](#methods) respectively. Arguments for color can be provided in two ways: As a [Simple Color](#simple-colors) or [RGB Color](#rgb-colors).

#### Simple Colors (3/4-bit)

The [`ANSI.Color`](./src/Color.cs) and [`ANSI.Bg`](./src/Bg.cs) enums provides access to the standard list of [3 and 4-bit ANSI colors](https://en.wikipedia.org/wiki/ANSI_escape_code#3-bit_and_4-bit).
These list include the bright versions of each standard color as well as 0 to use as a Reset Code.

#### RGB Colors (24-bit)

[24-bit ANSI RGB colors](https://en.wikipedia.org/wiki/ANSI_escape_code#24-bit) can be built using the [`ANSI.RGB`](./src/RGB.cs) struct. This struct provides a constructor that each take three byte values for Red, Green, and Blue respectively in order to produce the 24-bit RGB color value. The struct definition also includes several static pre-defined colors and helper methods to lighten, darken, and adjust them.

### Backgrounds

Background colors can be applied to text using the `[Add]Bg` [methods](#methods). These methods take a color argument and apply the color to the background of the text. You can also use the `ANSI.Bg` enum with any method that accepts a color argument to apply a background color instead of a foreground color.

### Effects

Various ANSI text effects are provided by the [`ANSI.Effect`](./src/Effect.cs) enum. These effects can be applied to text using the `[Add]Effect` [method](#methods).

### Indents

The `ANSI.Indent`, `ANSI.Dedent`, and `ANSI.Undent` methods can be used to apply and remove indentation from text. These methods are useful for applying uniform indentation to multiline blocks of text.

## Methods and Properties

### Text Styling Methods

All stylization functions are available statically via the [ANSI](./src/ANSI.cs "Static Styling Members") class or as extension methods for `string` types (via the [ANSIStringExtensions](./src/Extensions/Strings.cs "String Extensions") class).

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
| Clear/Strip   | Used to clear all text styling from the text.                                 |
| Reset         | Used to reset the text style to the terminal default.                         |
| Indent        | Used to apply an indentation to some potentially-multiline text.              |
| Dedent        | Used to remove an indentation from some potentially-multiline text.           |
| Undent        | Used to remove all indentation from some potentially-multiline text.          |
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
| Mix      | Used to blend two colors together. (Statically accessible via `RGB.Lerp`) |
| Random   | Used to generate a random color on the fly.                               |

## Usage Examples

```csharp
using Meep.Tech.Text;

// Apply a color to text
var redText = "This text is red".Color(ANSI.Color.Red);
Console.WriteLine(redText);
```

## Testing

Tests are contained within a seperate .Net C# Project named `Meep.Tech.ANSI.Tests` within the `/tests` directory. This project uses the testing framework: **[xUnit](https://xunit.net/)**.

## TODO

- [ ] Features:
  - [ ] Support for 8-bit ANSI Colors via the RGB struct's constructor
  - [ ] Conversion between 24, 8, and 4 bit colors
- [ ] Tests:
  - [ ] Styling
    - [ ] Color
    - [ ] Bg
    - [ ] Effect
    - [ ] Color + Bg
    - [ ] Color + Bg + Effect
  - [ ] Reset
  - [ ] Clear
  - [ ] Dedent
  - [ ] Undent
  - [ ] RGB Color Helper Members
