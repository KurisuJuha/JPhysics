using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    public class JCollision
    {
        /// <summary>
        /// 衝突したオブジェクト
        /// </summary>
        public GameObject gameObject;

        /// <summary>
        /// 衝突した二つ目のオブジェクト
        /// </summary>
        public GameObject _gameObject;

        /// <summary>
        /// 衝突したオブジェクトのトランスフォーム
        /// </summary>
        public Transform transform;

        /// <summary>
        /// 衝突した二つ目のオブジェクトのトランスフォーム
        /// </summary>
        public Transform _transform;

        /// <summary>
        /// 衝突したJPhysics
        /// </summary>
        public JPhysics JPhysics;

        /// <summary>
        /// 衝突した二つ目のJPhysics
        /// </summary>
        public JPhysics _JPhysics;

        /// <summary>
        /// 衝突したコライダー
        /// </summary>
        public JCollider JCollider;

        /// <summary>
        /// 衝突した二つ目のコライダー
        /// </summary>
        public JCollider _JCollider;
    }
}