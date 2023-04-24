using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PluggableMath
{
    /// <summary>
    /// Readable expression wrapper for TNumber.
    /// </summary>
    public readonly struct Operand<TNumber> where TNumber : struct, INumber<TNumber>
    {
        public readonly TNumber Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Operand(TNumber value)
        {
            Value = value;
        }

        public static Operand<TNumber> Zero => Parse<TNumber>.FromInt(0);
        public static Operand<TNumber> One => Parse<TNumber>.FromInt(1);
        public static Operand<TNumber> MinusOne => Parse<TNumber>.FromInt(-1);
        public static Operand<TNumber> MinValue => Math<TNumber>.NumericalMinValue;
        public static Operand<TNumber> MaxValue => Math<TNumber>.NumericalMaxValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Operand<TNumber>(TNumber number) => new Operand<TNumber>(number);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator TNumber(Operand<TNumber> operand) => operand.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Operand<TNumber>(float value) => Parse<TNumber>.UnsafeFromFloat(value);
        public static explicit operator float(Operand<TNumber> operand) => Parse<TNumber>.ToFloat(operand);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator +(Operand<TNumber> a) => a.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator -(Operand<TNumber> a) => a.Value.Negate();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator +(Operand<TNumber> a, Operand<TNumber> b) => a.Value.Add(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator -(Operand<TNumber> a, Operand<TNumber> b) => a.Value.Sub(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator *(Operand<TNumber> a, Operand<TNumber> b) => a.Value.Mul(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator /(Operand<TNumber> a, Operand<TNumber> b) => a.Value.Div(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Operand<TNumber> a, Operand<TNumber> b) => a.Value.EqualTo(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Operand<TNumber> a, Operand<TNumber> b) => !(a == b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(Operand<TNumber> a, Operand<TNumber> b) => a.Value.Less(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(Operand<TNumber> a, Operand<TNumber> b) => a.Value.Greater(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(Operand<TNumber> a, Operand<TNumber> b) => a.Value.LessOrEqual(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(Operand<TNumber> a, Operand<TNumber> b) => a.Value.GreaterOrEqual(b.Value);

        public bool Equals(Operand<TNumber> other) => Value.EqualTo(other.Value);

        public override bool Equals(object obj) => obj is Operand<TNumber> other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
