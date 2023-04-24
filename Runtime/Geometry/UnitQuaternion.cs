using System;
using System.Runtime.CompilerServices;
using PluggableMath;

namespace GameLibrary.Mathematics
{
    /// <summary>
    /// Normalized quaternion with unit length. Represent rotation.
    /// </summary>
    public readonly struct UnitQuaternion<TNumber> : IEquatable<UnitQuaternion<TNumber>> where TNumber : struct, INumber<TNumber>
    {
        public readonly Operand<TNumber> X;
        public readonly Operand<TNumber> Y;
        public readonly Operand<TNumber> Z;
        public readonly Operand<TNumber> W;

        // Constructors should be made private to maintain unit length invariant,
        // but they are made public since we can't prevent constructing quaternion with default constructor,
        // that forces length 0.

        /// <summary>
        /// Constructs a unit quaternion from four Operand<TNumber> values. Use if you know what you are doing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UnitQuaternion(Operand<TNumber> x, Operand<TNumber> y, Operand<TNumber> z, Operand<TNumber> w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Constructs a unit quaternion from general quaternion. Use if you know what you are doing.
        /// Does not ensure normalization. For normalizing see <see cref="UnitQuaternion{TNumber}<TNumber>.NormalizeToUnit"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UnitQuaternion(Quaternion<TNumber> quaternion)
        {
            X = quaternion.X;
            Y = quaternion.Y;
            Z = quaternion.Z;
            W = quaternion.W;
        }

        /// <summary>
        /// The identity rotation.
        /// </summary>
        public static UnitQuaternion<TNumber> Identity => new UnitQuaternion<TNumber>(Quaternion<TNumber>.Identity);

        /// <summary>
        /// Returns true if the given quaternion is exactly equal to this quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is UnitQuaternion<TNumber> softQuaternion && Equals(softQuaternion);

        /// <summary>
        /// Returns true if the given quaternion is exactly equal to this quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(UnitQuaternion<TNumber> other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override string ToString() => string.Format("({0}, {1}, {2}, {3})", X.ToString(), Y.ToString(), Z.ToString(), W.ToString());

        public override int GetHashCode() =>
            X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2 ^ W.GetHashCode() >> 1;

        /// <summary>
        /// The componentwise addition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator +(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b)
        {
            return new Quaternion<TNumber>(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        /// <summary>
        /// The componentwise negotiation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> operator -(UnitQuaternion<TNumber> a)
        {
            return new UnitQuaternion<TNumber>(-a.X, -a.Y, -a.Z, -a.W);
        }

        /// <summary>
        /// The componentwise subtraction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator -(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b)
        {
            return -b + a;
        }

        /// <summary>
        /// The quaternions multiplication.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> operator *(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b)
        {
            return EnsureNormalization(new UnitQuaternion<TNumber>(
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y + a.Y * b.W + a.Z * b.X - a.X * b.Z,
                a.W * b.Z + a.Z * b.W + a.X * b.Y - a.Y * b.X,
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z));
        }

        /// <summary>
        /// The componentwise multiplication.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator *(UnitQuaternion<TNumber> a, Operand<TNumber> b)
        {
            return new Quaternion<TNumber>(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        /// <summary>
        /// Rotate vector by the quaternion.
        /// </summary>
        public static Vector3<TNumber> operator *(UnitQuaternion<TNumber> unitQuaternion, Vector3<TNumber> vector)
        {
            Operand<TNumber> twoX = unitQuaternion.X * (Operand<TNumber>)2f;
            Operand<TNumber> twoY = unitQuaternion.Y * (Operand<TNumber>)2f;
            Operand<TNumber> twoZ = unitQuaternion.Z * (Operand<TNumber>)2f;
            Operand<TNumber> xx2 = unitQuaternion.X * twoX;
            Operand<TNumber> yy2 = unitQuaternion.Y * twoY;
            Operand<TNumber> zz2 = unitQuaternion.Z * twoZ;
            Operand<TNumber> xy2 = unitQuaternion.X * twoY;
            Operand<TNumber> xz2 = unitQuaternion.X * twoZ;
            Operand<TNumber> yz2 = unitQuaternion.Y * twoZ;
            Operand<TNumber> wx2 = unitQuaternion.W * twoX;
            Operand<TNumber> wy2 = unitQuaternion.W * twoY;
            Operand<TNumber> wz2 = unitQuaternion.W * twoZ;
            Vector3<TNumber> result = new Vector3<TNumber>(
                (Operand<TNumber>.One - (yy2 + zz2)) * vector.X + (xy2 - wz2) * vector.Y + (xz2 + wy2) * vector.Z,
                (xy2 + wz2) * vector.X + (Operand<TNumber>.One - (xx2 + zz2)) * vector.Y + (yz2 - wx2) * vector.Z,
                (xz2 - wy2) * vector.X + (yz2 + wx2) * vector.Y + (Operand<TNumber>.One - (xx2 + yy2)) * vector.Z);
            return result;
        }

        /// <summary>
        /// The quaternions division.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> operator /(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b)
        {
            return a * Inverse(b);
        }

        /// <summary>
        /// The componentwise division.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator /(UnitQuaternion<TNumber> a, Operand<TNumber> b)
        {
            return new Quaternion<TNumber>(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        /// <summary>
        /// Returns true if quaternions are approximately equal, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b) => ApproximatelyEqual(a, b);

        /// <summary>
        /// Returns true if quaternions are not approximately equal, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b) => !(a == b);

        /// <summary>
        /// The dot product between two rotations.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> Dot(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// The length of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> Length(UnitQuaternion<TNumber> a)
        {
            return Math<TNumber>.Sqrt(LengthSqr(a));
        }

        /// <summary>
        /// The squared length of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> LengthSqr(UnitQuaternion<TNumber> a)
        {
            return Dot(a, a);
        }

        /// <summary>
        /// The conjugate of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> Conjugate(UnitQuaternion<TNumber> a)
        {
            return new UnitQuaternion<TNumber>(-a.X, -a.Y, -a.Z, a.W);
        }

        /// <summary>
        /// The inverse of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> Inverse(UnitQuaternion<TNumber> a)
        {
            return Conjugate(a);
        }

        /// <summary>
        /// Normalize quaternion to unit quaternion. If input quaternion is zero, then returns identity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> NormalizeToUnit(Quaternion<TNumber> a)
        {
            return new UnitQuaternion<TNumber>(Quaternion<TNumber>.NormalizeSafe(a, Quaternion<TNumber>.Identity));
        }

        /// <summary>
        /// Returns a spherical interpolation between two quaternions.
        /// Non-commutative, torque-minimal, constant velocity.
        /// </summary>
        public static UnitQuaternion<TNumber> Slerp(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b, Operand<TNumber> t, bool longPath = false)
        {
            // Calculate angle between them.
            Operand<TNumber> cosHalfTheta = Dot(a, b);

            if (longPath)
            {
                if (cosHalfTheta > Operand<TNumber>.Zero)
                {
                    b = -b;
                    cosHalfTheta = -cosHalfTheta;
                }
            }
            else
            {
                if (cosHalfTheta < Operand<TNumber>.Zero)
                {
                    b = -b;
                    cosHalfTheta = -cosHalfTheta;
                }
            }

            // If a = b or a = b then theta = 0 then we can return interpolation between a and b
            if (Math<TNumber>.Abs(cosHalfTheta) > Operand<TNumber>.One - Math<TNumber>.Epsilon)
            {
                return Nlerp(a, b, t, longPath);
            }

            Operand<TNumber> halfTheta = Math<TNumber>.Acos(cosHalfTheta);
            Operand<TNumber> sinHalfTheta = Math<TNumber>.Sin(halfTheta);

            Operand<TNumber> influenceA = Math<TNumber>.Sin((Operand<TNumber>.One - t) * halfTheta) / sinHalfTheta;
            Operand<TNumber> influenceB = Math<TNumber>.Sin(t * halfTheta) / sinHalfTheta;

            return EnsureNormalization(new UnitQuaternion<TNumber>(a * influenceA + b * influenceB));
        }

        /// <summary>
        /// Returns a normalized componentwise interpolation between two quaternions.
        /// Commutative, torque-minimal, non-constant velocity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> Nlerp(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b, Operand<TNumber> t, bool longPath = false)
        {
            return NormalizeToUnit(Quaternion<TNumber>.Lerp(new Quaternion<TNumber>(a), new Quaternion<TNumber>(b), t, longPath));
        }

        /// <summary>
        /// Returns a rotation with the specified forward and up directions.
        /// If inputs are zero length or collinear or have some other weirdness,
        /// then rotation result will be some mix of <see cref="Vector3<TNumber>.Forward"/> and <see cref="Vector3<TNumber>.Up"/> vectors.
        /// </summary>
        public static UnitQuaternion<TNumber> LookRotation(Vector3<TNumber> forward, Vector3<TNumber> up)
        {
            // Third matrix column
            Vector3<TNumber> lookAt = Vector3<TNumber>.NormalizeSafe(forward, Vector3<TNumber>.Forward);
            // First matrix column
            Vector3<TNumber> sideAxis = Vector3<TNumber>.NormalizeSafe(Vector3<TNumber>.Cross(up, lookAt), Vector3<TNumber>.Orthonormal(lookAt));
            // Second matrix column
            Vector3<TNumber> rotatedUp = Vector3<TNumber>.Cross(lookAt, sideAxis);

            // Sums of matrix main diagonal elements
            Operand<TNumber> trace1 = Operand<TNumber>.One + sideAxis.X - rotatedUp.Y - lookAt.Z;
            Operand<TNumber> trace2 = Operand<TNumber>.One - sideAxis.X + rotatedUp.Y - lookAt.Z;
            Operand<TNumber> trace3 = Operand<TNumber>.One - sideAxis.X - rotatedUp.Y + lookAt.Z;

            // If orthonormal vectors forms identity matrix, then return identity rotation
            if (trace1 + trace2 + trace3 < Math<TNumber>.Epsilon)
            {
                return Identity;
            }

            // Choose largest diagonal
            if (trace1 + Math<TNumber>.Epsilon > trace2 && trace1 + Math<TNumber>.Epsilon > trace3)
            {
                Operand<TNumber> s = Math<TNumber>.Sqrt(trace1) * (Operand<TNumber>)2.0f;
                return new UnitQuaternion<TNumber>(
                    (Operand<TNumber>)0.25f * s,
                    (rotatedUp.X + sideAxis.Y) / s,
                    (lookAt.X + sideAxis.Z) / s,
                    (rotatedUp.Z - lookAt.Y) / s);
            }
            else if (trace2 + Math<TNumber>.Epsilon > trace1 && trace2 + Math<TNumber>.Epsilon > trace3)
            {
                Operand<TNumber> s = Math<TNumber>.Sqrt(trace2) * (Operand<TNumber>)2.0f;
                return new UnitQuaternion<TNumber>(
                    (rotatedUp.X + sideAxis.Y) / s,
                    (Operand<TNumber>)0.25f * s,
                    (lookAt.Y + rotatedUp.Z) / s,
                    (lookAt.X - sideAxis.Z) / s);
            }
            else
            {
                Operand<TNumber> s = Math<TNumber>.Sqrt(trace3) * (Operand<TNumber>)2.0f;
                return new UnitQuaternion<TNumber>(
                    (lookAt.X + sideAxis.Z) / s,
                    (lookAt.Y + rotatedUp.Z) / s,
                    (Operand<TNumber>)0.25f * s,
                    (sideAxis.Y - rotatedUp.X) / s);
            }
        }

        /// <summary>
        /// Returns a quaternion representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// If input vector is zero length then rotation will be around forward axis.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> AxisAngleRadians(Vector3<TNumber> axis, Operand<TNumber> angle)
        {
            axis = Vector3<TNumber>.NormalizeSafe(axis, Vector3<TNumber>.Forward);
            Operand<TNumber> sin = Math<TNumber>.Sin((Operand<TNumber>)0.5f * angle);
            Operand<TNumber> cos = Math<TNumber>.Cos((Operand<TNumber>)0.5f * angle);
            return new UnitQuaternion<TNumber>(axis.X * sin, axis.Y * sin, axis.Z * sin, cos);
        }

        /// <summary>
        /// Returns a quaternion representing a euler angle in radians.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> EulerRadians(Vector3<TNumber> angle)
        {
            Operand<TNumber> cr = Math<TNumber>.Cos(angle.X * (Operand<TNumber>)0.5f);
            Operand<TNumber> sr = Math<TNumber>.Sin(angle.X * (Operand<TNumber>)0.5f);
            Operand<TNumber> cp = Math<TNumber>.Cos(angle.Y * (Operand<TNumber>)0.5f);
            Operand<TNumber> sp = Math<TNumber>.Sin(angle.Y * (Operand<TNumber>)0.5f);
            Operand<TNumber> cy = Math<TNumber>.Cos(angle.Z * (Operand<TNumber>)0.5f);
            Operand<TNumber> sy = Math<TNumber>.Sin(angle.Z * (Operand<TNumber>)0.5f);

            return new UnitQuaternion<TNumber>(
                sr * cp * cy - cr * sp * sy,
                cr * sp * cy + sr * cp * sy,
                cr * cp * sy - sr * sp * cy,
                cr * cp * cy + sr * sp * sy);
        }

        /// <summary>
        /// Returns a quaternion representing a rotation around a unit axis by an angle in degrees.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// /// If input vector is zero length then rotation will be around forward axis.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static SoftUnitQuaternion<TNumber> AxisAngleDegrees(Vector3<TNumber> axis, Operand<TNumber> angle)
        // {
        //     axis = Vector3<TNumber>.NormalizeSafe(axis, Vector3<TNumber>.Forward);
        //     Operand<TNumber> sin = Math<TNumber>.Sin((Operand<TNumber>)0.5f * angle * Math<TNumber>.Deg2Rad);
        //     Operand<TNumber> cos = Math<TNumber>.Cos((Operand<TNumber>)0.5f * angle * Math<TNumber>.Deg2Rad);
        //     return new SoftUnitQuaternion<TNumber>(axis.X * sin, axis.Y * sin, axis.Z * sin, cos);
        // }

        /// <summary>
        /// Compares two quaternions with <see cref="Math<TNumber>.CalculationsEpsilonSqr"/> and returns true if they are similar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyEqual(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b)
        {
            return ApproximatelyEqual(a, b, Math<TNumber>.EpsilonSqr);
        }

        /// <summary>
        /// Compares two quaternions with some epsilon and returns true if they are similar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyEqual(UnitQuaternion<TNumber> a, UnitQuaternion<TNumber> b, Operand<TNumber> epsilon)
        {
            return Math<TNumber>.Abs(Dot(a, b)) > Operand<TNumber>.One - epsilon;
        }

        /// <summary>
        /// Check quaternion for normalization precision error and re-normalize it if needed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnitQuaternion<TNumber> EnsureNormalization(UnitQuaternion<TNumber> a)
        {
            Operand<TNumber> lengthSqr = LengthSqr(a);

            if (Math<TNumber>.Abs(Operand<TNumber>.One - lengthSqr) > Math<TNumber>.EpsilonSqr)
            {
                return new UnitQuaternion<TNumber>(a / Math<TNumber>.Sqrt(lengthSqr));
            }

            return a;
        }
    }
}
