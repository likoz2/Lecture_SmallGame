

namespace Lecture_SmallGame.SmallEngine;

/// <summary>
/// Class that manages colors which you can use to write.
/// </summary>
public struct Color : IFormattable
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; } = 255;

    /// <summary>
    /// A Random <see cref="Color"/>. Generates new <see cref="Color"/> every time this is used.
    /// </summary>
    public static Color Random => new Color(Rand.Next(0, 256), Rand.Next(0, 256), Rand.Next(0, 256));

    /// <summary>
    /// Create an instance using RGB float 0-255.
    /// </summary>
    /// <remarks>Allows a creation outside of the range.</remarks>
    /// <param name="r">red 0-255</param>
    /// <param name="g">green 0-255</param>
    /// <param name="b">blue 0-255</param>
    public Color(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    /// Create an instance using RGB int 0-255.
    /// </summary>
    /// <remarks>Allows a creation outside of the range.</remarks>
    /// <param name="r">red 0-255</param>
    /// <param name="g">green 0-255</param>
    /// <param name="b">blue 0-255</param>
    public Color(int r, int g, int b)
    {
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    /// Create an instance using gray scale float 0-255
    /// </summary>
    /// <param name="gray"></param>
    public Color(float gray) : this(gray, gray, gray)
    {
    }

    public Color(float gray, float alpha) : this(gray, gray, gray, alpha)
    {
    }

    /// <summary>
    /// Create an instance using RGB int 0-255.
    /// </summary>
    /// <remarks>Allows a creation outside of the range.</remarks>
    /// <param name="r">red 0-255</param>
    /// <param name="g">green 0-255</param>
    /// <param name="b">blue 0-255</param>
    /// <param name="a">alpha 0-255</param>
    public Color(float r, float g, float b, float a) : this(r, g, b)
    {
        A = a;
    }

    public static Color operator +(Color c1, Color c2)
    {
        return new Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
    }
    public static Color operator -(Color c1, Color c2)
    {
        return new Color(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B);
    }
    public static Color operator *(Color c1, float f)
    {
        return new Color(c1.R * f, c1.G * f, c1.B * f);
    }
    public static Color operator /(Color c1, float f)
    {
        return new Color(c1.R / f, c1.G / f, c1.B / f);
    }

    /// <summary>
    /// Create an instance using HSV values.
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <returns>New <see cref="Color"/> with RGB values calculated from the specified HSV.</returns>
    public static Color FromHSV(int h, int s, int v)
    {
        (int r, int g, int b) = HSVtoRGB(h, s, v);
        return new Color(r, g, b);
    }

    // Written by Copilot, it works... no idea if it is efficient or something...
    /// <summary>
    /// Convert HSV to RGB.
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <returns>Returns a <see langword="tuple"/> of r,g,b that are calculated from the specified HSV.</returns>
    public static (int r, int g, int b) HSVtoRGB(int h, int s, int v)
    {
        // Clamp input ranges
        h = Math.Clamp(h, 0, 359);
        s = Math.Clamp(s, 0, 100);
        v = Math.Clamp(v, 0, 100);

        float hf = h / 60f;
        float sf = s / 100f;
        float vf = v / 100f;

        int hi = (int)Math.Floor(hf) % 6;
        float f = hf - MathF.Floor(hf);

        float p = vf * (1f - sf);
        float q = vf * (1f - f * sf);
        float t = vf * (1f - (1f - f) * sf);

        float r = 0, g = 0, b = 0;
        switch (hi)
        {
            case 0:
                r = vf;
                g = t;
                b = p;
                break;
            case 1:
                r = q;
                g = vf;
                b = p;
                break;
            case 2:
                r = p;
                g = vf;
                b = t;
                break;
            case 3:
                r = p;
                g = q;
                b = vf;
                break;
            case 4:
                r = t;
                g = p;
                b = vf;
                break;
            case 5:
                r = vf;
                g = p;
                b = q;
                break;
        }

        return ((int)(r * 255), (int)(g * 255), (int)(b * 255));
    }

    /// <summary>
    /// Converts color to <see cref="string"/> similar to this: "0, 25, 150".
    /// </summary>
    /// <returns><see cref="string"/> like "R, G, B"</returns>
    public override string ToString()
    {
        return $"{R}, {G}, {B}";
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return format switch
        {
            "#" => $"{R}, {G}, {B}", // TODO Update this and add more formats
            ";" => $"{(int)Math.Round(R)};{(int)Math.Round(G)};{(int)Math.Round(B)}",
            _ => $"{R}, {G}, {B}",
        };
    }
    public static Color Black => new Color(0, 0, 0);
    public static Color DarkBlue => new Color(0, 0, 128);
    public static Color DarkGreen => new Color(0, 128, 0);
    public static Color DarkCyan => new Color(0, 128, 128);
    public static Color DarkRed => new Color(128, 0, 0);
    public static Color DarkMagenta => new Color(128, 0, 128);
    public static Color DarkYellow => new Color(128, 128, 0);
    public static Color Gray => new Color(192, 192, 192);
    public static Color DarkGray => new Color(128, 128, 128);
    public static Color Blue => new Color(0, 0, 255);
    public static Color Green => new Color(0, 255, 0);
    public static Color Cyan => new Color(0, 255, 255);
    public static Color Red => new Color(255, 0, 0);
    public static Color Magenta => new Color(255, 0, 255);
    public static Color Yellow => new Color(255, 255, 0);
    public static Color White => new Color(255, 255, 255);
    public static Color Transparent => new Color(0, 0, 0, 0);
}

