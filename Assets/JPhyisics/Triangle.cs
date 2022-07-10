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
        /// ��ڂ̒��_
        /// </summary>
        public Vector2 one;

        /// <summary>
        /// ��ڂ̒��_
        /// </summary>
        public Vector2 two;

        /// <summary>
        /// �O�ڂ̒��_
        /// </summary>
        public Vector2 thr;

        public override int GetHashCode()
        {

            return one.GetHashCode() ^ two.GetHashCode() ^ thr.GetHashCode();
        }
    }
}