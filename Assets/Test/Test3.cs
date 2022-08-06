using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public Rigidbody2D rd2d;

    void Start()
    {
        rd2d.angularVelocity = 100;
        rd2d.velocity = Vector2.zero;
    }

    void Update()
    {
        
    }
}
