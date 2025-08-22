
namespace Lecture_SmallGame.SmallEngine.Components;

public abstract class Collider : Component
{
    public bool IsDirty { get; internal set; }

    public Collider(GameObject gameObject) : base(gameObject)
    {

    }

    public abstract bool Intersects(Collider other);
}
