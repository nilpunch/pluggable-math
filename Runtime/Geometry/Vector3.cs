using System;
using System.Runtime.CompilerServices;

namespace PluggableMath
{
    public readonly struct Vector3<TNumber> where TNumber : struct, INumber<TNumber>
    {
        public readonly Operand<TNumber> X;
        public readonly Operand<TNumber> Y;
        public readonly Operand<TNumber> Z;

        /// <summary>
        /// Constructs a vector from three TNumber values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(Operand<TNumber> x, Operand<TNumber> y, Operand<TNumber> z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        /// <summary>
        /// Constructs a vector from three TNumber values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(TNumber x,TNumber y, TNumber z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(0, 0, 0).
        /// </summary>
        public static Vector3<TNumber> Zero => new Vector3<TNumber>(Parse<TNumber>.From(0), Parse<TNumber>.From(0), Parse<TNumber>.From(0));

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(1, 1, 1).
        /// </summary>
        public static Vector3<TNumber> One => new Vector3<TNumber>(Parse<TNumber>.From(1), Parse<TNumber>.From(1), Parse<TNumber>.From(1));

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(1, 0, 0).
        /// </summary>
        public static Vector3<TNumber> Right => new Vector3<TNumber>(Parse<TNumber>.From(1), Parse<TNumber>.From(0), Parse<TNumber>.From(0));

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(-1, 0, 0).
        /// </summary>
        public static Vector3<TNumber> Left => new Vector3<TNumber>(Parse<TNumber>.From(-1), Parse<TNumber>.From(0), Parse<TNumber>.From(0));

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(0, 1, 0).
        /// </summary>
        public static Vector3<TNumber> Up => new Vector3<TNumber>(Parse<TNumber>.From(0), Parse<TNumber>.From(1), Parse<TNumber>.From(0));

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(0, -1, 0).
        /// </summary>
        public static Vector3<TNumber> Down => new Vector3<TNumber>(Parse<TNumber>.From(0), Parse<TNumber>.From(-1), Parse<TNumber>.From(0));

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(0, 0, 1).
        /// </summary>
        public static Vector3<TNumber> Forward => new Vector3<TNumber>(Parse<TNumber>.From(0), Parse<TNumber>.From(0), Parse<TNumber>.From(1));

        /// <summary>
        /// Shorthand for writing Vector3<TNumber>(0, 0, -1).
        /// </summary>
        public static Vector3<TNumber> Backward => new Vector3<TNumber>(Parse<TNumber>.From(0), Parse<TNumber>.From(0), Parse<TNumber>.From(-1));

        /// <summary>
        /// Returns true if the given vector is exactly equal to this vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is Vector3<TNumber> otherVector && EqualTo(otherVector);

        /// <summary>
        /// Returns true if the given vector is exactly equal to this vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualTo(Vector3<TNumber> other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override string ToString() => string.Format("({0}, {1}, {2})", X.ToString(), Y.ToString(), Z.ToString());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2;

        /// <summary>
        /// Returns the componentwise addition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator +(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return new Vector3<TNumber>(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Returns the componentwise addition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator +(Vector3<TNumber> a, Operand<TNumber> b)
        {
            return new Vector3<TNumber>(a.X + b, a.Y + b, a.Z + b);
        }

        /// <summary>
        /// Returns the componentwise addition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator +(Operand<TNumber> a, Vector3<TNumber> b)
        {
            return b + a;
        }

        /// <summary>
        /// Returns the componentwise negotiation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator -(Vector3<TNumber> a)
        {
            return new Vector3<TNumber>(-a.X, -a.Y, -a.Z);
        }

        /// <summary>
        /// Returns the componentwise subtraction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator -(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return -b + a;
        }

        /// <summary>
        /// Returns the componentwise subtraction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator -(Vector3<TNumber> a, Operand<TNumber> b)
        {
            return -b + a;
        }

        /// <summary>
        /// Returns the componentwise subtraction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator -(Operand<TNumber> a, Vector3<TNumber> b)
        {
            return -b + a;
        }

        /// <summary>
        /// Returns the componentwise multiplication.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator *(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return new Vector3<TNumber>(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        /// <summary>
        /// Returns the componentwise multiplication.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator *(Vector3<TNumber> a, TNumber b)
        {
            return new Vector3<TNumber>(a.X * b, a.Y * b, a.Z * b);
        }

        /// <summary>
        /// Returns the componentwise multiplication.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator *(TNumber a, Vector3<TNumber> b)
        {
            return b * a;
        }

        /// <summary>
        /// Returns the componentwise division.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator /(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return new Vector3<TNumber>(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        /// <summary>
        /// Returns the componentwise division.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator /(Vector3<TNumber> a, Operand<TNumber> b)
        {
            return new Vector3<TNumber>(a.X / b, a.Y / b, a.Z / b);
        }

        /// <summary>
        /// Returns the componentwise division.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> operator /(Operand<TNumber> a, Vector3<TNumber> b)
        {
            return b / a;
        }

        /// <summary>
        /// Returns true if vectors are approximately equal, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3<TNumber> a, Vector3<TNumber> b) => ApproximatelyEqual(a, b);

        /// <summary>
        /// Returns true if vectors are not approximately equal, false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3<TNumber> a, Vector3<TNumber> b) => !a.EqualTo(b);

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> Dot(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// Returns the cross product of two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> Cross(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return new Vector3<TNumber>(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        /// <summary>
        /// Returns the length of a vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> Length(Vector3<TNumber> a)
        {
            return Math<TNumber>.Sqrt(LengthSqr(a));
        }

        /// <summary>
        /// Returns the squared length of a vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> LengthSqr(Vector3<TNumber> a)
        {
            return Dot(a, a);
        }

        /// <summary>
        /// Returns the distance between a and b.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> Distance(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return Math<TNumber>.Sqrt(DistanceSqr(a, b));
        }

        /// <summary>
        /// Returns the squared distance between a and b.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> DistanceSqr(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            var deltaX = a.X - b.X;
            var deltaY = a.Y - b.Y;
            var deltaZ = a.Z - b.Z;
            return deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ;
        }

        /// <summary>
        /// Returns a normalized version of a vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> Normalize(Vector3<TNumber> a)
        {
            Operand<TNumber> length = Length(a);
            return a / length;
        }

        /// <summary>
        /// Returns a safe normalized version of a vector.
        /// Returns the given default value when vector length close to zero.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> NormalizeSafe(Vector3<TNumber> a, Vector3<TNumber> defaultValue = new Vector3<TNumber>())
        {
            var lengthSqr = LengthSqr(a);
            if (lengthSqr < Epsilon<TNumber>.Sqr)
                return defaultValue;
            return a / Math<TNumber>.Sqrt(lengthSqr);
        }

        /// <summary>
        /// Returns non-normalized perpendicular vector to a given one. For normalized see <see cref="Orthonormal"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> Orthogonal(Vector3<TNumber> a)
        {
            return new Vector3<TNumber>(
                Math<TNumber>.CopySign(a.Z, a.X),
                Math<TNumber>.CopySign(a.Z, a.Y),
                -Math<TNumber>.CopySign(a.X, a.Z) - Math<TNumber>.CopySign(a.Y, a.Z));
        }

        /// <summary>
        /// Returns orthogonal basis vector to a given one.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> Orthonormal(Vector3<TNumber> a)
        {
            var length = Length(a);
            var s = Math<TNumber>.CopySign(length, a.Z);
            var h = a.Z + s;
            return new Vector3<TNumber>(s * h - a.X * a.X, -a.X * a.Y, -a.X * h);
        }

        /// <summary>
        /// Returns a vector that is made from the largest components of two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> MaxComponents(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return new Vector3<TNumber>(Math<TNumber>.Max(a.X, b.X), Math<TNumber>.Max(a.Y, b.Y), Math<TNumber>.Max(a.Z, b.Z));
        }

        /// <summary>
        /// Returns a vector that is made from the smallest components of two vectors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> MinComponents(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return new Vector3<TNumber>(Math<TNumber>.Min(a.X, b.X), Math<TNumber>.Min(a.Y, b.Y), Math<TNumber>.Min(a.Z, b.Z));
        }

        /// <summary>
        /// Returns the componentwise absolute value of a vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> AbsComponents(Vector3<TNumber> a)
        {
            return new Vector3<TNumber>(Math<TNumber>.Abs(a.X), Math<TNumber>.Abs(a.Y), Math<TNumber>.Abs(a.Z));
        }

        /// <summary>
        /// Returns the componentwise signes of a vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3<TNumber> SignComponents(Vector3<TNumber> a)
        {
            return new Vector3<TNumber>(Math<TNumber>.Sign(a.X), Math<TNumber>.Sign(a.Y), Math<TNumber>.Sign(a.Z));
        }

        /// <summary>
        /// Compares two vectors with <see cref="Math<TNumber>.CalculationsEpsilonSqr"/> and returns true if they are similar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyEqual(Vector3<TNumber> a, Vector3<TNumber> b)
        {
            return ApproximatelyEqual(a, b, Epsilon<TNumber>.Sqr);
        }

        /// <summary>
        /// Compares two vectors with some epsilon and returns true if they are similar.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyEqual(Vector3<TNumber> a, Vector3<TNumber> b, TNumber epsilon)
        {
            return DistanceSqr(a, b) < epsilon;
        }
    }
}