using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JuhaKurisu.JVector;

namespace JuhaKurisu.JPhysics
{
    [DefaultExecutionOrder(100), RequireComponent(typeof(JPhysics))]
    public abstract class JCollider : MonoBehaviour
    {
        [Header("Mass")]
        public bool AutoMass = true;

        public float Mass;

        [Header("CenterOfMass")]
        public bool AutoCenterOfMass = true;

        public Vector2 CenterOfMass;

        /// <summary>
        /// 重心に絶対座標を対応させたベクトル
        /// </summary>
        [NonSerialized]
        public Vector2 CenterOfMass_N;

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
                    Quaternion rot = transform.rotation;

                    tri.one = Triangles[i].one;
                    tri.two = Triangles[i].two;
                    tri.thr = Triangles[i].thr;

                    tri.one = tri.one * scale;
                    tri.two = tri.two * scale;
                    tri.thr = tri.thr * scale;

                    tri.one = rot * tri.one;
                    tri.two = rot * tri.two;
                    tri.thr = rot * tri.thr;
                    
                    tri.one = tri.one + (Vector2)pos;
                    tri.two = tri.two + (Vector2)pos;
                    tri.thr = tri.thr + (Vector2)pos;

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
        /// 派生クラスの値をTrianglesにセットします。
        /// </summary>
        public virtual void ChangeValue()
        {
            
        }

        private void LateUpdate()
        {
            if (AutoMass)
            {
                Mass = 0;
                foreach (var item in Triangles_N)
                {
                    Mass += item.TriangleArea();
                }
            }

            if (AutoCenterOfMass)
            {
                Vector2 a = new Vector2();
                float amass = 0;

                for (int i = 0; i < Triangles.Count; i++)
                {
                    Vector2 b = (Triangles[i].one + Triangles[i].two + Triangles[i].thr) / 3f;

                    if (i == 0)
                    {
                        amass = Triangles[i].TriangleArea();
                        a = b;
                    }
                    else
                    {
                        a = amass * (a + b) / (amass + Triangles[i].TriangleArea());
                    }
                }

                CenterOfMass = a;
            }

            CenterOfMass_N = CenterOfMass;
            CenterOfMass_N *= transform.localScale;
            CenterOfMass_N = transform.rotation * CenterOfMass_N;
            CenterOfMass_N += (Vector2)transform.position;

            ChangeValue();
        }
    }
}