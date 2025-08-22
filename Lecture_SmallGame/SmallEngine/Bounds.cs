
namespace Lecture_SmallGame.SmallEngine;

/// <summary>
/// Boundaries for <see cref="Components.Collider"/>, <see cref="Components.Renderer"/> and so on.
/// </summary>
/// <param name="Position">Where the <see cref="Bounds"/> starts.</param>
/// <param name="Size">Size of the <see cref="Bounds"/>.</param>
public struct Bounds(Vector Position, Vector Size)
{
    /// <summary>
    /// Position where the <see cref="Bounds"/> starts.
    /// </summary>
    public Vector Position { get; init; } = Position;
    /// <summary>
    /// Size of the <see cref="Bounds"/>.
    /// </summary>
    public Vector Size { get; init; } = Size;
    /// <summary>
    /// Position where the <see cref="Bounds"/> ends.
    /// </summary>
    public Vector SecondPosition { get; init; } = Position + Size;
    /// <summary>
    /// Width of the <see cref="Bounds"/>
    /// </summary>
    public readonly float Width => Size.X;
    /// <summary>
    /// Height of the <see cref="Bounds"/>
    /// </summary>
    public readonly float Height => Size.Y;

    /// <summary>
    /// Calculate whether the specified <see cref="Vector"/> is in the <see cref="Bounds"/>.
    /// </summary>
    /// <param name="position"></param>
    /// <returns><see langword="true"/> when the <see cref="Vector"/> is inside of the <see cref="Bounds"/>.<br></br>
    /// <see langword="false"/> when the <see cref="Vector"/> is outside of the <see cref="Bounds"/>.</returns>
    public bool IsInBounds(Vector position) => IsInBounds((int)position.X, (int)position.Y);

    /// <summary>
    /// Calculate whether the specified <see cref="Vector"/> is in the <see cref="Bounds"/>.
    /// </summary>
    /// <param name="position"></param>
    /// <returns><see langword="true"/> when the <see cref="Vector"/> is inside of the <see cref="Bounds"/>.<br></br>
    /// <see langword="false"/> when the <see cref="Vector"/> is outside of the <see cref="Bounds"/>.</returns>
    public bool IsInBounds(int x, int y)
    {
        return x >= Position.X && y >= Position.Y && x < SecondPosition.X && y < SecondPosition.Y;
    }

    /// <summary>
    /// Checks whether the <see cref="Bounds"/> intersects with other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other"></param>
    /// <returns><see langword="true"/> if the <see cref="Bounds"/> intersect and <see langword="false"/> if they don't.</returns>
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

