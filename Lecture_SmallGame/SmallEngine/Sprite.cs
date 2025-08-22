

namespace Lecture_SmallGame.SmallEngine;

public class Sprite
{
    public int Width { get; init; }
    public int Height { get; init; }
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

    public char CharAt(int x, int y)
    {
        if (y < 0 || y >= Height)
            return '\0';

        if (x < 0 || x >= Width)
            return '\0';

        return _pixelDatas[x, y].Char;
    }

    public PixelData PixelDataAt(int x, int y)
    {
        return _pixelDatas[x, y]; // will throw an exception if out of bounds
    }
}
