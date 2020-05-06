using System;
using System.Text.RegularExpressions;

namespace RationalNumber
{
    public class RationalNumber : IComparable<RationalNumber>
    {
        public enum DisplayMode {Fraction, DecimalFraction}

        private long denominator;
        public long Numerator { get; set; }

        public long Denominator
        {
            get => denominator;
            set
            {
                if (value < 1)
                {
                    throw new ApplicationException(
                        "Attempt to set an invalid denominator.");
                }
                
                denominator = value;
            }
        }

        public RationalNumber()
        {
            Numerator = 0;
            Denominator = 1;
        }

        public RationalNumber(long numerator, long denominator)
        {
            if (denominator < 1)
            {
                throw new ApplicationException(
                    "Attempt to initialize RationalNumber object with an invalid denominator.");
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        public static RationalNumber operator -(RationalNumber number1, RationalNumber number2)
        {
            RationalNumber resultNumber = new RationalNumber();
            resultNumber.Numerator = number1.Numerator * number2.Denominator - number2.Numerator * number1.Denominator;
            resultNumber.Denominator = number1.Denominator * number2.Denominator;
            resultNumber.ReduceFraction();

            return resultNumber;
        }
        
        public static RationalNumber operator +(RationalNumber number1, RationalNumber number2)
        {
            RationalNumber resultNumber = new RationalNumber();
            resultNumber.Numerator = number1.Numerator * number2.Denominator + number2.Numerator * number1.Denominator;
            resultNumber.Denominator = number1.Denominator * number2.Denominator;
            resultNumber.ReduceFraction();

            return resultNumber;
        }
        
        public static RationalNumber operator *(RationalNumber number1, RationalNumber number2)
        {
            RationalNumber resultNumber = new RationalNumber();
            resultNumber.Numerator = number1.Numerator * number2.Numerator;
            resultNumber.Denominator = number1.Denominator * number2.Denominator;
            resultNumber.ReduceFraction();

            return resultNumber;
        }
        
        public static RationalNumber operator /(RationalNumber number1, RationalNumber number2)
        {
            if (number2.Numerator == 0)
            {
                throw new DivideByZeroException();
            }
            
            RationalNumber resultNumber = new RationalNumber();
            resultNumber.Numerator = number1.Numerator * number2.Denominator;
            resultNumber.Denominator = number1.Denominator * number2.Numerator;
            resultNumber.ReduceFraction();

            return resultNumber;
        }
        
        public static bool operator >(RationalNumber number1, RationalNumber number2) => number1.CompareTo(number2) > 0;

        public static bool operator >=(RationalNumber number1, RationalNumber number2) => number1.CompareTo(number2) >= 0;

        public static bool operator <(RationalNumber number1, RationalNumber number2) => number1.CompareTo(number2) < 0;

        public static bool operator <=(RationalNumber number1, RationalNumber number2) => number1.CompareTo(number2) <= 0;

        public static bool operator ==(RationalNumber number1, RationalNumber number2) => number1.Equals(number2);

        public static bool operator !=(RationalNumber number1, RationalNumber number2) => !number1.Equals(number2);

        public static explicit operator int(RationalNumber number) => (int) number.ToDouble();
        
        public static explicit operator double(RationalNumber number) => number.ToDouble();

        public static implicit operator RationalNumber(int number) => new RationalNumber(number, 1);
        
        public static implicit operator RationalNumber(double number) => DecimalFractionToRationalNumber(number);

        private void ReduceFraction()
        {
            long greatestCommonDivisor = Numerator > Denominator
                ? GreatestCommonDivisor(Numerator, Denominator)
                : GreatestCommonDivisor(Denominator, Numerator);
            
            Numerator /= greatestCommonDivisor;
            Denominator /= greatestCommonDivisor;
        }
        
        public static long GreatestCommonDivisor(long a, long b)
        {
            if (b == 0) {
                return a;
            }
            
            return GreatestCommonDivisor(b, a % b);
        }
        
        public static RationalNumber DecimalFractionToRationalNumber(double number)
        {
            RationalNumber rationalNumber;
            long numerator;
            long denominator = 1;
            string s = number.ToString();
            int digitsAtferDot = s.Length - 1 - s.IndexOf('.');
            
            for (int i = 0; i < digitsAtferDot; i++) {
                number *= 10;
                denominator *= 10;
            }
            numerator = (long) Math.Round(number);
            rationalNumber = new RationalNumber(numerator, denominator);
            rationalNumber.ReduceFraction();
            
            return rationalNumber;
        }
        
        public static RationalNumber GetNumberFromString(string s)
        {
            RationalNumber rationalNumber;
            string fractionPattern = "^[+-]?[1-9][0-9]*/[1-9][0-9]*$|^0/[1-9][0-9]*$";
            string floatingPointPattern = "^[+-]?([0-9]+([.][0-9]*)?$|^[.][0-9]+)$";

            if (Regex.IsMatch(s, fractionPattern))
            {
                int sign = s[0] == '-' ? -1 : 1;
                int numberBeginPosition = Char.IsDigit(s[0]) ? 0 : 1;
                int fractionLinePos = s.IndexOf('/');
                string numerator = s.Substring(numberBeginPosition, fractionLinePos - numberBeginPosition);
                string denominator = s.Substring(fractionLinePos + 1);

                rationalNumber = new RationalNumber(sign * Int64.Parse(numerator), Int64.Parse(denominator));
                
                return rationalNumber;
            }
            else if (Regex.IsMatch(s, floatingPointPattern))
            {
                rationalNumber = DecimalFractionToRationalNumber(Double.Parse(s));
                return rationalNumber;
            }
            else
            {
                throw new ApplicationException("Attempt to convert an invalid string to a rational number.");
            }
        }
        
        public int CompareTo(RationalNumber other)
        {
            // Due to the nature of the representation of floating-point numbers,
            // we will use an approximate comparison with the necessary tolerance.
            double tolerance = 0.000000001;
            double delta = Math.Abs(this.ToDouble()-other.ToDouble());

            if (delta < tolerance)
            {
                return 0;
            }
            else
            {
                return this.ToDouble().CompareTo(other.ToDouble());
            }
        }
        
        public override bool Equals(object obj)
        {
            
            if (obj is RationalNumber rationalNumber)
            {
                // Due to the nature of the representation of floating-point numbers,
                // we will use an approximate comparison with the necessary tolerance.
                double tolerance = 0.000000001;
                double delta = Math.Abs(this.ToDouble()-rationalNumber.ToDouble());

                return delta < tolerance;
            }

            return false;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (Numerator.GetHashCode() * 256) ^ (Denominator.GetHashCode() * 128);
            }
        }

        public string ToString(DisplayMode mode)
        {
            switch (mode)
            {
                case DisplayMode.Fraction:
                {
                    return $"{Numerator}/{Denominator}";
                    break;
                }
                case DisplayMode.DecimalFraction:
                {
                    return ToDouble().ToString();
                    break;
                }
                default:
                {
                    return "";
                }
            }
        }
        
        public double ToDouble()
        {
            return (double)Numerator / Denominator;
        }

    }
}