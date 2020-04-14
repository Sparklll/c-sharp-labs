using System;
using System.Runtime.InteropServices;

namespace DynamicLibrary
{
    class Program
    {
        [DllImport("generator.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string generatePassword(int passwordLength);
        
        [DllImport("generator.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int generateRandomNumberFromRange(long min, long max);
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Generated password (e.g length 10) :  " + Program.generatePassword(10));
            Console.WriteLine("Generated random number (e.g range [-10000..10000]) :  " +
                              Program.generateRandomNumberFromRange(-10000, 10000));
            Console.ReadKey();
        }
    }
}