namespace Lecture_SmallGame.SmallEngine;

internal record PixelData(char Char, Color ForegroundColor, Color BackgroundColor, ColorMod ColorMod, int Layer)
{
    public Color ForegroundColor { get; set; } = ForegroundColor;

    public int Layer
    {
        get;
        set
        {
            field = value;
        }
    } = Layer;

}
