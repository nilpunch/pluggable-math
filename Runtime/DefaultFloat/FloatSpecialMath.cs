using System;
using System.Runtime.CompilerServices;

namespace PluggableMath
{
    public class FloatSpecialMath : ISpecialMath<Float>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Abs(Float number)
        {
            return new Float(MathF.Abs(number.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Sign(Float number)
        {
            return float.IsNegative(number.Value) ? new Float(-1) : new Float(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float SignWithZero(Float number)
        {
            return new Float(MathF.Sign(number.Value));
        }

        public unsafe Float CopySign(Float from, Float to)
        {
            var rawThis = *(uint*)&from.Value;
            var rawOther = *(uint*)&to.Value;

            var copied = (rawThis & 0x7FFFFFFF) | (rawOther & 0x80000000);

            return new Float(*(float*)&copied);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Sqrt(Float number)
        {
            return new Float(MathF.Sqrt(number.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Pow(Float number, Float power)
        {
            return new Float(MathF.Pow(number.Value, power.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Sin(Float number)
        {
            return new Float(MathF.Sin(number.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Cos(Float number)
        {
            return new Float(MathF.Cos(number.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Tan(Float number)
        {
            return new Float(MathF.Tan(number.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Atan(Float number)
        {
            return new Float(MathF.Atan(number.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Atan2(Float y, Float x)
        {
            return new Float(MathF.Atan2(y.Value, x.Value));
        }
    }
}