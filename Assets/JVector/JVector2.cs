using System;
using UnityEngine;

namespace JuhaKurisu.JVector
{
    public struct JVector2
    {
        #region public members

        public double x;
        public double y;

        #endregion

        #region constructor

        public JVector2(double p_x, double p_y)
        {
            x = p_x;
            y = p_y;
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
                    default:
                        throw new IndexOutOfRangeException("Invalid JVector2 index!");
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
                    default:
                        throw new IndexOutOfRangeException("Invalid JVector2 index!");
                }
            }
        }

        public static JVector2 down
        {
            get
            {
                return new JVector2(0, -1);
            }
        }
        public static JVector2 left
        {
            get
            {
                return new JVector2(-1, 0);
            }
        }
        public static JVector2 one
        {
            get
            {
                return new JVector2(1, 1);
            }
        }
        public static JVector2 right
        {
            get
            {
                return new JVector2(1, 0);
            }
        }
        public static JVector2 up
        {
            get
            {
                return new JVector2(0, 1);
            }
        }
        public static JVector2 zero
        {
            get
            {
                return new JVector2(0, 0);

            }
        }
        public double magnitude
        {
            get
            {
                return Math.Sqrt(sqrMagnitude);
            }
        }
        public JVector2 normalized
        {
            get
            {
                JVector2 result = new JVector2(x, y);
                result.Normalize();
                return result;
            }
        }
        public double sqrMagnitude
        {
            get
            {
                return x * x + y * y;
            }
        }

        #endregion

        #region public functions

        public static float Angle(JVector2 from, JVector2 to)
        {
            double cos = Dot(from.normalized, to.normalized);
            if (cos < -1)
            {
                cos = -1;
            }
            if (cos > 1)
            {
                cos = 1;
            }
            return (float)(Math.Acos(cos) * (180 / Math.PI));
        }
        public static JVector2 ClampMagnitude(JVector2 vector, double maxLength)
        {
            if (maxLength * maxLength >= vector.sqrMagnitude)
            {
                return vector;
            }
            return vector.normalized * maxLength;
        }
        public static double Distance(JVector2 a, JVector2 b)
        {
            return Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        }
        public static double Dot(JVector2 lhs, JVector2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }
        public static JVector2 Lerp(JVector2 a, JVector2 b, double t)
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
        public static JVector2 LerpUnclamped(JVector2 a, JVector2 b, double t)
        {
            return a + (b - a) * t;
        }
        public static JVector2 Max(JVector2 lhs, JVector2 rhs)
        {
            JVector2 temp = new JVector2();
            temp.x = Math.Max(lhs.x, rhs.x);
            temp.y = Math.Max(lhs.y, rhs.y);
            return temp;
        }
        public static JVector2 Min(JVector2 lhs, JVector2 rhs)
        {
            JVector2 temp = new JVector2();
            temp.x = Math.Min(lhs.x, rhs.x);
            temp.y = Math.Min(lhs.y, rhs.y);
            return temp;
        }
        public static JVector2 MoveTowards(JVector2 current, JVector2 target, double maxDistanceDelta)
        {
            JVector2 vector2 = target - current;
            double single = vector2.magnitude;
            if (single <= maxDistanceDelta || single == 0f)
            {
                return target;
            }
            return current + ((vector2 / single) * maxDistanceDelta);
        }
        public static JVector2 Reflect(JVector2 inDirection, JVector2 inNormal)
        {
            return (-2f * Dot(inNormal, inDirection)) * inNormal + inDirection;
        }
        public static JVector2 Scale(JVector2 a, JVector2 b)
        {
            JVector2 temp = new JVector2();
            temp.x = a.x * b.x;
            temp.y = a.y * b.y;
            return temp;
        }
        public static JVector2 SmoothDamp(JVector2 current, JVector2 target, ref JVector2 currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
        {
            smoothTime = Math.Max(0.0001, smoothTime);
            double num = 2 / smoothTime;
            double num2 = num * deltaTime;
            double d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            JVector2 vector = current - target;
            JVector2 vector2 = target;
            double maxLength = maxSpeed * smoothTime;
            vector = ClampMagnitude(vector, maxLength);
            target = current - vector;
            JVector2 vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            JVector2 vector4 = target + (vector + vector3) * d;
            if (Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }
            return vector4;
        }
        public static double SqrMagnitude(JVector2 a)
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
            }
        }
        public void Scale(JVector2 scale)
        {
            x *= scale.x;
            y *= scale.y;
        }
        public void Set(double newX, double newY)
        {
            x = newX;
            y = newY;
        }
        public double SqrMagnitude()
        {
            return sqrMagnitude;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1})", x, y);
        }
        public override bool Equals(object other)
        {
            return this == (JVector2)other;
        }
        public string ToString(string format)
        {
            return String.Format("({0}, {1})", x.ToString(format), y.ToString(format));
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }
        public JVector2 ToVector2()
        {
            return new JVector2((float)x, (float)y);
        }

        #endregion

        #region operator

        public static JVector2 operator +(JVector2 a, JVector2 b)
        {
            return new JVector2(a.x + b.x, a.y + b.y);
        }
        public static JVector2 operator -(JVector2 a)
        {
            return new JVector2(-a.x, -a.y);
        }
        public static JVector2 operator -(JVector2 a, JVector2 b)
        {
            return new JVector2(a.x - b.x, a.y - b.y);
        }
        public static JVector2 operator *(double d, JVector2 a)
        {
            return new JVector2(a.x * d, a.y * d);
        }
        public static JVector2 operator *(JVector2 a, double d)
        {
            return new JVector2(a.x * d, a.y * d);
        }
        public static JVector2 operator /(JVector2 a, double d)
        {
            return new JVector2(a.x / d, a.y / d);
        }
        public static bool operator ==(JVector2 lhs, JVector2 rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(JVector2 lhs, JVector2 rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator JVector2(JVector3 v)
        {
            return new JVector2(v.x, v.y);
        }
        public static implicit operator JVector3(JVector2 v)
        {
            return new JVector3(v.x, v.y, 0);
        }
        public static implicit operator JVector2(Vector2 v)
        {
            return new JVector2(v.x, v.y);
        }
        public static implicit operator Vector2(JVector2 v)
        {
            return new Vector2((float)v.x, (float)v.y);
        }

        #endregion
    }
}