using System.Reflection;
using System.Runtime.InteropServices;

namespace PZ_9
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = false)]
        static extern void GetSystemInfo(out SYSTEM_INFO Info);

        [Flags()]
        public enum AllocationType : uint
        {
            COMMIT = 0x1000,
            RESERVE = 0x2000,
            RESET = 0x80000,
            LARGE_PAGES = 0x20000000,
            PHYSICAL = 0x400000,
            TOP_DOWN = 0x100000,
            WRITE_WATCH = 0x200000
        }

        [Flags()]
        public enum MemoryProtection : uint
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            EXECUTE_WRITECOPY = 0x80,
            NOACCESS = 0x01,
            READONLY = 0x02,
            READWRITE = 0x04,
            WRITECOPY = 0x08,
            GUARD_Modifierflag = 0x100,
            NOCACHE_Modifierflag = 0x200,
            WRITECOMBINE_Modifierflag = 0x400
        }
        [StructLayout(LayoutKind.Sequential)]

        public struct SYSTEM_INFO
        {
            public ProcessorArchitecture ProcessorArchitecture;
            public uint PageSize;
            public IntPtr MinimumApplicationAddress;
            public IntPtr MaximumApplicationAddress;
            public IntPtr ActiveProcessorMask;
            public uint NumberOfProcessors;
            public uint ProcessorType;
            public uint AllocationGranularity;
            public ushort ProcessorLevel;
            public ushort ProcessorRevision;
        }

        static void ZeroMemory(IntPtr address, int size)
        {
            byte[] buffer = new byte[size];
            Marshal.Copy(buffer, 0, address, size);
        }

        static void FillMemory(IntPtr address, int size)
        {
            Random rnd = new Random();
            byte value1 = Convert.ToByte(rnd.Next(0, 127));

            byte[] buffer = new byte[size];
            for (int i = 0; i < size; i++)
                buffer[i] = value1;

            Marshal.Copy(buffer, 0, address, size);
        }

        static void Main(string[] args)
        {
            SYSTEM_INFO Info;
            GetSystemInfo(out Info);
            uint page_size = Info.PageSize;
            IntPtr regionOne = VirtualAlloc(IntPtr.Zero, 2 * page_size, AllocationType.RESERVE | AllocationType.COMMIT, MemoryProtection.READWRITE);
            IntPtr regionTwo = VirtualAlloc(IntPtr.Zero, 2 * page_size, AllocationType.RESERVE | AllocationType.COMMIT, MemoryProtection.READWRITE);
            Console.WriteLine($"Адрес первого региона: {regionOne}");
            Console.WriteLine($"Адрес второго региона: {regionTwo}");
            ZeroMemory(regionOne, (int)page_size);
            Console.WriteLine("Выводим содержимое первого региона: " + Marshal.ReadByte(regionOne));
            FillMemory(regionTwo, (int)page_size);
            Console.WriteLine("Выводим содержимое второго региона: " + Marshal.ReadByte(regionTwo));
        }
    }
}