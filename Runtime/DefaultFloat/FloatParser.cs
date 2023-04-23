using System.Runtime.CompilerServices;

namespace PluggableMath
{
    public class FloatParser : IParser<Float>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float FromInt(int value)
        {
            return new Float(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float FromDivision(int nominator, int denominator)
        {
            return new Float((float)nominator / denominator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ToFloat(Float number)
        {
            return number.Value;
        }
    }
}