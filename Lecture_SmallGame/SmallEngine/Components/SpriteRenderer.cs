using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine.Components;

internal class SpriteRenderer : Renderer // TODO double buffer
{
    public Sprite Sprite { get; set { field = value; Writer.ReRender(Bounds); } }
    public override Vector LocalPosition { get; set { field = value; Writer.ReRender(Bounds); } }
    public override Bounds Bounds => new Bounds(Transform.Position + LocalPosition, new Vector(Sprite.Width, Sprite.Height));

    public SpriteRenderer(GameObject gameObject) : base(gameObject)
    {
        Sprite = new Sprite(["XXX", "XXX", "XXX"]);
    }

    public override char CurrentCharAt(int x, int y)
    {
        return Sprite.CharAt(x, y);
    }

    public override PixelData CurrentPixelDataAt(int x, int y)
    {
        return Sprite.PixelDataAt(x, y);
    }

    public override void OnLayerChange(int value)
    {
        Sprite.Layer = value;
    }

    public override char CurrentCharAtGlobal(int x, int y)
    {
        Vector vec = new Vector(x, y) - Transform.Position - LocalPosition;
        return CurrentCharAt((int)vec.X, (int)vec.Y);
    }

    public override PixelData CurrentPixelDataAtGlobal(int x, int y)
    {
        Vector vec = new Vector(x, y) - Transform.Position - LocalPosition;
        return CurrentPixelDataAt((int)vec.X, (int)vec.Y);
    }
}
