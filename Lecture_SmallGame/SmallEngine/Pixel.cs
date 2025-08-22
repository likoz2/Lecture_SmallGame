
namespace Lecture_SmallGame.SmallEngine;

internal class Pixel(int x, int y)
{
    internal bool IsDirty { get; private set; } = true;
    internal char Char { get; private set; } = ' ';
    internal Color ForegroundColor { get; private set; } = Color.Gray;
    internal Color BackgroundColor { get; private set; } = Color.Black;
    internal ColorMod ColorMod { get; private set; } = ColorMod.Normal;
    internal int Layer { get; private set; } = int.MaxValue;

    internal void Invalidate()
    {
        IsDirty = true;
    }

    internal void Write()
    {
        Console.SetCursorPosition(x, y);
        Console.Write($"\u001b[38;2;{ForegroundColor:;}m\u001b[48;2;{BackgroundColor:;};{(int)ColorMod}m{Char}");

        IsDirty = false;
    }

    internal void Clear()
    {
        IsDirty = true;
        Char = ' ';
        ForegroundColor = Color.Gray;
        BackgroundColor = Color.Black;
        ColorMod = ColorMod.Normal;
    }

    internal void UpdatePixel(PixelData pixelData) => UpdatePixel(pixelData.Char, pixelData.ForegroundColor, pixelData.BackgroundColor, pixelData.ColorMod, pixelData.Layer);

    internal void UpdatePixel(char c, Color foregroundColor, Color backgroundColor, ColorMod colorMod, int layer)
    {
        Char = c;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
        ColorMod = colorMod;
        Layer = layer;

        IsDirty = true;
    }
}
