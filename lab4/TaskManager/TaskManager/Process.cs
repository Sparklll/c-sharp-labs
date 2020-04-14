using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace TaskManager
{
    [Flags]
    public enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VirtualMemoryOperation = 0x00000008,
        VirtualMemoryRead = 0x00000010,
        VirtualMemoryWrite = 0x00000020,
        DuplicateHandle = 0x00000040,
        CreateProcess = 0x000000080,
        SetQuota = 0x00000100,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        QueryLimitedInformation = 0x00001000,
        Synchronize = 0x00100000
    }
    
    public enum ProcessorArchitecture : int
    {
        PROCESSOR_ARCHITECTURE_INTEL = 0,
        PROCESSOR_ARCHITECTURE_MIPS = 1,
        PROCESSOR_ARCHITECTURE_ALPHA = 2,
        PROCESSOR_ARCHITECTURE_PPC = 3,
        PROCESSOR_ARCHITECTURE_SHX = 4,
        PROCESSOR_ARCHITECTURE_ARM = 5,
        PROCESSOR_ARCHITECTURE_IA64 = 6,
        PROCESSOR_ARCHITECTURE_ALPHA64 = 7,
        PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF,
    }
        
    [StructLayout(LayoutKind.Sequential, Size=72)]
    public struct PROCESS_MEMORY_COUNTERS
    {
        public uint cb;
        public uint PageFaultCount;
        public UInt64 PeakWorkingSetSize;
        public UInt64 WorkingSetSize;
        public UInt64 QuotaPeakPagedPoolUsage;
        public UInt64 QuotaPagedPoolUsage;
        public UInt64 QuotaPeakNonPagedPoolUsage;
        public UInt64 QuotaNonPagedPoolUsage;
        public UInt64 PagefileUsage;
        public UInt64 PeakPagefileUsage;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_INFO
    {
        public ushort processorArchitecture;
        ushort reserved;
        public uint pageSize;
        public IntPtr minimumApplicationAddress;
        public IntPtr maximumApplicationAddress;
        public IntPtr activeProcessorMask;
        public uint numberOfProcessors;
        public uint processorType;
        public uint allocationGranularity;
        public ushort processorLevel;
        public ushort processorRevision;
    }
    public class Process
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
            ProcessAccessFlags processAccess,
            bool bInheritHandle,
            int processId
        );
        
        [DllImport("psapi.dll", SetLastError=true)]
        private static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS counters, uint size);
        
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle([In] IntPtr hObject);
        
        [DllImport("kernel32.dll")]
        private static extern void GetSystemTimeAsFileTime(out FILETIME
            lpSystemTimeAsFileTime);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetProcessTimes(IntPtr hProcess, out FILETIME
                lpCreationTime, out FILETIME lpExitTime, out FILETIME lpKernelTime,
            out FILETIME lpUserTime);
        
        [DllImport("kernel32", SetLastError = true)]
        private static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);
        
        
        public uint ProcessId { get; }
        public uint NumberOfProcessors { get; } 
        public string ProcessName { get; }
        public TimeSpan ProcessCreationTime { get; set; }
        public TimeSpan PreviousSystemTime { get; set; }
        public TimeSpan PreviousKernelTime { get; set; }
        public TimeSpan PreviousUserTime { get; set; }

        public Process(uint processId, string processName)
        {
            ProcessId = processId;
            ProcessName = processName;
            
            // get number of processors
            SYSTEM_INFO systemInfo;
            GetSystemInfo(out systemInfo);
            NumberOfProcessors =  systemInfo.numberOfProcessors;
            
            FILETIME fileTime;
            GetSystemTimeAsFileTime(out fileTime);
            PreviousSystemTime = GetTimeSpanFromFileTime(fileTime);

            // Get amount of time ran in kernel and user mode
            FILETIME creationTime, exitTime, kernelTime, userTime;
            IntPtr processHandle = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, (int)ProcessId);
            if (processHandle != IntPtr.Zero)
            {
                bool success = GetProcessTimes(processHandle, out creationTime, out exitTime, out kernelTime,
                    out userTime);
                if (success)
                {
                    ProcessCreationTime = GetTimeSpanFromFileTime(creationTime);
                    PreviousKernelTime = GetTimeSpanFromFileTime(kernelTime);
                    PreviousUserTime = GetTimeSpanFromFileTime(userTime);
                }

                CloseHandle(processHandle);
            }
        }
        
        private static TimeSpan GetTimeSpanFromFileTime(FILETIME time)
        {
            return TimeSpan.FromMilliseconds((((ulong)time.dwHighDateTime << 32) + (uint)time.dwLowDateTime) * 0.000001);
        }

        public double GetProcessCpuUsage()
        {
            double cpuUsage = 0;
            IntPtr processHandle  = OpenProcess(ProcessAccessFlags.QueryInformation, false, (int)ProcessId);
            
            if (processHandle != IntPtr.Zero)
            {
                FILETIME fileTime = new FILETIME();
                GetSystemTimeAsFileTime(out fileTime);
                TimeSpan currentSystemTime = GetTimeSpanFromFileTime(fileTime);

                FILETIME kernelTime, userTime;
                bool success = GetProcessTimes(processHandle, out fileTime, out fileTime, out kernelTime, out userTime);
                if (success)
                {
                    TimeSpan currentKernelTime = GetTimeSpanFromFileTime(kernelTime);
                    TimeSpan currentUserTime = GetTimeSpanFromFileTime(userTime);

                    // Calculate process cpu usage
                    double totalProcess = (currentKernelTime.Subtract(PreviousKernelTime).TotalMilliseconds) + (currentUserTime.Subtract(PreviousUserTime).TotalMilliseconds);
                    double totalSystem = currentSystemTime.Subtract(PreviousSystemTime).TotalMilliseconds;
                    if (totalSystem > 0)
                    {
                        cpuUsage = (totalProcess * 100.0) / (totalSystem * NumberOfProcessors);
                    }
                        
        
                    // Store current time info
                    PreviousSystemTime = currentSystemTime;
                    PreviousKernelTime = currentKernelTime;
                    PreviousUserTime = currentUserTime;
                }
        
                CloseHandle(processHandle);
            }

            return System.Math.Round(cpuUsage, 2);
        }
        public double GetProcessMemoryUsage()
        {
            double memoryUsed = 0;
            IntPtr processHandle = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, (int)ProcessId);
            
            if (processHandle != IntPtr.Zero)
            {
                PROCESS_MEMORY_COUNTERS processMemoryCounters = new PROCESS_MEMORY_COUNTERS();
                processMemoryCounters.cb = (uint)Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS));
                
                bool success = GetProcessMemoryInfo(processHandle, out processMemoryCounters, processMemoryCounters.cb);
                if (success)
                {
                    memoryUsed = processMemoryCounters.WorkingSetSize / (1024.0 * 1024.0);
                }
                CloseHandle(processHandle);
            }
            
            return System.Math.Round(memoryUsed, 2);
        }
    }
}