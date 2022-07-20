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

        /// <summary>
        /// �g���C�A���O���̖ʐς��Z�o�ł��܂��B
        /// </summary>
        /// <returns>�g���C�A���O���̖ʐ�
        /// </returns>
        public float TriangleArea()
        {
            return TriangleArea(one, two, thr);
        }

        /// <summary>
        /// �g���C�A���O���̖ʐς��Z�o�ł��܂��B
        /// </summary>
        /// <param name="one">��ڂ̍��W</param>
        /// <param name="two">��ڂ̍��W</param>
        /// <param name="thr">�O�ڂ̍��W</param>
        /// <returns>�g���C�A���O���̖ʐ�</returns>
        public static float TriangleArea(Vector2 one, Vector2 two, Vector2 thr)
        {
            float ln1 = Vector2.Distance(one, two);
            float ln2 = Vector2.Distance(two, thr);
            float ln3 = Vector2.Distance(thr, one);

            // �w�����̌���

            float s1 = (ln1 + ln2 + ln3) / 2f;
            float s2 = s1 * (s1 - ln1) * (s1 - ln2) * (s1 - ln3);

            float result = Mathf.Sqrt(s2);

            return result;
        }
    }
}