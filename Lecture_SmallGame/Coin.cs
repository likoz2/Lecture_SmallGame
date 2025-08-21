using Lecture_SmallGame.SmallEngine;
using Lecture_SmallGame.SmallEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame;

internal class Coin : GameObject
{

    public Coin()
    {
        BoxCollider collider = AddComponent<BoxCollider>();
        SpriteRenderer renderer = AddComponent<SpriteRenderer>();
        renderer.Sprite = new Sprite([" __", "/Co\\", "\\in/", " ˇˇ"]);
        collider.Size = new Vector(renderer.Sprite.Width, renderer.Sprite.Height);
    }
}
