using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine.Components;

internal abstract class Component
{
    public Transform Transform { get; init; }
    public GameObject GameObject { get; init; }

    public Component(GameObject gameObject)
    {
        GameObject = gameObject;
        Transform = gameObject.Transform;
    }
}
