using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    [DefaultExecutionOrder(100)]
    public class JTriangleCollider : JCollider
    {
        public Triangle triangle;

        public override void ChangeValue()
        {
            if (Triangles.Count > 0)
            {
                Triangles[0] = triangle;
            }
            else
            {
                Triangles.Add(triangle);
            }
        }
    }
}