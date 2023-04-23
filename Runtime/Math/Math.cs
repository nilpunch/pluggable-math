namespace PluggableMath
{
    public static class Math<TNumber> where TNumber : INumber<TNumber>
    {
        private static ISpecialMath<TNumber> s_spectialMath;

        public static void SetSpecialMath(ISpecialMath<TNumber> specialMath)
        {
            s_spectialMath = specialMath;
        }

        public static Operand<TNumber> Max(Operand<TNumber> a, Operand<TNumber> b)
        {
            if (a > b || a.Value.IsNaN())
            {
                return a;
            }

            return b;
        }

        public static Operand<TNumber> Min(Operand<TNumber> a, Operand<TNumber> b)
        {
            if (a < b || a.Value.IsNaN())
            {
                return a;
            }

            return b;
        }

        public static Operand<TNumber> Clamp(Operand<TNumber> value, Operand<TNumber> min, Operand<TNumber> max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;

            return value;
        }

        public static Operand<TNumber> Lerp(Operand<TNumber> a, Operand<TNumber> b, Operand<TNumber> t)
        {
            return a + t * (b - a);
        }

        public static bool ApproximatelyEqual(Operand<TNumber> a, Operand<TNumber> b)
        {
            return ApproximatelyEqual(a, b, Epsilon<TNumber>.Sqr);
        }

        public static bool ApproximatelyEqual(Operand<TNumber> x, Operand<TNumber> y, Operand<TNumber> epsilon)
        {
            var difference = Abs(x - y);
            return difference <= epsilon || difference <= Max(Abs(x), Abs(y)) * epsilon;
        }

        // Special Math

        public static Operand<TNumber> Abs(Operand<TNumber> number)
        {
            return s_spectialMath.Abs(number.Value);
        }

        public static Operand<TNumber> Sign(Operand<TNumber> number)
        {
            return s_spectialMath.Sign(number.Value);
        }

        public static Operand<TNumber> SignWithZero(Operand<TNumber> number)
        {
            return s_spectialMath.SignWithZero(number.Value);
        }

        public static Operand<TNumber> CopySign(Operand<TNumber> from, Operand<TNumber> to)
        {
            return s_spectialMath.CopySign(from.Value, to.Value);
        }

        public static Operand<TNumber> Sqrt(Operand<TNumber> number)
        {
            return s_spectialMath.Sqrt(number.Value);
        }

        public static Operand<TNumber> Pow(Operand<TNumber> number, Operand<TNumber> power)
        {
            return s_spectialMath.Pow(number.Value, power.Value);
        }

        public static Operand<TNumber> Sin(Operand<TNumber> number)
        {
            return s_spectialMath.Sin(number.Value);
        }

        public static Operand<TNumber> Cos(Operand<TNumber> number)
        {
            return s_spectialMath.Cos(number.Value);
        }

        public static Operand<TNumber> Tan(Operand<TNumber> number)
        {
            return s_spectialMath.Tan(number.Value);
        }

        public static Operand<TNumber> Atan(Operand<TNumber> number)
        {
            return s_spectialMath.Atan(number.Value);
        }

        public static Operand<TNumber> Atan2(Operand<TNumber> y, Operand<TNumber> x)
        {
            return s_spectialMath.Atan2(y.Value, x.Value);
        }
    }
}