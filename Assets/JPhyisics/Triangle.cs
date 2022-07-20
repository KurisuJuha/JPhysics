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

        /// <summary>
        /// トライアングルの面積を算出できます。
        /// </summary>
        /// <returns>トライアングルの面積
        /// </returns>
        public float TriangleArea()
        {
            return TriangleArea(one, two, thr);
        }

        /// <summary>
        /// トライアングルの面積を算出できます。
        /// </summary>
        /// <param name="one">一つ目の座標</param>
        /// <param name="two">二つ目の座標</param>
        /// <param name="thr">三つ目の座標</param>
        /// <returns>トライアングルの面積</returns>
        public static float TriangleArea(Vector2 one, Vector2 two, Vector2 thr)
        {
            float ln1 = Vector2.Distance(one, two);
            float ln2 = Vector2.Distance(two, thr);
            float ln3 = Vector2.Distance(thr, one);

            // ヘロンの公式

            float s1 = (ln1 + ln2 + ln3) / 2f;
            float s2 = s1 * (s1 - ln1) * (s1 - ln2) * (s1 - ln3);

            float result = Mathf.Sqrt(s2);

            return result;
        }
    }
}