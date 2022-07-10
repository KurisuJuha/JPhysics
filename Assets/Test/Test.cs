using System.Collections;
using System.Collections.Generic;
using JuhaKurisu.JPhysics;
using UnityEngine;

public class Test : MonoBehaviour
{
    public JCollider JTriangleCollider;

    public JCollider JTriangleCollider2;

    public GameObject object1;
    public GameObject object2;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(JTriangleCollider.Triangles_N[0].one + "," + JTriangleCollider2.Triangles_N[0].one);
        }

        if (JPhysics.ColliderDetection(JTriangleCollider, JTriangleCollider2))
        {
            Debug.Log("c");
        }

        JCollision[] col = JPhysics.ObjectDetection(object1, object2);
        if (col.Length > 0)
        {
            Debug.Log(col[0].gameObject.name + "," + col.Length);
        }
    }
}
