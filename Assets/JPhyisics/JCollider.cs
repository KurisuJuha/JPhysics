using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    [DefaultExecutionOrder(100)]
    public abstract class JCollider : MonoBehaviour
    {
        [NonSerialized]
        public List<Triangle> Triangles = new List<Triangle>();

        private List<Triangle> Triangles_N
        {
            get
            {
                List<Triangle> tris = new List<Triangle>();

                for (int i = 0; i < Triangles.Count; i++)
                {
                    Triangle tri = new Triangle();

                    Vector3 scale = transform.localScale;
                    Vector3 pos = transform.position;

                    tri.one = Triangles[i].one * scale + (Vector2)pos;
                    tri.two = Triangles[i].two * scale + (Vector2)pos;
                    tri.thr = Triangles[i].thr * scale + (Vector2)pos;

                    tris.Add(tri);
                }

                return tris;
            }
        }

        private void OnDrawGizmosSelected()
        {
            ChangeValue();
            if (Triangles is { Count: > 0 })
            {
                foreach (var triangle in Triangles_N)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(triangle.one, triangle.two);
                    Gizmos.DrawLine(triangle.thr, triangle.two);
                    Gizmos.DrawLine(triangle.one, triangle.thr);
                }   
            }
        }

        public virtual void ChangeValue()
        {
            
        }

        private void LateUpdate()
        {
            ChangeValue();
        }
    }
}