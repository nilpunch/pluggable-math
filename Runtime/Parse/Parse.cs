namespace PluggableMath
{
    public static class Parse<TNumber> where TNumber : INumber<TNumber>
    {
        private static INumberParser<TNumber> _numberParser;
        
        public static TNumber From(int value)
        {
            return _numberParser.FromInt(value);
        }
        
        public static TNumber FromDivision(int nominator, int denominator)
        {
            return _numberParser.FromDivision(nominator, denominator);
        }
        
        public static void SetParser(INumberParser<TNumber> numberParser)
        {
            _numberParser = numberParser;
        }
    }
}