using System;
using System.Runtime.CompilerServices;
using PluggableMath;

namespace GameLibrary.Mathematics
{
    /// <summary>
    /// General quaternion. Represent rotation and scaling.
    /// </summary>
    public readonly struct Quaternion<TNumber> : IEquatable<Quaternion<TNumber>>, IFormattable where TNumber : struct, INumber<TNumber>
    {
        public readonly Operand<TNumber> X;
        public readonly Operand<TNumber> Y;
        public readonly Operand<TNumber> Z;
        public readonly Operand<TNumber> W;

        /// <summary>
        /// Constructs a quaternion from four Operand<TNumber> values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion(Operand<TNumber> x, Operand<TNumber> y, Operand<TNumber> z, Operand<TNumber> w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Constructs a quaternion from unit quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion(UnitQuaternion<TNumber> quaternion)
        {
            X = quaternion.X;
            Y = quaternion.Y;
            Z = quaternion.Z;
            W = quaternion.W;
        }

        /// <summary>
        /// The identity rotation.
        /// </summary>
        public static Quaternion<TNumber> Identity => new Quaternion<TNumber>(Operand<TNumber>.Zero, Operand<TNumber>.Zero, Operand<TNumber>.Zero, Operand<TNumber>.One);

        /// <summary>
        /// Returns true if the given quaternion is exactly equal to this quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) =>
            other is Quaternion<TNumber> softQuaternion && Equals(softQuaternion);

        /// <summary>
        /// Returns true if the given quaternion is exactly equal to this quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Quaternion<TNumber> other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override string ToString() =>
            ToString("F2", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        public string ToString(string format) =>
            ToString(format, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        public string ToString(IFormatProvider provider) => ToString("F2", provider);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("({0}, {1}, {2}, {3})", X.ToString(),
                Y.ToString(), Z.ToString(),
                Y.ToString());
        }

        public override int GetHashCode() =>
            X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2 ^ W.GetHashCode() >> 1;

        /// <summary>
        /// Returns the componentwise addition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator +(Quaternion<TNumber> a, Quaternion<TNumber> b)
        {
            return new Quaternion<TNumber>(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        /// <summary>
        /// Returns the componentwise negotiation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator -(Quaternion<TNumber> a)
        {
            return new Quaternion<TNumber>(-a.X, -a.Y, -a.Z, -a.W);
        }

        /// <summary>
        /// Returns the componentwise subtraction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator -(Quaternion<TNumber> a, Quaternion<TNumber> b)
        {
            return -b + a;
        }

        /// <summary>
        /// Returns the quaternions multiplication.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator *(Quaternion<TNumber> a, Quaternion<TNumber> b)
        {
            return new Quaternion<TNumber>(
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y + a.Y * b.W + a.Z * b.X - a.X * b.Z,
                a.W * b.Z + a.Z * b.W + a.X * b.Y - a.Y * b.X,
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z);
        }

        /// <summary>
        /// Returns the componentwise multiplication.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator *(Quaternion<TNumber> a, Operand<TNumber> b)
        {
            return new Quaternion<TNumber>(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        /// <summary>
        /// Returns the vector transformed by the quaternion, including scale and rotation.
        /// Also known as sandwich product: q * vec * conj(q)
        /// </summary>
        public static Vector3<TNumber> operator *(Quaternion<TNumber> quaternion, Vector3<TNumber> vector)
        {
            Operand<TNumber> twoX = quaternion.X * (Operand<TNumber>)2f;
            Operand<TNumber> twoY = quaternion.Y * (Operand<TNumber>)2f;
            Operand<TNumber> twoZ = quaternion.Z * (Operand<TNumber>)2f;
            Operand<TNumber> xx = quaternion.X * quaternion.X;
            Operand<TNumber> yy = quaternion.Y * quaternion.Y;
            Operand<TNumber> zz = quaternion.Z * quaternion.Z;
            Operand<TNumber> ww = quaternion.W * quaternion.W;
            Operand<TNumber> xy2 = quaternion.X * twoY;
            Operand<TNumber> xz2 = quaternion.X * twoZ;
            Operand<TNumber> yz2 = quaternion.Y * twoZ;
            Operand<TNumber> wx2 = quaternion.W * twoX;
            Operand<TNumber> wy2 = quaternion.W * twoY;
            Operand<TNumber> wz2 = quaternion.W * twoZ;
            Vector3<TNumber> result = new Vector3<TNumber>(
                (ww + xx - yy - zz) * vector.X + (xy2 - wz2) * vector.Y + (xz2 + wy2) * vector.Z,
                (xy2 + wz2) * vector.X + (ww - xx + yy - zz) * vector.Y + (yz2 - wx2) * vector.Z,
                (xz2 - wy2) * vector.X + (yz2 + wx2) * vector.Y + (ww - xx - yy + zz) * vector.Z);
            return result;
        }

        /// <summary>
        /// Returns the quaternions division.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator /(Quaternion<TNumber> a, Quaternion<TNumber> b)
        {
            return a * Inverse(b);
        }

        /// <summary>
        /// Returns the componentwise division.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> operator /(Quaternion<TNumber> a, Operand<TNumber> b)
        {
            return new Quaternion<TNumber>(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        /// <summary>
        /// Returns true if quaternions are approximately equal, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Quaternion<TNumber> a, Quaternion<TNumber> b) => ApproximatelyEqual(a, b);

        /// <summary>
        /// Returns true if quaternions are not approximately equal, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Quaternion<TNumber> a, Quaternion<TNumber> b) => !(a == b);

        /// <summary>
        /// The dot product between two rotations.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> Dot(Quaternion<TNumber> a, Quaternion<TNumber> b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// Returns the length of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> Length(Quaternion<TNumber> a)
        {
            return Math<TNumber>.Sqrt(LengthSqr(a));
        }

        /// <summary>
        /// Returns the squared length of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> LengthSqr(Quaternion<TNumber> a)
        {
            return Dot(a, a);
        }

        /// <summary>
        /// Returns the conjugate of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> Conjugate(Quaternion<TNumber> a)
        {
            return new Quaternion<TNumber>(-a.X, -a.Y, -a.Z, a.W);
        }

        /// <summary>
        /// Returns the inverse of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> Inverse(Quaternion<TNumber> a)
        {
            Operand<TNumber> lengthSqr = LengthSqr(a);
            Quaternion<TNumber> conjugation = Conjugate(a);
            return conjugation / lengthSqr;
        }

        /// <summary>
        /// Returns a normalized version of a quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> Normalize(Quaternion<TNumber> a)
        {
            Operand<TNumber> length = Length(a);
            return a / length;
        }

        /// <summary>
        /// Returns a safe normalized version of a quaternion.
        /// Returns the given default value when quaternion length close to zero.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion<TNumber> NormalizeSafe(Quaternion<TNumber> a, Quaternion<TNumber> defaultValue = new Quaternion<TNumber>())
        {
            Operand<TNumber> sqrLength = LengthSqr(a);
            if (sqrLength < Math<TNumber>.EpsilonSqr)
                return defaultValue;
            return a / Math<TNumber>.Sqrt(sqrLength);
        }

        /// <summary>
        /// Returns a spherical interpolation between two quaternions.
        /// Non-commutative, torque-minimal, constant velocity.
        /// Preserve it's properties when quaternions have identical length.
        /// </summary>
        public static Quaternion<TNumber> Slerp(Quaternion<TNumber> a, Quaternion<TNumber> b, Operand<TNumber> t, bool longPath = false)
        {
            // Calculate angle between them.
            Operand<TNumber> cosHalfTheta = Dot(Normalize(a), Normalize(b));

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

            // If a = b or a = b then theta = 0 and we can return interpolation between a and b
            if (Math<TNumber>.Abs(cosHalfTheta) > Operand<TNumber>.One - Math<TNumber>.Epsilon)
            {
                return Lerp(a, b, t, longPath);
            }

            Operand<TNumber> halfTheta = Math<TNumber>.Acos(cosHalfTheta);
            Operand<TNumber> sinHalfTheta = Math<TNumber>.Sin(halfTheta);

            Operand<TNumber> influenceA = Math<TNumber>.Sin((Operand<TNumber>.One - t) * halfTheta) / sinHalfTheta;
            Operand<TNumber> influenceB = Math<TNumber>.Sin(t * halfTheta) / sinHalfTheta;

            return a * influenceA + b * influenceB;
        }

        /// <summary>
        /// Returns a normalized componentwise interpolation between two quaternions.
        /// Commutative, torque-minimal, non-constant velocity.
        /// Preserve it's properties when quaternions have identical length.
        /// </summary>
        public static Quaternion<TNumber> Nlerp(Quaternion<TNumber> a, Quaternion<TNumber> b, Operand<TNumber> t, bool longPath = false)
        {
            Operand<TNumber> dot = Dot(a, b);

            if (longPath)
            {
                if (dot > Operand<TNumber>.Zero)
                {
                    b = -b;
                }
            }
            else
            {
                if (dot < Operand<TNumber>.Zero)
                {
                    b = -b;
                }
            }

            Operand<TNumber> lenght = Length(a) * (Operand<TNumber>.One - t) + Length(b) * t;
            Quaternion<TNumber> normalized = Normalize(a * (Operand<TNumber>.One - t) + b * t);

            return normalized * lenght;
        }

        /// <summary>
        /// Returns a componentwise interpolation between two quaternions.
        /// </summary>
        public static Quaternion<TNumber> Lerp(Quaternion<TNumber> a, Quaternion<TNumber> b, Operand<TNumber> t, bool longPath = false)
        {
            Operand<TNumber> dot = Dot(a, b);

            if (longPath)
            {
                if (dot > Operand<TNumber>.Zero)
                {
                    b = -b;
                }
            }
            else
            {
                if (dot < Operand<TNumber>.Zero)
                {
                    b = -b;
                }
            }

            return a * (Operand<TNumber>.One - t) + b * t;
        }

        /// <summary>
        /// Compares two quaternions with <see cref="Math<TNumber>.CalculationsEpsilonSqr"/> and returns true if they are similar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyEqual(Quaternion<TNumber> a, Quaternion<TNumber> b)
        {
            return ApproximatelyEqual(a, b, Math<TNumber>.EpsilonSqr);
        }

        /// <summary>
        /// Compares two quaternions with some epsilon and returns true if they are similar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyEqual(Quaternion<TNumber> a, Quaternion<TNumber> b, Operand<TNumber> epsilon)
        {
            return Math<TNumber>.Abs(Operand<TNumber>.One - Math<TNumber>.Abs(Dot(a, b))) < epsilon;
        }
    }
}
