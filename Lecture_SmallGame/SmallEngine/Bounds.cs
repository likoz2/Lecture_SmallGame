
namespace Lecture_SmallGame.SmallEngine;

public struct Bounds(Vector Position, Vector Size)
{
    public Vector Position { get; init; } = Position;
    public Vector Size { get; init; } = Size;
    public Vector SecondPosition { get; init; } = Position + Size;
    public readonly float Width => Size.X;
    public readonly float Height => Size.Y;

    public bool IsInBounds(Vector position) => IsInBounds((int)position.X, (int)position.Y);

    public bool IsInBounds(int x, int y)
    {
        return x >= Position.X && y >= Position.Y && x < SecondPosition.X && y < SecondPosition.Y;
    }

    public bool Intersects(Bounds other)
    {
        Vector aPos = Position;
        Vector aSize = Size;
        Vector bPos = other.Position;
        Vector bSize = other.Size;

        bool xOverlap = aPos.X < bPos.X + bSize.X && aPos.X + aSize.X > bPos.X;
        bool yOverlap = aPos.Y < bPos.Y + bSize.Y && aPos.Y + aSize.Y > bPos.Y;

        return xOverlap && yOverlap;
    }
}

