using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuhaKurisu.JPhysics
{
    public class JCollision
    {
        /// <summary>
        /// �Փ˂����I�u�W�F�N�g
        /// </summary>
        public GameObject gameObject;

        /// <summary>
        /// �Փ˂�����ڂ̃I�u�W�F�N�g
        /// </summary>
        public GameObject _gameObject;

        /// <summary>
        /// �Փ˂����I�u�W�F�N�g�̃g�����X�t�H�[��
        /// </summary>
        public Transform transform;

        /// <summary>
        /// �Փ˂�����ڂ̃I�u�W�F�N�g�̃g�����X�t�H�[��
        /// </summary>
        public Transform _transform;

        /// <summary>
        /// �Փ˂���JPhysics
        /// </summary>
        public JPhysics JPhysics;

        /// <summary>
        /// �Փ˂�����ڂ�JPhysics
        /// </summary>
        public JPhysics _JPhysics;

        /// <summary>
        /// �Փ˂����R���C�_�[
        /// </summary>
        public JCollider JCollider;

        /// <summary>
        /// �Փ˂�����ڂ̃R���C�_�[
        /// </summary>
        public JCollider _JCollider;
    }
}