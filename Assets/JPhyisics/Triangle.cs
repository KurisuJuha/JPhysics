using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    [Serializable]
    public struct Triangle
    {
        public Vector2 one;
        public Vector2 two;
        public Vector2 thr;

        public override bool Equals(object obj)
        {
            bool ret = true;

            if (obj != null)
            {
                Triangle tri = (Triangle)obj;
                ret = tri.one == one;
                ret = ret && tri.two == two;
                ret = ret && tri.thr == thr;
                return ret;
            }

            return false;
        }
    }
}