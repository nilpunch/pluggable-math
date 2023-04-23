namespace PluggableMath
{
    public interface IEpsilon<TNumber> where TNumber : struct, INumber<TNumber>
    {
        TNumber Normal { get; }
        TNumber Sqr { get; }
    }
}