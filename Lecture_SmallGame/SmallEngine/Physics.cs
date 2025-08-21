using Lecture_SmallGame.SmallEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_SmallGame.SmallEngine;

internal static class Physics
{
    private static readonly List<Collider> _coliders = new List<Collider>();

    internal static void AddCollider(Collider collider)
    {
        _coliders.Add(collider);
    }

    // TODO For now it works only with BoxColliders, add other
    // CHECK What is the correct solution? Do I check like:
    //          if A is BoxCollider and B is BoxCollider => check intersection for 2 boxes
    //          if A is BoxCollider and B is CircleCollider => check intersection for box with circle
    //      and so on? I feel like it is way too much of lines of code :/
    internal static HashSet<Collider> CheckIntersection(RigidBody rigidBody)
    {
        Collider? collider = rigidBody.GameObject.GetComponent<BoxCollider>();
        if (collider == null)
            throw new Exception("RigidBody must have a BoxCollider.");

        return [.. _coliders.Where(x => collider != x && collider.Intersects(x))];
    }

    internal static void RemoveCollider(BoxCollider collider)
    {
        _coliders.Remove(collider);
    }
}
