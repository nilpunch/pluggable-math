using System.Runtime.CompilerServices;

using SystemMath = System.Math;

namespace PluggableMath
{
    public readonly struct Float : INumber<Float>
    {
        public readonly float Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float(float value)
        {
            Value = value;
        }

        public IEpsilon<Float> Epsilon => new GenericEpsilon<Float>(new Float(1e-6f));
        public IParser<Float> Parser => new FloatParser();
        public ISpecialMath<Float> SpecialMath => new FloatSpecialMath();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Add(Float other)
        {
            return new Float(Value + other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Sub(Float other)
        {
            return new Float(Value - other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Div(Float other)
        {
            return new Float(Value / other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Mul(Float other)
        {
            return new Float(Value * other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float Negate()
        {
            return new Float(-Value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Less(Float other)
        {
            return Value < other.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LessOrEqual(Float other)
        {
            return Value <= other.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Greater(Float other)
        {
            return Value > other.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GreaterOrEqual(Float other)
        {
            return Value >= other.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EqualTo(Float other)
        {
            return Value == other.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNaN()
        {
            return float.IsNaN(Value);
        }
    }
}