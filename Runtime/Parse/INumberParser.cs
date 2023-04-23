namespace PluggableMath
{
    public interface INumberParser<TNumber> where TNumber : INumber<TNumber>
    {
        TNumber FromInt(int value);

        TNumber FromDivision(int nominator, int denominator);
    }
}