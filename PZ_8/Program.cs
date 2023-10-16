using System.Runtime.InteropServices;

namespace PZ_8
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr HeapCreate(UInt32 flOptions, UIntPtr dwInitialSize, UIntPtr dwMaximumSize);

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(delegate ()
            {
                while (true)
                {
                    Console.WriteLine("Первый поток активен");
                    Thread.Sleep(1010);
                    HeapCreate(0x00040000, (UIntPtr)900000, (UIntPtr)900860);
                }
            });
            Thread thread2 = new Thread(delegate ()
            {
                while (true)
                {
                    Console.WriteLine("Второй поток активен");
                    Thread.Sleep(1010);
                    HeapCreate(0x00040000, (UIntPtr)900000, (UIntPtr)900860);
                }
            });
            Thread thread3 = new Thread(delegate ()
            {
                while (true)
                {
                    Console.WriteLine("Третий поток активен");
                    Thread.Sleep(1010);
                    HeapCreate(0x00040000, (UIntPtr)900000, (UIntPtr)900860);
                }
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();
        }
    }
}