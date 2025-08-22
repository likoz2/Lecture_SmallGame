
using Lecture_SmallGame.SmallEngine;

namespace Lecture_SmallGame;

public class CoinSpawner : GameObject
{
    public override void Update()
    {
        if (Rand.Value < 0.1f)
            Spawn();
    }

    private void Spawn()
    {
        Vector pos = new(Rand.Next(Writer.Width - 3), Rand.Next(Writer.Height - 3));
        Coin coin = Engine.Instantiate<Coin>(pos);
        coin.Transform.Name = "Coin";
    }
}
