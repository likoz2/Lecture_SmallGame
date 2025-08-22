using Lecture_SmallGame.SmallEngine.Components;

namespace Lecture_SmallGame.SmallEngine;

public static class Engine
{
    static readonly List<GameObject> _gameObjects = new List<GameObject>();
    const int _tickRate = 30;

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

    public static T Instantiate<T>() where T : GameObject, new() => Instantiate<T>(new Vector());

    public static T Instantiate<T>(Vector position) where T : GameObject, new() => Instantiate<T>(position, Root);

    public static T Instantiate<T>(Vector position, Transform parent) where T : GameObject, new()
    {
        T gameObject = new T();
        gameObject.Transform.Position = position;

        parent?.AddChild(gameObject.Transform); // parent is null when creating ROOT Transform, probably a better approach exists

        _gameObjects.Add(gameObject);

        return gameObject;
    }

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
