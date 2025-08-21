using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lecture_SmallGame.Program;

namespace Lecture_SmallGame.SmallEngine;

internal class Pixel(int x, int y)
{
    public bool IsDirty { get; private set; } = true;
    public char Char { get; private set; } = ' ';
    public Color ForegroundColor { get; private set; } = Color.Gray;
    public Color BackgroundColor { get; private set; } = Color.Black;
    public ColorMod ColorMod { get; private set; } = ColorMod.Normal;
    public int Layer { get; private set; } = int.MaxValue;

    public void Invalidate()
    {
        IsDirty = true;
    }

    public void Write()
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
