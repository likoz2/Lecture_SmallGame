
namespace Lecture_SmallGame.SmallEngine.Components;

// TODO IsDirty with trigger colliders being calculated only once per frame instead of every Move()
/// <summary>
/// A <see cref="Component"/> that handles collisions with other <see cref="GameObject"/>s that have a <see cref="Collider"/> attached.
/// </summary>
/// <remarks>This <see cref="Component"/> automatically adds a <see cref="BoxCollider"/> to the <see cref="GameObject"/>.</remarks>
public class RigidBody : Component
{
    readonly HashSet<Collider> _intersectors = new HashSet<Collider>();

    public RigidBody(GameObject gameObject) : base(gameObject)
    {
        gameObject.AddComponent<BoxCollider>();
    }

    // TODO collider checking, continuous collider checking, IsTrigger
    public void Move(Vector vector)
    {
        Transform.Position += vector;

        CheckIntersection();
    }

    private void CheckIntersection()
    {
        HashSet<Collider> newIntersectors = Physics.CheckIntersection(this);

        foreach (Collider c in newIntersectors)
        {
            if (_intersectors.Contains(c))
            {
                GameObject.OnTriggerStay(c); // still intersects
            }
            else
            {
                GameObject.OnTriggerEnter(c); // enter
            }
        }

        List<Collider> exitColls = [.. _intersectors.Where(x => !newIntersectors.Contains(x))];
        exitColls.ForEach(GameObject.OnTriggerExit); // no longer intersects
        exitColls.ForEach(x => _intersectors.Remove(x));

        newIntersectors.ToList().ForEach(x => _intersectors.Add(x));
    }
}
