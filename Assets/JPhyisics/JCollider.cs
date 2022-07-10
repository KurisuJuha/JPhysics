using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    [DefaultExecutionOrder(100)]
    public abstract class JCollider : MonoBehaviour
    {
        /// <summary>
        /// 衝突判定に使用するトライアングルのリスト
        /// </summary>
        [NonSerialized]
        public List<Triangle> Triangles = new List<Triangle>();

        /// <summary>
        /// 衝突判定に使用するトライアングルのリストの位置にオブジェクトのスケール、位置を対応させたリスト
        /// </summary>
        public List<Triangle> Triangles_N
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

        private void OnDrawGizmos()
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

        /// <summary>
        /// aaa
        /// </summary>
        public virtual void ChangeValue()
        {
            
        }

        private void LateUpdate()
        {
            ChangeValue();
        }
    }
}