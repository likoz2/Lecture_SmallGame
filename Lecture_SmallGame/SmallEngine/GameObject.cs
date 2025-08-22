using Lecture_SmallGame.SmallEngine.Components;

namespace Lecture_SmallGame.SmallEngine;

// Both GameObject and Component inherited from EngineObject.. but I didnt find a usecase for that so the dont inherit it anymore..
public class GameObject
{
    public Transform Transform { get; init; }

    private readonly List<Component> _components = new List<Component>();

    public GameObject()
    {
        Transform = AddComponent<Transform>();
    }

    public T AddComponent<T>() where T : Component
    {
        T component = (T)Activator.CreateInstance(typeof(T), this)!;

        _components.Add(component);

        if (component is Renderer renderer)
        {
            Writer.AddRenderer(renderer);
        }
        else if (component is Collider collider)
        {
            Physics.AddCollider(collider);
        }

        return component;
    }

    public T? GetComponent<T>() where T : Component
    {
        return (T?)_components.FirstOrDefault(x => x.GetType() == typeof(T));
    }

    public IEnumerable<T> GetComponents<T>() where T : Component
    {
        return _components.Where(x => x is T).Cast<T>();
    }

    public virtual void OnTriggerStay(Collider other) { }
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }
    public virtual void Update() { }
    public virtual void OnKeyPressed(ConsoleKeyInfo key) { }
}