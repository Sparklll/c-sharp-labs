using System;

namespace NumbersDateText
{
    class Program
    {
        static void Main(string[] args)
        {
            Functions functions = new Functions();
            string s;

            // Console.WriteLine("Test #TASK5, enter the string :");
            // s = Console.ReadLine();
            // functions.FindNonEnglishCapitalLetters(s);
            //
            // Console.WriteLine();

            // Console.WriteLine("Test #TASK12, enter the string :");
            // s = Console.ReadLine();
            // functions.PrintNonEnglishWords(s);

            // Console.WriteLine();
            //
            Console.WriteLine("Test #TASK15, enter the string :");
            s = Console.ReadLine();
            Console.WriteLine(functions.ReplaceLettersAfterVowels(s));


            Console.ReadKey();
        }
    }
}