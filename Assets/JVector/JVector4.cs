using System;
using UnityEngine;

namespace JuhaKurisu.JVector
{
    public struct JVector4
    {
        #region public members

        public double x;
        public double y;
        public double z;
        public double w;

        #endregion

        #region constructor

        public JVector4(double p_x, double p_y)
        {
            x = p_x;
            y = p_y;
            z = 0;
            w = 0;
        }
        public JVector4(double p_x, double p_y, double p_z)
        {
            x = p_x;
            y = p_y;
            z = p_z;
            w = 0;
        }
        public JVector4(double p_x, double p_y, double p_z, double p_w)
        {
            x = p_x;
            y = p_y;
            z = p_z;
            w = p_w;
        }

        #endregion

        #region public properties

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid JVector4 index!");
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
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid JVector4 index!");
                }
            }
        }

        public static JVector4 one
        {
            get
            {
                return new JVector4(1, 1, 1, 1);
            }
        }
        public static JVector4 zero
        {
            get
            {
                return new JVector4(0, 0, 0, 0);
            }
        }
        public double magnitude
        {
            get
            {
                return Math.Sqrt(sqrMagnitude);
            }
        }
        public JVector4 normalized
        {
            get
            {
                return Vector4.Normalize(this);
            }
        }
        public double sqrMagnitude
        {
            get
            {
                return x * x + y * y + z * z + w * w;

            }
        }

        #endregion

        #region public functions

        public static double Distance(JVector4 a, JVector4 b)
        {
            return Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z) + (a.w - b.w) * (a.w - b.w));
        }
        public static double Dot(JVector4 lhs, JVector4 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
        }
        public static JVector4 Lerp(JVector4 a, JVector4 b, float t)
        {
            if (t <= 0)
            {
                return a;
            }
            else if (t >= 1)
            {
                return b;
            }
            return a + (b - a) * t;
        }
        public static JVector4 LerpUnclamped(JVector4 a, JVector4 b, double t)
        {
            return a + (b - a) * t;
        }
        public static double Magnitude(JVector4 a)
        {
            return a.magnitude;
        }
        public static JVector4 Max(JVector4 lhs, JVector4 rhs)
        {
            JVector4 temp = new JVector4();
            temp.x = Math.Max(lhs.x, rhs.x);
            temp.y = Math.Max(lhs.y, rhs.y);
            temp.z = Math.Max(lhs.z, rhs.z);
            temp.w = Math.Max(lhs.w, rhs.w);
            return temp;
        }
        public static JVector4 Min(JVector4 lhs, JVector4 rhs)
        {
            JVector4 temp = new JVector4();
            temp.x = Math.Min(lhs.x, rhs.x);
            temp.y = Math.Min(lhs.y, rhs.y);
            temp.z = Math.Min(lhs.z, rhs.z);
            temp.w = Math.Min(lhs.w, rhs.w);
            return temp;
        }
        public static JVector4 MoveTowards(JVector4 current, JVector4 target, double maxDistanceDelta)
        {
            JVector4 vector4 = target - current;
            double single = vector4.magnitude;
            if (single <= maxDistanceDelta || single == 0f)
            {
                return target;
            }
            return current + ((vector4 / single) * maxDistanceDelta);
        }
        public static JVector4 Normalize(JVector4 value)
        {
            if (value == zero)
            {
                return zero;
            }
            else
            {
                JVector4 tempDVec = new JVector4();
                tempDVec.x = value.x / value.magnitude;
                tempDVec.y = value.y / value.magnitude;
                tempDVec.z = value.z / value.magnitude;
                tempDVec.w = value.w / value.magnitude;
                return tempDVec;
            }
        }
        public static JVector4 Project(JVector4 vector, JVector4 onNormal)
        {
            if (vector == zero || onNormal == zero)
            {
                return zero;
            }
            return Dot(vector, onNormal) / (onNormal.magnitude * onNormal.magnitude) * onNormal;
        }
        public static JVector4 Scale(JVector4 a, JVector4 b)
        {
            JVector4 temp = new JVector4();
            temp.x = a.x * b.x;
            temp.y = a.y * b.y;
            temp.z = a.z * b.z;
            temp.w = a.w * b.w;
            return temp;
        }
        public static double SqrMagnitude(JVector4 a)
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
                w /= length;
            }
        }
        public void Scale(JVector4 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
            w *= scale.w;
        }
        public void Set(double new_x, double new_y, double new_z, double new_w)
        {
            x = new_x;
            y = new_y;
            z = new_z;
            w = new_w;
        }
        public double SqrMagnitude()
        {
            return sqrMagnitude;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3})", x, y, z, w);
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }
        public override bool Equals(object other)
        {
            return this == (JVector4)other;
        }
        public string ToString(string format)
        {
            return String.Format("({0}, {1}, {2}, {3})", x.ToString(format), y.ToString(format), z.ToString(format), w.ToString(format));
        }
        public Vector4 ToVector4()
        {
            return new Vector4((float)x, (float)y, (float)z, (float)w);
        }

        #endregion

        #region operator

        public static JVector4 operator +(JVector4 a, JVector4 b)
        {
            return new JVector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }
        public static JVector4 operator -(JVector4 a)
        {
            return new JVector4(-a.x, -a.y, -a.z, -a.w);
        }
        public static JVector4 operator -(JVector4 a, JVector4 b)
        {
            return new JVector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }
        public static JVector4 operator *(double d, JVector4 a)
        {
            return new JVector4(a.x * d, a.y * d, a.z * d, a.w * d);
        }
        public static JVector4 operator *(JVector4 a, double d)
        {
            return new JVector4(a.x * d, a.y * d, a.z * d, a.w * d);
        }
        public static JVector4 operator /(JVector4 a, double d)
        {
            return new JVector4(a.x / d, a.y / d, a.z / d, a.w / d);
        }
        public static bool operator ==(JVector4 lhs, JVector4 rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(JVector4 lhs, JVector4 rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator JVector4(JVector2 v)
        {
            return new JVector4(v.x, v.y, 0, 0);
        }
        public static implicit operator JVector4(JVector3 v)
        {
            return new JVector4(v.x, v.y, v.z, 0);
        }
        public static implicit operator JVector2(JVector4 v)
        {
            return new JVector2(v.x, v.y);
        }
        public static implicit operator JVector3(JVector4 v)
        {
            return new JVector3(v.x, v.y, v.z);
        }
        public static implicit operator JVector4(Vector4 v)
        {
            return new JVector4(v.x, v.y, v.z, v.w);
        }
        public static implicit operator Vector4(JVector4 v)
        {
            return new Vector4((float)v.x, (float)v.y, (float)v.z, (float)v.w);
        }

        #endregion
    }
}