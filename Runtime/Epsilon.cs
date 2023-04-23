namespace PluggableMath
{
    public static class Epsilon<TNumber> where TNumber : INumber<TNumber>
    {
        public static TNumber Normal { get; private set; }
        public static TNumber Sqr { get; private set; }
        
        public static void SetValue(TNumber value)
        {
            Normal = value;
            Sqr = value.Mul(value);
        }
    }
}