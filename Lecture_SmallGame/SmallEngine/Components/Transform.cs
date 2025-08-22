using System.Collections;

namespace Lecture_SmallGame.SmallEngine.Components;

/// <summary>
/// A <<see cref="Component"/>> that manages the Position of the GameObject as well as its name and children.
/// </summary>
public class Transform : Component, IEnumerable<Transform>
{
    private readonly List<Transform> _children = new List<Transform>();
    public string Name { get; set; }

    public Vector Position
    {
        get;
        set
        {
            Vector lastPosition = Transform.Position;
            field = value;

            // TODO Waiting for the double buffer feature to tidy this up.
            GameObject.GetComponents<Renderer>().ToList().ForEach(x => Writer.OnRendererMoved(x, lastPosition, Transform.Position));
            GameObject.GetComponents<Renderer>().ToList().ForEach(x => x.IsDirty = true);
        }
    }

    public Transform(GameObject gameObject) : base(gameObject)
    {
        Transform ??= this;
    }

    public IEnumerator<Transform> GetEnumerator()
    {
        yield return this;

        foreach (Transform child in _children)
        {
            foreach (Transform transform in child)
            {
                yield return transform;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    internal void AddChild(Transform child)
    {
        _children.Add(child);
    }
}
