using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine;

internal struct Color : IFormattable
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; } = 255;

    public static Color Random => new Color(SmallEngine.Random.Next(0, 256), SmallEngine.Random.Next(0, 256), SmallEngine.Random.Next(0, 256));

    internal Color(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
    }

    internal Color(int r, int g, int b)
    {
        R = r;
        G = g;
        B = b;
    }

    public Color(float gray) : this(gray, gray, gray)
    {
    }

    public Color(float gray, float alpha) : this(gray, gray, gray, alpha)
    {
    }

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

    internal static Color FromHSV(int h, int s, int v)
    {
        (int r, int g, int b) = HSVtoRGB(h, s, v);
        return new Color(r, g, b);
    }

    // Written by Copilot, it works... no idea if it is efficient or something...
    internal static (int r, int g, int b) HSVtoRGB(int h, int s, int v)
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

