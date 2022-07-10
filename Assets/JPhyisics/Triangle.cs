using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    [Serializable]
    public struct Triangle
    {
        /// <summary>
        /// 一つ目の頂点
        /// </summary>
        public Vector2 one;

        /// <summary>
        /// 二つ目の頂点
        /// </summary>
        public Vector2 two;

        /// <summary>
        /// 三つ目の頂点
        /// </summary>
        public Vector2 thr;

        public override int GetHashCode()
        {

            return one.GetHashCode() ^ two.GetHashCode() ^ thr.GetHashCode();
        }
    }
}