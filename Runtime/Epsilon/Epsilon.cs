namespace PluggableMath
{
    public static class Epsilon<TNumber> where TNumber : struct, INumber<TNumber>
    {
        public static TNumber Normal { get; }
        public static TNumber Sqr { get; }
        
        static Epsilon()
        {
            var realisation = new TNumber().Epsilon;
            Normal = realisation.Normal;
            Sqr = realisation.Sqr;
        }
    }
}