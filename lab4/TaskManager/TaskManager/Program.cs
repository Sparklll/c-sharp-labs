using System;
using System.Threading;


namespace TaskManager
{
    class Program
    {
        static void Main()
        {
            ProcessAnalyzer processAnalyzer = new ProcessAnalyzer();
            ConsoleKeyInfo signal;
            
            Console.CursorVisible = false;

            do
            {
                processAnalyzer.CollectProcesses();
                Thread.Sleep(500);
                processAnalyzer.ShowOverallInfo();
                Console.SetCursorPosition(0, 0);
                
                // To update the list of processes, press the spacebar
                do
                {
                    signal = Console.ReadKey(true);
                } while (signal.Key != ConsoleKey.Spacebar);
                
                Console.Clear();
                processAnalyzer.Processes.Clear();
            } while (true);

        }
    }
}
