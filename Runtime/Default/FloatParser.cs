using System.Runtime.CompilerServices;

namespace PluggableMath
{
    public class FloatParser : INumberParser<Float>
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
    }
}