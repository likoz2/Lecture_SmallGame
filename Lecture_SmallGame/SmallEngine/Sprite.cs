

namespace Lecture_SmallGame.SmallEngine;

/// <summary>
/// Class that provides a Sprite behavior. Contains a manipulation for its content.
/// </summary>
public class Sprite
{
    /// <summary>Width of the <see cref="Sprite"/>.</summary>
    public int Width { get; init; }
    /// <summary>Height of the <see cref="Sprite"/>.</summary>
    public int Height { get; init; }
    /// <summary>Layer of the <see cref="Sprite"/>.</summary>
    public int Layer
    {
        get;
        internal set
        {
            field = value;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _pixelDatas[i, j].Layer = value;
                }
            }
        }
    }

    private readonly PixelData[,] _pixelDatas;

    /// <summary>Initializes a new instance of the <see cref="Sprite"/> class using a width and height parameters.</summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Sprite(int width, int height)
    {
        Width = Math.Clamp(width, 1, 128);
        Height = Math.Clamp(height, 1, 128);

        _pixelDatas = new PixelData[Width, Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _pixelDatas[i, j] = new PixelData('\0', Color.Gray, Color.Black, ColorMod.Normal, 10);
            }
        }
    }

    /// <summary>Initializes a new instance of the <see cref="Sprite"/> class using an array.</summary>
    /// <param name="array">The sprite to be created.</param>
    public Sprite(string[] array)
    {
        Width = array.ToList().Max(x => x.Length);
        Height = array.Length;

        _pixelDatas = new PixelData[Width, Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (i >= array[j].Length)
                    _pixelDatas[i, j] = new PixelData('\0', Color.Gray, Color.Black, ColorMod.Normal, 10);
                else
                    _pixelDatas[i, j] = new PixelData(array[j][i], Color.Gray, Color.Black, ColorMod.Normal, 10);
            }
        }
    }

    /// <summary>
    /// Checks what <see langword="char"/> is at a specified position in the sprite.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>A <see langword="char"/> on specified position or '\0' when out of bounds.</returns>
    public char CharAt(int x, int y)
    {
        if (y < 0 || y >= Height)
            return '\0';

        if (x < 0 || x >= Width)
            return '\0';

        return _pixelDatas[x, y].Char;
    }

    /// <summary>
    /// Checks what <see cref="PixelData"/> is at a specified position in the sprite.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>A <see cref="PixelData"/> on specified position.</returns>
    /// <exception cref="IndexOutOfRangeException"><paramref name="x"/> or <paramref name="y"/> out of bounds.</exception>
    public PixelData PixelDataAt(int x, int y)
    {
        return _pixelDatas[x, y]; // will throw an exception if out of bounds
    }
}
