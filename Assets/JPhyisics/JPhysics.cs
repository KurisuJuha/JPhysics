using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    [DefaultExecutionOrder(200)]
     public class JPhysics : MonoBehaviour
     {
         
         public List<Vector2> Velocities = new List<Vector2>(512);
     
         public List<float> AngularVelocities = new List<float>(512);
     
         public Vector2 Velocity
         {
             get
             {
                 Vector2 v = Vector2.zero;
                 
                 foreach (var velo in Velocities)
                 {
                     v += velo;
                 }
     
                 return v;
             }
         }
     
         public float AngularVelocity
         {
             get
             {
                 float v = 0;
                 
                 foreach (var velo in AngularVelocities)
                 {
                     v += velo;
                 }
     
                 return v;
             }
         }
     
     
         // Start is called before the first frame update
         void Awake()
         {
             for (int i = 0; i < 512; i++)
             {
                 Velocities.Add(Vector2.zero);
                 AngularVelocities.Add(0);
             }
         }
     
         // Update is called once per frame
         void LateUpdate()
         {
             transform.localPosition += (Vector3)Velocity * Time.deltaTime;
             transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, AngularVelocity * Time.deltaTime));
         }
     
         public void AddForce(Vector2 power, int layer)
         {
             Velocities[layer] += power * Time.deltaTime;
         }

         private static Vector2 sub_vector(Vector2 a, Vector2 b)
         {
             Vector2 ret;
             
             ret.x = a.x - b.x;
             ret.y = a.y - b.y;

             return ret;
         }

         public static bool Tri_PointDetection(Triangle triangle, Vector2 point)
         {
             Vector2 ab = sub_vector(triangle.two, triangle.one);
             Vector2 bp = sub_vector(point, triangle.two);

             Vector2 bc = sub_vector(triangle.thr, triangle.two);
             Vector2 cp = sub_vector(point, triangle.thr);

             Vector2 ca = sub_vector(triangle.one, triangle.thr);
             Vector2 ap = sub_vector(point, triangle.one);

             float c1 = ab.x * bp.y - ab.y * bp.x;
             float c2 = bc.x * cp.y - bc.y * cp.x;
             float c3 = ca.x * ap.y - ca.y * ap.x;

             if ((c1 > 0 && c2 > 0 && c3 > 0) || (c1 < 0 && c2 < 0 && c3 < 0))
             {
                 return true;
             }

             return false;
         }
         
         public static bool Tri_TriDetection(Triangle triangle1, Triangle triangle2)
         {
             bool ret = Tri_PointDetection(triangle1, triangle2.one);
             ret = ret || Tri_PointDetection(triangle1, triangle2.two);
             ret = ret || Tri_PointDetection(triangle1, triangle2.thr);

             if (!ret)
             {
                 
                 ret = Tri_PointDetection(triangle2, triangle1.one);
                 ret = ret || Tri_PointDetection(triangle2, triangle1.two);
                 ret = ret || Tri_PointDetection(triangle2, triangle1.thr);
             }
             
             return ret;
         }
         
         public static bool CollisionDetection(JCollider jCollider1,JCollider jCollider2)
         {
             List<Triangle> tris1 = jCollider1.Triangles_N;
             List<Triangle> tris2 = jCollider2.Triangles_N;

             bool ret = false;
             
             for (int i1 = 0; i1 < tris1.Count; i1++)
             {
                 for (int i2 = 0; i2 < tris2.Count; i2++)
                 {
                     ret = Tri_TriDetection(tris1[i1], tris2[i2]);
                     
                     // もしretがtrueならもう計算する必要が無いので抜ける
                     if (ret)
                     {
                         break;
                     }
                 }
                 // もしretがtrueならもう計算する必要が無いので抜ける
                 if (ret)
                 {
                     break;
                 }
             }
             
             return ret;
         }
     }
}