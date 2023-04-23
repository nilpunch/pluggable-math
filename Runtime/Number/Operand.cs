﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PluggableMath
{
    /// <summary>
    /// Readable expression wrapper for TNumber.
    /// </summary>
    public struct Operand<TNumber> where TNumber : INumber<TNumber>
    {
        public readonly TNumber Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Operand(TNumber value)
        {
            Value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Operand<TNumber>(TNumber number) => new Operand<TNumber>(number);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator +(Operand<TNumber> a) => a.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator -(Operand<TNumber> a) => a.Value.Negate();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator +(Operand<TNumber> a, Operand<TNumber> b) => a.Value.Add(b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Operand<TNumber> operator -(Operand<TNumber> a, Operand<TNumber> b) => -a + b;

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
    }
}