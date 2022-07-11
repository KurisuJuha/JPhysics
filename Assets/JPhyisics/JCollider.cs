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
        /// �Փ˔���Ɏg�p����g���C�A���O���̃��X�g
        /// </summary>
        [NonSerialized]
        public List<Triangle> Triangles = new List<Triangle>();

        /// <summary>
        /// �Փ˔���Ɏg�p����g���C�A���O���̃��X�g�̈ʒu�ɃI�u�W�F�N�g�̃X�P�[���A�ʒu��Ή����������X�g
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
        /// �h���N���X�̒l��Triangles�ɃZ�b�g���܂��B
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