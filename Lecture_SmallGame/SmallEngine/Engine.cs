using Lecture_SmallGame.SmallEngine.Components;

namespace Lecture_SmallGame.SmallEngine;

public static class Engine
{
    static readonly List<GameObject> _gameObjects = new List<GameObject>();
    const int _tickRate = 30;

    /// <summary>
    /// The Root object in the hierarchy.
    /// </summary>
    public static Transform Root { get; private set; }

    static Engine()
    {
        Root = Instantiate<GameObject>().GetComponent<Transform>()!;
        Root.Name = "Root";
        Task.Run(GameLoop);
        Task.Run(InputLoop);
    }

    private static void GameLoop()
    {
        while (true)
        {
            // TODO This waits here for the double buffer and rerender on frame feature.
            //gameObjects.SelectMany(x => x.GetComponents<Renderer>()).ToList().ForEach(x => x.Render());
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update();
            }

            Writer.ReWrite();

            Task.Delay(1000 / _tickRate).Wait();
        }
    }

    private static void InputLoop()
    {
        ConsoleKeyInfo key;
        while (true)
        {
            key = Console.ReadKey(true);
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].OnKeyPressed(key);
            }
        }
    }

    /// <summary>
    /// Creates a new instance of the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>Should be used instead of just calling the parameterless constructor.</remarks>
    /// <typeparam name="T">The type of object to instantiate. Must be a subclass of <see cref="GameObject"/> and have a parameterless
    /// constructor.</typeparam>
    /// <returns>A new instance of <typeparamref name="T"/>.</returns>
    public static T Instantiate<T>() where T : GameObject, new() => Instantiate<T>(new Vector());

    /// <summary>
    /// Creates a new instance of the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>Should be used instead of just calling the parameterless constructor.</remarks>
    /// <typeparam name="T">The type of object to instantiate. Must be a subclass of <see cref="GameObject"/> and have a parameterless
    /// constructor.</typeparam>
    /// <param name="position">The position where the new object will be instantiated.</param>
    /// <returns>A new instance of type <typeparamref name="T"/> positioned at the specified location.</returns>
    public static T Instantiate<T>(Vector position) where T : GameObject, new() => Instantiate<T>(position, Root);

    /// <summary>
    /// Creates a new instance of the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>Should be used instead of just calling the parameterless constructor.</remarks>
    /// <typeparam name="T">The type of object to instantiate. Must be a subclass of <see cref="GameObject"/> and have a parameterless
    /// constructor.</typeparam>
    /// <param name="position">The position where the new object will be instantiated.</param>
    /// <param name="parent">The parent <see cref="Transform"/> to which the new object's transform will be added. If <see langword="null"/>,
    /// the parent will be a set to the Root <see cref="Transform"/>.</param>
    public static T Instantiate<T>(Vector position, Transform parent) where T : GameObject, new()
    {
        T gameObject = new T();
        gameObject.Transform.Position = position;

        parent?.AddChild(gameObject.Transform); // parent is null when creating ROOT Transform, probably a better approach exists

        _gameObjects.Add(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Destroys the gameobject and all its components. Destroys children first.
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Destroy(GameObject gameObject)
    {
        foreach (Transform child in gameObject.Transform)
        {
            if (child.GameObject != gameObject)
                Destroy(child.GameObject);
        }

        DestroyObject(gameObject);
    }

    private static void DestroyObject(GameObject gameObject)
    {
        gameObject.GetComponents<TextRenderer>().ToList().ForEach(Writer.RemoveRenderer);
        gameObject.GetComponents<SpriteRenderer>().ToList().ForEach(Writer.RemoveRenderer);
        gameObject.GetComponents<BoxCollider>().ToList().ForEach(Physics.RemoveCollider);

        _gameObjects.Remove(gameObject);
    }
}
