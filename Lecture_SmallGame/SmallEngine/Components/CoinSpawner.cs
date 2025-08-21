using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine.Components;

internal class CoinSpawner : GameObject
{
    public override void Update()
    {
        if (Random.Value < 0.1f)
            Spawn();
    }

    private void Spawn()
    {
        Vector pos = new(Random.Next(Writer.Width - 3), Random.Next(Writer.Height - 3));
        Coin coin = Engine.Instantiate<Coin>(pos);
        coin.Transform.Name = "Coin";
    }
}
