using System;
using System.IO.Compression;

namespace RationalNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            RationalNumber number1 = new RationalNumber(1, 2);
            RationalNumber number2 = new RationalNumber(4, 3);
            
            Console.WriteLine($"Number1 : {number1.ToString(RationalNumber.DisplayMode.Fraction)} \n" +
                              $"Number2 : {number2.ToString(RationalNumber.DisplayMode.Fraction)} \n");
            
            
            // Test 1 : Math operations
            Console.WriteLine(
                $"Number1 + Number2 = {(number1 + number2).ToString(RationalNumber.DisplayMode.Fraction)}");
            Console.WriteLine(
                $"Number1 - Number2 = {(number1 - number2).ToString(RationalNumber.DisplayMode.Fraction)}");
            Console.WriteLine(
                $"Number1 * Number2 = {(number1 * number2).ToString(RationalNumber.DisplayMode.Fraction)}");
            Console.WriteLine(
                $"Number1 / Number2 = {(number1 / number2).ToString(RationalNumber.DisplayMode.Fraction)} \n");
            
            
            // Test 2 : Сomparison operations
            Console.WriteLine(
                $"Number1 >  Number2 = {number1 > number2}");
            Console.WriteLine(
                $"Number1 >= Number2 = {number1 >= number2}");
            Console.WriteLine(
                $"Number1 <  Number2 = {number1 < number2}");
            Console.WriteLine(
                $"Number1 <= Number2 = {number1 <= number2}");
            Console.WriteLine(
                $"Number1 == Number2 = {number1 == number2}");
            Console.WriteLine(
                $"Number1 != Number2 = {number1 != number2} \n");
            
            
            // Test 3 : Getting a rational number from strings of different formats
            // + show rational numbers in decimal fraction mode
            string pattern1 = "10/7";
            string pattern2 = "-4/5";
            string pattern3 = "1.2785";
            string pattern4 = "-2.";
            
            RationalNumber number3 = RationalNumber.GetNumberFromString(pattern1);
            RationalNumber number4 = RationalNumber.GetNumberFromString(pattern2);
            RationalNumber number5 = RationalNumber.GetNumberFromString(pattern3);
            RationalNumber number6 = RationalNumber.GetNumberFromString(pattern4);
            Console.WriteLine($"Number3 : {number3.ToString(RationalNumber.DisplayMode.DecimalFraction)} \n" +
                              $"Number4 : {number4.ToString(RationalNumber.DisplayMode.DecimalFraction)} \n" +
                              $"Number5 : {number5.ToString(RationalNumber.DisplayMode.DecimalFraction)} \n" +
                              $"Number6 : {number6.ToString(RationalNumber.DisplayMode.DecimalFraction)} \n");
            
            
            // Test 4 : Explicit/implicit operators
            RationalNumber number7 = 5;
            RationalNumber number8 = 4.90;
            int a = (int)number8;
            double b = (double)number7 + 0.1;
            Console.WriteLine($"Number7 : {number7.ToString(RationalNumber.DisplayMode.Fraction)} \n" +
                              $"Number8 : {number8.ToString(RationalNumber.DisplayMode.Fraction)} \n" +
                              $"a = {a}, b = {b} \n");
            
            
            // Test 5 : Demonstration of floating point numbers representation feature
            // and showing of an implemented method taking these feature into account.
            double c = 6.185;
            double d = c * 0.1 / 0.1;
            RationalNumber number9  = c;
            RationalNumber number10 = d;
            Console.WriteLine($"c = {c} \n" +
                              $"d = {d} \n" +
                              $"number9 = {number9.ToString(RationalNumber.DisplayMode.DecimalFraction)} \n" +
                              $"number10 = {number10.ToString(RationalNumber.DisplayMode.DecimalFraction)} \n" +
                              $"Default C# CompareTo method : c == d - {c.CompareTo(d) == 0} \n" +
                              $"Rational Number class CompareTo method : number9 == number10 - {number9.CompareTo(number10) == 0}");
            
        }
    }
}