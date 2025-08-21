using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine.Components;

internal abstract class Collider : Component
{
    public bool IsDirty { get; internal set; }

    public Collider(GameObject gameObject) : base(gameObject)
    {

    }

    internal abstract bool Intersects(Collider other);
}
