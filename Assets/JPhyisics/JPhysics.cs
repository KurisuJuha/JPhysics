using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JuhaKurisu.JPhysics
{
    [DefaultExecutionOrder(200), DisallowMultipleComponent]
    public class JPhysics : MonoBehaviour
    {
        /// <summary>
        /// スタティックかどうか
        /// スタティックの場合毎フレームの処理は発生せず、当たり判定のみ機能します。
        /// </summary>
        public bool isStatic;

        [Header("Mass")]

        /// <summary>
        /// 質量を自動で設定します
        /// </summary>
        public bool AutoMass = true;

        /// <summary>
        /// 質量
        /// </summary>
        public float Mass;

        [Header("CenterOfMass")]
        /// <summary>
        /// 重心を自動で設定します
        /// </summary>
        public bool AutoCenterOfMass = true;

        /// <summary>
        /// 重心
        /// </summary>
        public Vector2 CenterOfMass;

        /// <summary>
        /// 絶対座標の重心
        /// </summary>
        public Vector2 CenterOfMass_N;

        [Header("Velocity")]
        /// <summary>
        /// 移動の強さのリスト
        /// </summary>
        public List<Vector2> Velocities = new List<Vector2>(512);

        /// <summary>
        /// 回転の強さのリスト
        /// </summary>
        public List<float> AngularVelocities = new List<float>(512);

        /// <summary>
        /// Velocitiesを足し合わせた移動の強さ
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                Vector2 v = Vector2.zero;

                foreach (var velo in Velocities)
                {
                    v += velo;
                }

                return v;
            }
        }

        /// <summary>
        /// AngularVelocitiesを足し合わせた回転の強さ
        /// </summary>
        public float AngularVelocity
        {
            get
            {
                float v = 0;

                foreach (var velo in AngularVelocities)
                {
                    v += velo;
                }

                return v;
            }
        }

        /// <summary>
        /// シーンにあるJPhysicsのリスト
        /// </summary>
        public static List<JPhysics> JPhysicsList = new List<JPhysics>();

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere((Vector3)CenterOfMass_N, 0.1f);
        }

        private void Awake()
        {
            for (int i = 0; i < 512; i++)
            {
                Velocities.Add(Vector2.zero);
                AngularVelocities.Add(0);
            }

            JPhysicsList.Add(this);
        }

        private void LateUpdate()
        {
            if (!isStatic)
            {
                if (AutoMass)
                {
                    // 質量を算出
                    float mass = 0;

                    foreach (var item in GetColliders())
                    {
                        mass += item.Mass;
                    }

                    Mass = mass;
                }

                if (AutoCenterOfMass)
                {
                    // 重心を算出
                    Vector2 a = new Vector2();
                    float amass = 0;

                    var cols = GetColliders();
                    
                    for (int i = 0; i < cols.Length; i++)
                    {
                        Vector2 b = cols[i].CenterOfMass;

                        if (i == 0)
                        {
                            amass = cols[i].Mass;
                            a = b;
                        }
                        else
                        {
                            a = amass * (a + b) / (amass + cols[i].Mass);
                        }
                    }

                    CenterOfMass = a;

                    CenterOfMass_N = CenterOfMass;
                    CenterOfMass_N *= transform.localScale;
                    CenterOfMass_N = transform.rotation * CenterOfMass_N;
                    CenterOfMass_N += (Vector2)transform.position;
                }

                transform.localPosition += (Vector3)Velocity * Time.deltaTime;
                transform.RotateAround(CenterOfMass_N, Vector3.forward, AngularVelocity * Time.deltaTime);
            }
        }

        /// <summary>
        /// 指定したレイヤーのvelocityに指定した強さの力を加えることができます。
        /// </summary>
        /// <param name="power">与える強さ</param>
        /// <param name="layer">与える対象のレイヤー</param>
        public void AddForce(Vector2 power, int layer)
        {
            Velocities[layer] += power * Time.deltaTime;
        }

        /// <summary>
        /// このゲームオブジェクトにアタッチされているJColliderを継承しているコンポーネントを取得できます。
        /// </summary>
        /// <returns>取得したコライダーの配列</returns>
        public JCollider[] GetColliders()
        {
            return GetColliders(gameObject);
        }

        /// <summary>
        /// ゲームオブジェクトにアタッチされているJColliderを継承しているコンポーネントを取得できます。
        /// </summary>
        /// <param name="gameObject">取得の対象のゲームオブジェクト</param>
        /// <returns>取得したコライダーの配列</returns>
        public static JCollider[] GetColliders(GameObject gameObject)
        {
            return gameObject.GetComponents<JCollider>();
        }

        /// <summary>
        /// 一つのトライアングルと一つの点が衝突しているかどうかを判定できます。
        /// </summary>
        /// <param name="triangle">一つ目のトライアングル</param>
        /// <param name="point">一つ目の点</param>
        /// <returns>triangleとpointが衝突しているかどうか</returns>
        public static bool Tri_PointDetection(Triangle triangle, Vector2 point)
        {
            Vector2 ab = sub_vector(triangle.two, triangle.one);
            Vector2 bp = sub_vector(point, triangle.two);

            Vector2 bc = sub_vector(triangle.thr, triangle.two);
            Vector2 cp = sub_vector(point, triangle.thr);

            Vector2 ca = sub_vector(triangle.one, triangle.thr);
            Vector2 ap = sub_vector(point, triangle.one);

            float c1 = ab.x * bp.y - ab.y * bp.x;
            float c2 = bc.x * cp.y - bc.y * cp.x;
            float c3 = ca.x * ap.y - ca.y * ap.x;

            if ((c1 > 0 && c2 > 0 && c3 > 0) || (c1 < 0 && c2 < 0 && c3 < 0))
            {
                return true;
            }

            return false;

            Vector2 sub_vector(Vector2 a, Vector2 b)
            {
                Vector2 ret;

                ret.x = a.x - b.x;
                ret.y = a.y - b.y;

                return ret;
            }
        }

        /// <summary>
        /// 二つのトライアングルが衝突しているかどうかを判定できます。
        /// </summary>
        /// <param name="triangle1">一つ目のトライアングル</param>
        /// <param name="triangle2">二つ目のトライアングル</param>
        /// <returns>二つのトライアングルが衝突しているかどうか</returns>
        public static bool Tri_TriDetection(Triangle triangle1, Triangle triangle2)
        {
            bool ret = Tri_PointDetection(triangle1, triangle2.one);
            ret = ret || Tri_PointDetection(triangle1, triangle2.two);
            ret = ret || Tri_PointDetection(triangle1, triangle2.thr);

            if (!ret)
            {

                ret = Tri_PointDetection(triangle2, triangle1.one);
                ret = ret || Tri_PointDetection(triangle2, triangle1.two);
                ret = ret || Tri_PointDetection(triangle2, triangle1.thr);
            }

            return ret;
        }

        /// <summary>
        /// 二つのコライダーが衝突しているかどうかを判定できます。
        /// </summary>
        /// <param name="jCollider1">一つ目のコライダー</param>
        /// <param name="jCollider2">二つ目のコライダー</param>
        /// <returns>二つのコライダーが衝突しているかどうか</returns>
        public static bool ColliderDetection(JCollider jCollider1, JCollider jCollider2)
        {


            List<Triangle> tris1 = jCollider1.Triangles_N;
            List<Triangle> tris2 = jCollider2.Triangles_N;

            bool ret = false;

            for (int i1 = 0; i1 < tris1.Count; i1++)
            {
                for (int i2 = 0; i2 < tris2.Count; i2++)
                {
                    ret = Tri_TriDetection(tris1[i1], tris2[i2]);

                    // もしretがtrueならもう計算する必要が無いので抜ける
                    if (ret)
                    {
                        break;
                    }
                }
                // もしretがtrueならもう計算する必要が無いので抜ける
                if (ret)
                {
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// オブジェクトが衝突しているかどうかを判定できます。
        /// </summary>
        /// <param name="gameObject">衝突対象のオブジェクト</param>
        /// <returns>衝突しているコライダーの配列</returns>
        public JCollisions ObjectDetection(GameObject gameObject)
        {
            return ObjectDetection(this.gameObject, gameObject);
        }

        /// <summary>
        /// 二つのオブジェクトが衝突しているかどうかを判定できます。
        /// </summary>
        /// <param name="gameObject1">一つ目のオブジェクト</param>
        /// <param name="gameObject2">二つ目のオブジェクト</param>
        /// <returns>衝突しているコライダーの配列</returns>
        public static JCollisions ObjectDetection(GameObject gameObject1, GameObject gameObject2)
        {
            JCollider[] colliders1 = GetColliders(gameObject1);
            JCollider[] colliders2 = GetColliders(gameObject2);
            List<JCollision> collisions = new List<JCollision>();

            foreach (JCollider item in colliders1)
            {
                foreach (JCollider item2 in colliders2)
                {
                    if (ColliderDetection(item, item2))
                    {
                        //JCollisionを作る
                        JCollision collision = new JCollision();

                        //自分の方
                        collision._gameObject = item.gameObject;
                        collision._transform = item.transform;
                        if (item.TryGetComponent(out JPhysics _jPhysics))
                        {
                            collision._JPhysics = _jPhysics;
                        }
                        collision._JCollider = item;

                        //相手の方
                        collision.gameObject = item2.gameObject;
                        collision.transform = item2.transform;
                        if (item2.TryGetComponent(out JPhysics jPhysics))
                        {
                            collision.JPhysics = jPhysics;
                        }
                        collision.JCollider = item2;


                        collisions.Add(collision);
                    }
                }
            }

            return new JCollisions()
            {
                collisions = collisions.ToArray(),
                onCollision = collisions.Count > 0
            };
        }

        /// <summary>
        /// 自身が全てのオブジェクトに対して衝突しているかどうかを判定します。
        /// </summary>
        /// <returns>衝突しているコライダーの配列</returns>
        public JCollisions Detection()
        {
            return Detection(gameObject);
        }

        /// <summary>
        /// 指定されたオブジェクトが全てのオブジェクトに対して衝突しているかどうかを判定します。
        /// </summary>
        /// <param name="gameObject">衝突を検知するオブジェクト</param>
        /// <returns>衝突しているコライダーの配列</returns>
        public static JCollisions Detection(GameObject gameObject)
        {
            List<JCollision> jCollisions = new List<JCollision>();
            foreach (JPhysics jPhysics in JPhysicsList)
            {
                jCollisions.AddRange(ObjectDetection(gameObject, jPhysics.gameObject).collisions);
            }

            return new JCollisions()
            {
                collisions = jCollisions.ToArray(),
                onCollision = jCollisions.Count > 0
            };
        }

        private void OnDestroy()
        {
            JPhysicsList.Remove(this);
        }
    }
}