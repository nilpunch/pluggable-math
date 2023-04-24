using System;
using System.Runtime.CompilerServices;

namespace PluggableMath
{
    public static class Parse<TNumber> where TNumber : struct, INumber<TNumber>
    {
        private static IParser<TNumber> Parser { get; }

        static Parse()
        {
            Parser = new TNumber().Parser;
        }

        public static TNumber FromInt(int value)
        {
            return Parser.FromInt(value);
        }

        public static TNumber FromDivision(int nominator, int denominator)
        {
            return Parser.FromDivision(nominator, denominator);
        }

        public static float ToFloat(TNumber number)
        {
            return Parser.ToFloat(number);
        }

        public static TNumber UnsafeFromFloat(float value)
        {
            return Parser.UnsafeFromFloat(value);
        }
    }
}
