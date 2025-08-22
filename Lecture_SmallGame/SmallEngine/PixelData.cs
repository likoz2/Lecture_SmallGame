namespace Lecture_SmallGame.SmallEngine;

/// <summary>
/// Class that holds the data for Pixel which is used to write into console.
/// </summary>
/// <param name="Char"></param>
/// <param name="ForegroundColor"></param>
/// <param name="BackgroundColor"></param>
/// <param name="ColorMod"></param>
/// <param name="Layer"></param>
public record PixelData(char Char, Color ForegroundColor, Color BackgroundColor, ColorMod ColorMod, int Layer)
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
