
namespace Lecture_SmallGame.SmallEngine.Components;

/// <summary>
/// A base <see cref="Component"/> that allows collision handling for inheriting classes.
/// </summary>
public abstract class Collider : Component
{
    public bool IsDirty { get; internal set; }

    public Collider(GameObject gameObject) : base(gameObject)
    {

    }

    public abstract bool Intersects(Collider other);
}
