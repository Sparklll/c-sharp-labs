using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;


namespace TaskManager
{
    [Flags]
    public enum SnapshotFlags : uint
    {
        HeapList = 0x00000001,
        Process = 0x00000002,
        Thread = 0x00000004,
        Module = 0x00000008,
        Module32 = 0x00000010,
        Inherit = 0x80000000,
        All = 0x0000001F,
        NoHeaps = 0x40000000
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct PROCESSENTRY32
    {
        const int MAX_PATH = 260;
        internal UInt32 dwSize;
        internal UInt32 cntUsage;
        internal UInt32 th32ProcessID;
        internal IntPtr th32DefaultHeapID;
        internal UInt32 th32ModuleID;
        internal UInt32 cntThreads;
        internal UInt32 th32ParentProcessID;
        internal Int32 pcPriClassBase;
        internal UInt32 dwFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        internal string szExeFile;
    }
    public class ProcessAnalyzer
    {
        [DllImport("kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr CreateToolhelp32Snapshot([In]UInt32 dwFlags, [In]UInt32 th32ProcessID);

        [DllImport("kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool Process32First([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool Process32Next([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle([In] IntPtr hObject);
        
        
        
        public List<Process> Processes { get; set; }

        public ProcessAnalyzer()
        {
            Processes = new List<Process>();
        }
        public void CollectProcesses()
        {
            IntPtr handleToSnapshot = IntPtr.Zero;
            try
            {
                PROCESSENTRY32 processEntry = new PROCESSENTRY32();
                processEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(PROCESSENTRY32));
                handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.Process, 0);
                if (Process32First(handleToSnapshot, ref processEntry))
                {
                    do
                    {
                        Processes.Add(new Process(processEntry.th32ProcessID,processEntry.szExeFile));
                    } while (Process32Next(handleToSnapshot, ref processEntry));
                }
                else
                {
                    throw new ApplicationException(string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Can't get the process.", ex);
            }
            finally
            {
                CloseHandle(handleToSnapshot);
            }
        }
        
        public void ShowOverallInfo()
        {
            Processes = Processes.OrderBy(process => process.ProcessName.ToLower()).ToList();

            Console.WriteLine(string.Format("{0,-10}", "PROCESS ID\t\t") +
                              string.Format("{0,-30}", "PROCESS NAME\t\t") +
                              string.Format("{0,-20}", "MEMORY USAGE (MB)\t\t") +
                              string.Format("{0,-20}", "CPU USAGE (%)"));
            
            foreach (var process in Processes)
            {
                Console.WriteLine(string.Format("{0,-10}", process.ProcessId) + "\t\t" +
                                  string.Format("{0,-30}", process.ProcessName) + "\t\t" +
                                  string.Format("{0,-20}", process.GetProcessMemoryUsage()) + "\t\t" +
                                  string.Format("{0,-20}", process.GetProcessCpuUsage()));
            }
        }
    }
}