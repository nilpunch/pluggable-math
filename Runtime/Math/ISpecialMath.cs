namespace PluggableMath
{
    /// <summary>
    /// Here lays all the functions that can be highly optimised in different realisation.
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    public interface ISpecialMath<TNumber> where TNumber : struct, INumber<TNumber>
    {
        TNumber Abs(TNumber number);
        TNumber Sign(TNumber number);
        TNumber SignWithZero(TNumber number);
        TNumber CopySign(TNumber from, TNumber to);

        TNumber Sqrt(TNumber number);
        TNumber Pow(TNumber number, TNumber power);
        
        TNumber Sin(TNumber number);
        TNumber Cos(TNumber number);
        TNumber Tan(TNumber number);
        TNumber Atan(TNumber number);
        TNumber Atan2(TNumber y, TNumber x);
    }
}