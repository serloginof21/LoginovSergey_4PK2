using System.Runtime.InteropServices;

namespace PZ_10
{
    internal class Program
    {
        const int pageSize = 4096; // Размер страницы

        // Константы для флагов защиты памяти
        const uint pageReadwrite = 0x04;
        const uint memoryCommit = 0x1000;
        const uint memoryReserve = 0x2000;

        //Библиотеки
        [DllImport("kernel32.dll")]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        static extern bool VirtualQuery(IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [StructLayout(LayoutKind.Sequential)]
        struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress; // Базовый адрес участка памяти
            public IntPtr AllocationBase; // Адрес начала выделения
            public uint AllocationProtect; // Флаги защиты выделенной памяти
            public IntPtr RegionSize; // Размер региона
            public uint State; // Состояние
            public uint Protect; // Флаги защиты региона
            public uint Type; // Тип региона
        }

        static void Main(string[] args)
        {
            IntPtr memory = VirtualAlloc(IntPtr.Zero, pageSize, memoryCommit | memoryReserve, pageReadwrite);

            if (memory == IntPtr.Zero)
            {
                Console.WriteLine("Не удалось выделить память.");
                return;
            }

            byte fillValue = 0x7F; // Заполнение выделенной памяти значением 0x7F (7Fh)
            Marshal.WriteByte(memory, fillValue);

            MEMORY_BASIC_INFORMATION memoryInfo = new MEMORY_BASIC_INFORMATION(); // Сбор информации о памяти с помощью VirtualQuery
            VirtualQuery(memory, out memoryInfo, (uint)Marshal.SizeOf(memoryInfo));

            Console.WriteLine($"Размер региона: {memoryInfo.RegionSize.ToInt64()} байт");
            Console.WriteLine($"Флаги защиты: 0x{memoryInfo.AllocationProtect:X}");
            Console.WriteLine($"Базовый адрес: 0x{memoryInfo.BaseAddress.ToInt64():X}");
            Console.WriteLine($"Состояние: 0x{memoryInfo.State:X}");

            Marshal.FreeHGlobal(memory); // Освобождение выделенной памяти
        }
    }
}