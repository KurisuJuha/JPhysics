using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
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
         void Update()
         {
             transform.localPosition += (Vector3)Velocity * Time.deltaTime;
             transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, AngularVelocity * Time.deltaTime));
         }
     
         public void AddForce(Vector2 power, int layer)
         {
             Velocities[layer] += power * Time.deltaTime;
         }
     }
}