using Lecture_SmallGame.SmallEngine.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Lecture_SmallGame.Program;

namespace Lecture_SmallGame.SmallEngine;

public enum ColorMod
{
    Reset = 0,
    Underline = 4,
    Invert = 7,
    Normal = 10,
}

public enum ColorGround
{
    Both = 0,
    Foreground = 38,
    Background = 48,
}

internal static class Writer
{
    public static int Width { get; private set; } = 160;
    public static int Height { get; private set; } = 40;

    private static readonly ScreenBuffer _screenBuffer = new ScreenBuffer(Width, Height);
    private static readonly List<Renderer> _renderers = new List<Renderer>();

    [SupportedOSPlatform("windows")]
    static Writer()
    {
        SetConsoleWindowSize(Width, Height);
    }

    [SupportedOSPlatform("windows")]
    private static void SetConsoleWindowSize(int width, int height)
    {
        Console.SetWindowSize(width, height);
    }

    public static void WriteBorder()
    {
        throw new NotImplementedException();
        Console.SetCursorPosition(1, Height);
        Console.WriteLine();
    }

    internal static void ReWrite()
    {
        _screenBuffer.Where(x => x.IsDirty).ToList().ForEach(x => x.Write());
    }

    internal static void AddRenderer(Renderer renderer, bool render = true)
    {
        int index = _renderers.Count;
        for (int i = 0; i < _renderers.Count; i++)
        {
            Renderer r = _renderers[i];
            if (renderer.Layer < r.Layer)
            {
                index = i;
                break;
            }
        }

        _renderers.Insert(index, renderer);

        if (render)
            TryRender(renderer);
    }

    internal static void TryRender(Renderer renderer)
    {
        for (int i = 0; i < renderer.Bounds.Size.X; i++)
        {
            for (int j = 0; j < renderer.Bounds.Size.Y; j++)
            {
                PixelData pixelData = renderer.CurrentPixelDataAt(i, j);
                int x = i + (int)renderer.Bounds.Position.X;
                int y = j + (int)renderer.Bounds.Position.Y;
                if (!_screenBuffer.HasKeys(x, y))
                    continue;

                if (pixelData.Layer < _screenBuffer.GetLayerAt(x, y))
                {
                    _screenBuffer.UpdatePixel(x, y, pixelData);
                }
            }
        }
    }

    internal static void RemoveRenderer(Renderer renderer)
    {
        _renderers.Remove(renderer);

        ReRender(renderer.Bounds);
    }

    internal static void OnRendererChangeLayer(Renderer renderer)
    {
        _renderers.Remove(renderer);
        AddRenderer(renderer, false);

        ReRender(renderer.Bounds);
    }

    internal static void OnRendererMoved(Renderer renderer, Vector lastPosition, Vector position)
    {
        Bounds lastBounds = new Bounds(lastPosition + renderer.LocalPosition, renderer.Bounds.Size);
        Bounds newBounds = new Bounds(position + renderer.LocalPosition, renderer.Bounds.Size);
        ReRender(lastBounds, newBounds);
    }

    internal static void ReRender(params Bounds[] boundsArr)
    {
        HashSet<(int, int)> rendered = new HashSet<(int, int)>();

        foreach (Bounds bounds in boundsArr)
        {
            for (int i = 0; i < bounds.Size.X; i++)
            {
                for (int j = 0; j < bounds.Size.Y; j++)
                {
                    int x = i + (int)bounds.Position.X;
                    int y = j + (int)bounds.Position.Y;

                    bool added = rendered.Add((x, y));
                    if (!added)
                        continue;

                    Renderer? renderer = _renderers.Where(r => r != null && r.Bounds.IsInBounds(x, y) && r.CurrentCharAtGlobal(x, y) != '\0').FirstOrDefault();

                    if (renderer == null)
                    {
                        _screenBuffer.ClearBuffer(x, y);
                        continue;
                    }

                    PixelData pixelData = renderer.CurrentPixelDataAtGlobal(x, y);
                    _screenBuffer.UpdatePixel(x, y, pixelData);
                }
            }
        }
    }
}
