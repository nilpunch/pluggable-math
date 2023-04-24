namespace PluggableMath
{
    public interface IParser<TNumber> where TNumber : struct, INumber<TNumber>
    {
        TNumber FromInt(int value);

        TNumber FromDivision(int nominator, int denominator);

        float ToFloat(TNumber number);

        public TNumber UnsafeFromFloat(float value);
    }
}
