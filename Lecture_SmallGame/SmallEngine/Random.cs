using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine;

internal static class Random
{
    // CHECK Is is better to init here on this line or in the static ctor?
    private static readonly System.Random _random;

    static Random()
    {
        _random = new System.Random();
    }

    public static float Value => (float)_random.NextDouble();

    internal static int Next(int max)
    {
        return _random.Next(0, max);
    }
    internal static int Next(int min, int max)
    {
        return _random.Next(min, max);
    }
}
