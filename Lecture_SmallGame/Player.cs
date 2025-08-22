using Lecture_SmallGame.SmallEngine;
using Lecture_SmallGame.SmallEngine.Components;

namespace Lecture_SmallGame;

internal class Player : GameObject
{
    public readonly TextRenderer _coinText;

    private readonly RigidBody _rb;

    private int _coins = 0;

    public Player()
    {
        _rb = AddComponent<RigidBody>();
        BoxCollider collider = GetComponent<BoxCollider>()!;
        collider.Size = new Vector(3, 3);

        SpriteRenderer renderer = AddComponent<SpriteRenderer>();
        renderer.Sprite = new Sprite([" o ", "\\|/", "/ \\"]);

        _coinText = AddComponent<TextRenderer>();
        _coinText.LocalPosition = new Vector(0, -2);
        _coinText.Layer = 0;
        _coinText.ColorTopLeft = Color.Yellow;
        _coinText.ColorTopRight = Color.Red;

    }

    public override void OnKeyPressed(ConsoleKeyInfo cki)
    {
        if (cki.Key == ConsoleKey.W)
        {
            _rb.Move(Vector.Up);
        }
        else if (cki.Key == ConsoleKey.S)
        {
            _rb.Move(Vector.Down);
        }
        else if (cki.Key == ConsoleKey.A)
        {
            _rb.Move(Vector.Left);
        }
        else if (cki.Key == ConsoleKey.D)
        {
            _rb.Move(Vector.Right);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.GameObject is Coin coin)
        {
            _coins++;
            Engine.Destroy(coin);
            _coinText.Text = "C: " + _coins;
        }
    }
}
