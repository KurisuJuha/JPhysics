using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    [DefaultExecutionOrder(100)]
    public class JBoxCollider : JCollider
    {
        public Vector2 position;
        public Vector2 size;

        public override void ChangeValue()
        {
            Vector2 pos1 = position + new Vector2(size.x / -2f, size.y / -2f); //左下
            Vector2 pos2 = position + new Vector2(size.x / -2f, size.y / 2f); //左上
            Vector2 pos3 = position + new Vector2(size.x / 2f, size.y / 2f); //右上
            Vector2 pos4 = position + new Vector2(size.x / 2f, size.y / -2f); //右下

            Triangle tri1 = new Triangle();
            tri1.one = pos1;
            tri1.two = pos2;
            tri1.thr = pos3;
            
            Triangle tri2 = new Triangle();
            tri2.one = pos1;
            tri2.two = pos3;
            tri2.thr = pos4;
            
            
            if (Triangles.Count > 0)
            {
                Triangles[0] = tri1;
            }
            else
            {
                Triangles.Add(tri1);
            }

            if (Triangles.Count > 1)
            {
                Triangles[1] = tri2;
            }
            else
            {
                Triangles.Add(tri2);
            }
        }
    }
}