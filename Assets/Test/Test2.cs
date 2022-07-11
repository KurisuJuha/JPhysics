using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JuhaKurisu.JPhysics;

public class Test2 : MonoBehaviour
{
    public JPhysics JPhysics;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JCollisions cols = JPhysics.Detection();
        if (cols.onCollision)
        {
            Debug.Log(cols.collisions.Length);
        }
    }
}
