using System.Collections;
using System.Collections.Generic;
using JuhaKurisu.JPhysics;
using UnityEngine;

public class Test : MonoBehaviour
{
    public JCollider JTriangleCollider;

    public JCollider JTriangleCollider2;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(JTriangleCollider.Triangles_N[0].one + "," + JTriangleCollider2.Triangles_N[0].one);
        }

        if (JPhysics.CollisionsDetection(JTriangleCollider, JTriangleCollider2))
        {
            Debug.Log("c");
        }
    }
}
