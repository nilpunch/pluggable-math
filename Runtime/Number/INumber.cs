﻿namespace PluggableMath
{
    public interface INumber<TNumber> where TNumber : INumber<TNumber>
    {
        TNumber Add(TNumber other);
        TNumber Sub(TNumber other);
        TNumber Div(TNumber other);
        TNumber Mul(TNumber other);

        TNumber Negate();

        bool Less(TNumber other);
        bool LessOrEqual(TNumber other);
        bool Greater(TNumber other);
        bool GreaterOrEqual(TNumber other);
        bool EqualTo(TNumber other);

        bool IsNaN();
    }
}