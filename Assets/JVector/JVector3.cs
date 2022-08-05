using System;
using UnityEngine;
using UnityEngine.Internal;

namespace JuhaKurisu.JVector
{
    public struct JVector3
    {
        #region メンバー
        
        public double x;
        public double y;
        public double z;

        #endregion

        #region コンストラクター

        public JVector3(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        public JVector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #endregion

        #region プロパティ

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;  
                    case 2: return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid JVector3 index");

                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1: 
                        y = value;
                        break;
                    case 2: 
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid JVector3 index");
                }
            }
        }

        public static JVector3 back
        {
            get
            {
                return new JVector3(0, 0, -1);
            }
        }

        public static JVector3 down
        {
            get
            {
                return new JVector3(0, -1, 0);
            }
        }

        public static JVector3 forward
        {
            get
            {
                return new JVector3(0, 0, 1);
            }
        }

        public static JVector3 left
        {
            get
            {
                return new JVector3(-1, 0, 0);
            }
        }

        public static JVector3 one
        {
            get
            {
                return new JVector3(1, 1, 1);
            }
        }

        public static JVector3 right
        {
            get
            {
                return new JVector3(1, 0, 0);
            }
        }

        public static JVector3 up
        {
            get
            {
                return new JVector3(0, 1, 0);
            }
        }

        public static JVector3 zero
        {
            get
            {
                return new JVector3(0, 0, 0);
            }
        }

        public double sqrMagnitude
        {
            get
            {
                return x * x + y * y + z * z;
            }
        }

        public double magnitude
        {
            get
            {
                return Math.Sqrt(sqrMagnitude);
            }
        }

        public JVector3 normalized
        {
            get
            {
                return Normalize(this);
            }
        }

        #endregion

        #region メソッド

        public static float Angle(JVector3 from, JVector3 to)
        {
            double cos = Dot(from.normalized, to.normalized);
            if (cos < -1) cos = -1;
            if (cos > 1) cos = 1;

            return (float)(Math.Acos(cos) * (180 / Math.PI));
        }

        public static double Dot(JVector3 lhs, JVector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static float AngleBetween(JVector3 from, JVector3 to)
        {
            double cos = Dot(from.normalized, to.normalized);
            if (cos < -1) cos = -1;
            if (cos > 1) cos = 1;
            return (float)Math.Acos(cos);
        }

        public static JVector3 ClampMagnitude(JVector3 vector, double maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength) return vector.normalized * maxLength;
            else return vector;
        }

        public static JVector3 Cross(JVector3 lhs, JVector3 rhs)
        {
            double x = lhs.y * rhs.z - rhs.y * lhs.z;
            double y = lhs.z * rhs.x - rhs.z * lhs.x;
            double z = lhs.x * rhs.y - rhs.x * lhs.y;

            return new JVector3(x, y, z);
        }

        public static double Distance(JVector3 a, JVector3 b)
        {
            return Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        }

        public static JVector3 Exclude(JVector3 excludeThis, JVector3 fromThat)
        {
            return fromThat - Project(fromThat, excludeThis);
        }

        public static JVector3 Lerp(JVector3 a, JVector3 b, double t)
        {
            if (t <= 0) return a;
            if (t >= 1) return b;
            return a + (b - a) * t;
        }

        public static JVector3 LerpUnclamped(JVector3 a, JVector3 b, double t)
        {
            return a + (b - a) * t;
        }

        public static double Magnitude(JVector3 a)
        {
            return a.magnitude;
        }

        public static JVector3 Max(JVector3 lhs, JVector3 rhs)
        {
            JVector3 temp = new JVector3();
            temp.x = Math.Max(lhs.x, rhs.x);
            temp.y = Math.Max(lhs.y, rhs.y);
            temp.z = Math.Max(lhs.z, rhs.z);
            return temp;
        }

        public static JVector3 Min(JVector3 lhs , JVector3 rhs)
        {
            JVector3 temp = new JVector3();
            temp.x = Math.Min(lhs.x, rhs.x);
            temp.y = Math.Min(lhs.y, rhs.y);
            temp.z = Math.Min(lhs.z, rhs.z);
            return temp;
        }

        public static JVector3 MoveTowards(JVector3 current, JVector3 target, double maxDistanceDelta)
        {
            JVector3 vector3 = target - current;
            double single = vector3.magnitude;
            if (single <= maxDistanceDelta || single == 0f)
            {
                return target;
            }
            return current + ((vector3 / single) * maxDistanceDelta);
        }

        public static JVector3 Normalize(JVector3 value)
        {
            if (value == zero) return zero;
            else
            {
                JVector3 tempDVec = new JVector3();
                tempDVec.x = value.x / value.magnitude;
                tempDVec.y = value.y / value.magnitude;
                tempDVec.z = value.z / value.magnitude;
                return tempDVec;
            }
        }

        public static void OrthoNormalize(ref JVector3 normal, ref JVector3 tangent)
        {
            double mag = Magnitude(normal);
            if (mag > 0)
                normal /= mag;
            else
                normal = new JVector3(1, 0, 0);

            double dot0 = Dot(normal, tangent);
            tangent -= dot0 * normal;
            mag = Magnitude(tangent);
            if (mag < 0)
                tangent = OrthoNormalVectorFast(normal);
            else
                tangent /= mag;
        }

        public static void OrthoNormalize(ref JVector3 normal, ref JVector3 tangent, ref JVector3 binormal)
        {
            double mag = Magnitude(normal);
            if (mag > 0)
                normal /= mag;
            else
                normal = new JVector3(1, 0, 0);

            double dot0 = Dot(normal, tangent);
            tangent -= dot0 * normal;
            mag = Magnitude(tangent);
            if (mag > 0)
                tangent /= mag;
            else
                tangent = OrthoNormalVectorFast(normal);

            double dot1 = Dot(tangent, binormal);
            dot0 = Dot(normal, binormal);
            binormal -= dot0 * normal + dot1 * tangent;
            mag = Magnitude(binormal);
            if (mag > 0)
                binormal /= mag;
            else
                binormal = Cross(normal, tangent);
        }

        public static JVector3 Project(JVector3 vector, JVector3 onNormal)
        {
            if (vector == zero || onNormal == zero)
            {
                return zero;
            }
            return Dot(vector, onNormal) / (onNormal.magnitude * onNormal.magnitude) * onNormal;
        }

        public static JVector3 ProjectOnPlane(JVector3 vector, JVector3 planeNormal)
        {
            return vector - Project(vector, planeNormal);
        }

        public static JVector3 Reflect(JVector3 inDirection, JVector3 inNormal)
        {
            return (-2f * Dot(inNormal, inDirection)) * inNormal + inDirection;
        }

        public static JVector3 RotateTowards(JVector3 current, JVector3 target, double maxRadiansDelta, double maxMagnitudeDelta)
        {
            double currentMag = Magnitude(current);
            double targetMag = Magnitude(target);

            if (currentMag > 0 && targetMag > 0)
            {
                JVector3 currentNorm = current / currentMag;
                JVector3 targetNorm = target / targetMag;

                double dot = Dot(currentNorm, targetNorm);

                if (dot > 1)
                {
                    return MoveTowards(current, target, maxMagnitudeDelta);
                }
                else if (dot < -1)
                {
                    JVector3 axis = OrthoNormalVectorFast(currentNorm);
                    JMatrix4x4 m = SetAxisAngle(axis, maxRadiansDelta);
                    JVector3 rotated = m * currentNorm;
                    rotated *= ClampedMove(currentMag, targetMag, maxMagnitudeDelta);
                    return rotated;
                }
                else
                {
                    double angle = Math.Acos(dot);
                    JVector3 axis = Normalize(Cross(currentNorm, targetNorm));
                    JMatrix4x4 m = SetAxisAngle(axis, Math.Min(maxRadiansDelta, angle));
                    JVector3 rotated = m * currentNorm;
                    rotated *= ClampedMove(currentMag, targetMag, maxMagnitudeDelta);
                    return rotated;
                }
            }
            else
            {
                return MoveTowards(current, target, maxMagnitudeDelta);
            }
        }

        public static JVector3 Scale(JVector3 a, JVector3 b)
        {
            JVector3 temp = new JVector3();
            temp.x = a.x * b.x;
            temp.y = a.y * b.y;
            temp.z = a.z * b.z;
            return temp;
        }

        public static JVector3 Slerp(JVector3 lhs, JVector3 rhs, double t)
        {
            if (t < 0)
            {
                t = 0;
            }
            if (t > 1)
            {
                t = 1;
            }
            return SlerpUnclamped(lhs, rhs, t);
        }

        public static JVector3 SlerpUnclamped(JVector3 lhs, JVector3 rhs, double t)
        {
            double lhsMag = Magnitude(lhs);
            double rhsMag = Magnitude(rhs);

            if (lhsMag < 0 || rhsMag < 0)
                return Lerp(lhs, rhs, t);

            double lerpedMagnitude = rhsMag * t + lhsMag * (1 - t);

            double dot = Dot(lhs, rhs) / (lhsMag * rhsMag);

            if (dot > 1)
            {
                return Lerp(lhs, rhs, t);
            }
            else if (dot < -1)
            {
                JVector3 lhsNorm = lhs / lhsMag;
                JVector3 axis = OrthoNormalVectorFast(lhsNorm);
                JMatrix4x4 m = SetAxisAngle(axis, Math.PI * t);
                JVector3 slerped = m * lhsNorm;
                slerped *= lerpedMagnitude;
                return slerped;
            }
            else
            {
                JVector3 axis = Cross(lhs, rhs);
                JVector3 lhsNorm = lhs / lhsMag;
                axis = Normalize(axis);
                double angle = Math.Acos(dot) * t;

                JMatrix4x4 m = SetAxisAngle(axis, angle);
                JVector3 slerped = m * lhsNorm;
                slerped *= lerpedMagnitude;
                return slerped;
            }
        }

        public static JVector3 SmoothDamp(JVector3 current, JVector3 target, ref JVector3 currentVelocity, double smoothTime, double maxSpeed)
        {
            double deltaTime = Time.deltaTime;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static JVector3 SmoothDamp(JVector3 current, JVector3 target, ref JVector3 currentVelocity, double smoothTime)
        {
            double deltaTime = Time.deltaTime;
            double maxSpeed = double.PositiveInfinity;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static JVector3 SmoothDamp(JVector3 current, JVector3 target, ref JVector3 currentVelocity, double smoothTime, [DefaultValue("Mathf.Infinity")] double maxSpeed, [DefaultValue("Time.deltaTime")] double deltaTime)
        {
            smoothTime = Math.Max(0.0001, smoothTime);
            double num = 2f / smoothTime;
            double num2 = num * deltaTime;
            double d = 1f / (1f + num2 + 0.48 * num2 * num2 + 0.235 * num2 * num2 * num2);
            JVector3 vector = current - target;
            JVector3 vector2 = target;
            double maxLength = maxSpeed * smoothTime;
            vector = ClampMagnitude(vector, maxLength);
            target = current - vector;
            JVector3 vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            JVector3 vector4 = target + (vector + vector3) * d;
            if (Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }
            return vector4;
        }

        public static double SqrMagnitude(JVector3 a)
        {
            return a.sqrMagnitude;
        }

        public void Normalize()
        {
            if (this != zero)
            {
                double length = magnitude;
                x /= length;
                y /= length;
                z /= length;
            }
        }

        public void Scale(JVector3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }

        public void Set(double new_x, double new_y, double new_z)
        {
            x = new_x;
            y = new_y;
            z = new_z;
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", x, y, z);
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }
        public override bool Equals(object other)
        {
            return this == (JVector3)other;
        }
        public string ToString(string format)
        {
            return String.Format("({0}, {1}, {2})", x.ToString(format), y.ToString(format), z.ToString(format));
        }
        public Vector3 ToVector3()
        {
            return new Vector3((float)x, (float)y, (float)z);
        }

        #endregion

        #region 非公開メソッド
        private static JVector3 OrthoNormalVectorFast(JVector3 normal)
        {
            double k1OverSqrt2 = Math.Sqrt(0.5);
            JVector3 res;
            if (Math.Abs(normal.z) > k1OverSqrt2)
            {
                double a = normal.y * normal.y + normal.z * normal.z;
                double k = 1 / Math.Sqrt(a);
                res.x = 0;
                res.y = -normal.z * k;
                res.z = normal.y * k;
            }
            else
            {
                double a = normal.x * normal.x + normal.y * normal.y;
                double k = 1 / Math.Sqrt(a);
                res.x = -normal.y * k;
                res.y = normal.x * k;
                res.z = 0;
            }
            return res;
        }
        private static double ClampedMove(double lhs, double rhs, double clampedDelta)
        {
            double delta = rhs - lhs;
            if (delta > 0.0F)
                return lhs + Math.Min(delta, clampedDelta);
            else
                return lhs - Math.Min(-delta, clampedDelta);
        }
        public static JMatrix4x4 SetAxisAngle(JVector3 rotationAxis, double radians)
        {
            JMatrix4x4 m = new JMatrix4x4();

            double s, c;
            double xx, yy, zz, xy, yz, zx, xs, ys, zs, one_c;

            s = Math.Sin(radians);
            c = Math.Cos(radians);

            xx = rotationAxis.x * rotationAxis.x;
            yy = rotationAxis.y * rotationAxis.y;
            zz = rotationAxis.z * rotationAxis.z;
            xy = rotationAxis.x * rotationAxis.y;
            yz = rotationAxis.y * rotationAxis.z;
            zx = rotationAxis.z * rotationAxis.x;
            xs = rotationAxis.x * s;
            ys = rotationAxis.y * s;
            zs = rotationAxis.z * s;
            one_c = 1 - c;

            m[0, 0] = (one_c * xx) + c;
            m[0, 1] = (one_c * xy) - zs;
            m[0, 2] = (one_c * zx) + ys;

            m[1, 0] = (one_c * xy) + zs;
            m[1, 1] = (one_c * yy) + c;
            m[1, 2] = (one_c * yz) - xs;

            m[2, 0] = (one_c * zx) - ys;
            m[2, 1] = (one_c * yz) + xs;
            m[2, 2] = (one_c * zz) + c;

            return m;
        }
        #endregion

        #region 計算するやつ

        public static JVector3 operator +(JVector3 a, JVector3 b)
        {
            return new JVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static JVector3 operator -(JVector3 a)
        {
            return new JVector3(-a.x, -a.y, -a.z);
        }
        public static JVector3 operator -(JVector3 a, JVector3 b)
        {
            return new JVector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static JVector3 operator *(double d, JVector3 a)
        {
            return new JVector3(a.x * d, a.y * d, a.z * d);
        }
        public static JVector3 operator *(JVector3 a, double d)
        {
            return new JVector3(a.x * d, a.y * d, a.z * d);
        }
        public static JVector3 operator /(JVector3 a, double d)
        {
            return new JVector3(a.x / d, a.y / d, a.z / d);
        }
        public static bool operator ==(JVector3 lhs, JVector3 rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(JVector3 lhs, JVector3 rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator JVector3(Vector3 v)
        {
            return new JVector3(v.x, v.y, v.z);
        }
        public static implicit operator Vector3(JVector3 v)
        {
            return new Vector3((float)v.x, (float)v.y, (float)v.z);
        }

        #endregion
    }
}