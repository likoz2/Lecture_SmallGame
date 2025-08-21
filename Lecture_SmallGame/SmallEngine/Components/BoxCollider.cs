using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine.Components;

internal class BoxCollider : Collider
{
    public Vector Size { get; set { field = value; } }
    public Bounds Bounds => new Bounds(Transform.Position, Size);

    public BoxCollider(GameObject gameObject) : base(gameObject)
    {
    }

    internal override bool Intersects(Collider other)
    {
        if (other is BoxCollider box)
        {
            return Bounds.Intersects(box.Bounds);
        }

        return false;
    }
}
