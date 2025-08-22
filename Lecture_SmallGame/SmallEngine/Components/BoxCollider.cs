
namespace Lecture_SmallGame.SmallEngine.Components;

public class BoxCollider : Collider
{
    public Vector Size { get; set { field = value; } }
    public Bounds Bounds => new Bounds(Transform.Position, Size);

    public BoxCollider(GameObject gameObject) : base(gameObject)
    {
    }

    public override bool Intersects(Collider other)
    {
        if (other is BoxCollider box)
        {
            return Bounds.Intersects(box.Bounds);
        }

        return false;
    }
}
