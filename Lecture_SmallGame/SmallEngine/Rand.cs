
namespace Lecture_SmallGame.SmallEngine;

public static class Rand
{
    // CHECK Is is better to init here on this line or in the static ctor?
    private static readonly System.Random _random;

    static Rand()
    {
        _random = new System.Random();
    }

    public static float Value => (float)_random.NextDouble();

    public static int Next(int max)
    {
        return _random.Next(0, max);
    }
    public static int Next(int min, int max)
    {
        return _random.Next(min, max);
    }
}
