using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine;

internal struct Vector(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;

    public readonly float Magnitude => MathF.Sqrt(X * X + Y * Y);
    public readonly Vector Normalized => this / (MathF.Abs(X) + Math.Abs(Y));

    public static Vector Up => new Vector(0, -1);
    public static Vector Down => new Vector(0, 1);
    public static Vector Left => new Vector(-1, 0);
    public static Vector Right => new Vector(1, 0);

    public static Vector operator +(Vector v, Vector u)
    {
        return new Vector(v.X + u.X, v.Y + u.Y);
    }

    public static Vector operator -(Vector v, Vector u)
    {
        return new Vector(v.X - u.X, v.Y - u.Y);
    }

    public static Vector operator *(Vector v, float f)
    {
        return new Vector(v.X * f, v.Y * f);
    }

    public static Vector operator /(Vector v, float f)
    {
        return new Vector(v.X / f, v.Y / f);
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}
