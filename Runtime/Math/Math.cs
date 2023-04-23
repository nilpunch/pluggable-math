namespace PluggableMath
{
    public static class Math<TNumber> where TNumber : struct, INumber<TNumber>
    {
        private static ISpecialMath<TNumber> SpecialMath { get; }

        static Math()
        {
            SpecialMath = new TNumber().SpecialMath;
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
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

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
            return SpecialMath.Abs(number.Value);
        }

        public static Operand<TNumber> Sign(Operand<TNumber> number)
        {
            return SpecialMath.Sign(number.Value);
        }

        public static Operand<TNumber> SignWithZero(Operand<TNumber> number)
        {
            return SpecialMath.SignWithZero(number.Value);
        }

        public static Operand<TNumber> CopySign(Operand<TNumber> from, Operand<TNumber> to)
        {
            return SpecialMath.CopySign(from.Value, to.Value);
        }

        public static Operand<TNumber> Sqrt(Operand<TNumber> number)
        {
            return SpecialMath.Sqrt(number.Value);
        }

        public static Operand<TNumber> Pow(Operand<TNumber> number, Operand<TNumber> power)
        {
            return SpecialMath.Pow(number.Value, power.Value);
        }

        public static Operand<TNumber> Sin(Operand<TNumber> number)
        {
            return SpecialMath.Sin(number.Value);
        }

        public static Operand<TNumber> Cos(Operand<TNumber> number)
        {
            return SpecialMath.Cos(number.Value);
        }

        public static Operand<TNumber> Tan(Operand<TNumber> number)
        {
            return SpecialMath.Tan(number.Value);
        }

        public static Operand<TNumber> Atan(Operand<TNumber> number)
        {
            return SpecialMath.Atan(number.Value);
        }

        public static Operand<TNumber> Atan2(Operand<TNumber> y, Operand<TNumber> x)
        {
            return SpecialMath.Atan2(y.Value, x.Value);
        }
    }
}