
namespace Lecture_SmallGame.SmallEngine.Components;

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
