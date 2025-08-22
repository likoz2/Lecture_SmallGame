
namespace Lecture_SmallGame.SmallEngine.Components;

/// <summary>
/// A base class that allows <see cref="Component"/> behavior. Every <see cref="Component"/> must inherit from this class.
/// </summary>
public abstract class Component
{
    public Transform Transform { get; init; }
    public GameObject GameObject { get; init; }

    public Component(GameObject gameObject)
    {
        GameObject = gameObject;
        Transform = gameObject.Transform;
    }
}
