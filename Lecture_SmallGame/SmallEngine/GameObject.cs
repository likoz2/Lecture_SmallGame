using Lecture_SmallGame.SmallEngine.Components;

// CHECK Both GameObject and Component inherited from EngineObject.. but I didnt find a usecase for that so they dont inherit it anymore..
namespace Lecture_SmallGame.SmallEngine;


/// <summary>
/// Any in-game object should inherit from this class. This class manages all the necessary funtions.
/// </summary>
/// <remarks>The in-game object should be then only instantiated using <see cref="Engine.Instantiate{T}()"/>.</remarks>
public class GameObject
{
    /// <summary>
    /// The <see cref="Components.Transform"/> component tied to this <see cref="GameObject"/>.
    /// </summary>
    public Transform Transform { get; init; }

    private readonly List<Component> _components = new List<Component>();

    /// <summary>
    /// DO NOT USE this contructor. Use <see cref="Engine.Instantiate{T}()"/> instead.
    /// </summary>
    public GameObject()
    {
        Transform = AddComponent<Transform>();
    }

    /// <summary>
    /// Adds a <see cref="Component"/> to this <see cref="GameObject"/>.
    /// </summary>
    /// <typeparam name="T">Type of the <see cref="Component"/> to be added.</typeparam>
    /// <returns></returns>
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

    /// <summary>
    /// Tries to get a <see cref="Component"/> of the specified type attached to this <see cref="GameObject"/>.
    /// </summary>
    /// <typeparam name="T">Type of the <see cref="Component"/></typeparam>
    /// <returns>The <see cref="Component"/> of the type specified. Returns <see langword="null"/> if not found.</returns>
    public T? GetComponent<T>() where T : Component
    {
        return (T?)_components.FirstOrDefault(x => x.GetType() == typeof(T));
    }

    /// <summary>
    /// Tries to get <see cref="Component"/>s of the specified type attached to this <see cref="GameObject"/>.
    /// </summary>
    /// <typeparam name="T">Type of the <see cref="Component"/></typeparam>
    /// <returns>All the <see cref="Component"/>s of the type specified. Returns an empty <see cref="IEnumerable{T}"/> if not found.</returns>
    public IEnumerable<T> GetComponents<T>() where T : Component
    {
        return _components.Where(x => x is T).Cast<T>();
    }

    /// <summary>
    /// Called when the <see cref="GameObject"/> has a <see cref="Collider"/> attached to it, which is still hitting a <see cref="RigidBody"/> of other <see cref="GameObject"/>.
    /// </summary>
    /// <param name="other">The <see cref="RigidBody"/>'s <see cref="Collider"/> which hit this <see cref="GameObject"/>'s <see cref="Collider"/></param>
    public virtual void OnTriggerStay(Collider other) { }
    /// <summary>
    /// Called when the <see cref="GameObject"/> has a <see cref="Collider"/> attached to it, which hit a <see cref="RigidBody"/> of other <see cref="GameObject"/>.
    /// </summary>
    /// <param name="other">The <see cref="RigidBody"/>'s <see cref="Collider"/> which hit this <see cref="GameObject"/>'s <see cref="Collider"/></param>
    public virtual void OnTriggerEnter(Collider other) { }
    /// <summary>
    /// Called when the <see cref="GameObject"/> has a <see cref="Collider"/> attached to it, which no longer hits a <see cref="RigidBody"/> of other <see cref="GameObject"/>.
    /// </summary>
    /// <param name="other">The <see cref="RigidBody"/>'s <see cref="Collider"/> which hit this <see cref="GameObject"/>'s <see cref="Collider"/></param>
    public virtual void OnTriggerExit(Collider other) { }

    /// <summary>
    /// Called at the start of every frame.
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Called when key is pressed. Keep in mind that console is not capable of registering when user "holds" or "releases" a key.
    /// </summary>
    /// <param name="key">The <see cref="ConsoleKeyInfo"/> that was pressed by the user.</param>
    public virtual void OnKeyPressed(ConsoleKeyInfo key) { }
}