namespace PluggableMath
{
    public class GenericEpsilon<TNumber> : IEpsilon<TNumber> where TNumber : struct, INumber<TNumber>
    {
        public TNumber Normal { get; }
        public TNumber Sqr { get; }

        public GenericEpsilon(TNumber number)
        {
            Normal = number;
            Sqr = number.Mul(number);
        }
    }
}