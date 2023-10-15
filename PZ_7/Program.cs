using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PZ_7
{

    [StructLayout(LayoutKind.Sequential)]
    public class MEMORYSTATUS
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUS()
        {
            dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUS));
        }
    }

    internal class Program
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUS lpBuffer);

        static void Main(string[] args)
        {
            MEMORYSTATUS memoryStatus = new MEMORYSTATUS();
            Process currentProcess = Process.GetCurrentProcess();

            if (GlobalMemoryStatusEx(memoryStatus))
            {
                Console.WriteLine("Информация о памяти текущего процесса:");
                Console.WriteLine($"Объем физической памяти, выделенной для текущего процесса: {currentProcess.WorkingSet64 / 1024} КБ");
                Console.WriteLine($"Объем памяти, доступной в данный момент: {memoryStatus.ullAvailPhys} байт");
                Console.WriteLine($"Объем файла подкачки, выделенный для текущего процесса: {currentProcess.PagedMemorySize64 / 1024} КБ");
                Console.WriteLine($"Объем файла подкачки, доступного в данный момент: {memoryStatus.ullAvailPageFile} байт");
                Console.WriteLine($"Всего виртуальной памяти: {memoryStatus.ullTotalVirtual} байт");
                Console.WriteLine($"Объем виртуальной памяти, доступной в данный момент: {memoryStatus.ullAvailVirtual} байт");
                Console.WriteLine($"Объем памяти, используемой процессами: {memoryStatus.ullTotalPhys - memoryStatus.ullAvailPhys} байт");
            }
            else
            {
                Console.WriteLine("Не удалось получить информацию о памяти.");
            }
        }
    }
}