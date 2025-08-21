using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine;

internal class ScreenBuffer : IEnumerable<Pixel>
{

    private readonly Pixel[,] _buffer;

    public ScreenBuffer(int width, int height)
    {
        _buffer = new Pixel[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _buffer[i, j] = new Pixel(i, j);
            }
        }
    }

    public IEnumerator<Pixel> GetEnumerator()
    {
        return new Enumerator(this);
    }

    internal void ClearBuffer(int x, int y)
    {
        if (!HasKeys(x, y))
            return;

        _buffer[x, y].Clear();
    }

    internal int GetLayerAt(int x, int y)
    {

        return _buffer[x, y].Layer;
    }

    internal bool HasKeys(int x, int y)
    {
        if (x < 0 || x >= _buffer.GetLength(0))
            return false;
        if (y < 0 || y >= _buffer.GetLength(1))
            return false;

        return true;
    }

    internal void UpdatePixel(int x, int y, PixelData pixelData)
    {
        if (!HasKeys(x, y))
            return;

        _buffer[x, y].UpdatePixel(pixelData);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class Enumerator : IEnumerator<Pixel>
    {
        public Pixel Current => screenBuffer._buffer[x, y];

        object IEnumerator.Current => Current;

        private ScreenBuffer screenBuffer;
        private int x = -1;
        private int y = 0;

        public Enumerator(ScreenBuffer screenBuffer)
        {
            this.screenBuffer = screenBuffer;
        }

        public void Dispose()
        {
            x = -1;
            y = 0;
        }

        public bool MoveNext()
        {
            x++;
            if (x >= screenBuffer._buffer.GetLength(0))
            {
                x = 0;
                y++;

                if (y >= screenBuffer._buffer.GetLength(1))
                {
                    return false;
                }
            }

            return true;
        }

        public void Reset()
        {
            x = -1;
            y = 0;
        }
    }
}
