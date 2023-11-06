using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LabWork1
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool GetCPInfo(uint CodePage, out CPINFO lpCPInfo); //Позволяет получить информацию о заданной кодовой странице

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey); // Позволяет получить состояние заданной клавиши на клавиатуре

        [DllImport("kernel32.dll")]
        static extern int GetLastError(); //Возвращает код последней произошедшей ошибки в рамках текущего потока

        [DllImport("user32.dll")]
        static extern bool UnloadKeyboardLayout(IntPtr hkl); //Функция позволяет выгрузить указанную клавиатурную компоненту (keyboard layout) из текущего процесса

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct CPINFO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] MaxCharSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
            public char[] DefaultChar;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] LeadByte;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Имя компьютера и имя пользователя:");
            string computerName = Environment.MachineName;
            string userName = Environment.UserName;
            Console.WriteLine($"Имя компьютера: {computerName}");
            Console.WriteLine($"Имя пользователя: {userName}");

            Console.WriteLine("\nПути к системным каталогам Windows:");
            string systemDirectory = Environment.SystemDirectory;
            string windowsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            Console.WriteLine($"Путь к системному каталогу Windows: {systemDirectory}");
            Console.WriteLine($"Путь к каталогу Windows: {windowsDirectory}");

            Console.WriteLine("\nВерсия операционной системы:");
            string osVersion = Environment.OSVersion.VersionString;
            Console.WriteLine($"Версия операционной системы: {osVersion}");

            Console.WriteLine("\nСистемные метрики:");
            int processorCount = Environment.ProcessorCount;
            long memorySize = Environment.WorkingSet;
            Console.WriteLine($"Количество процессоров: {processorCount}");
            Console.WriteLine($"Объем памяти: {memorySize} байт");

            Console.WriteLine("\nСистемные параметры:");
            string systemDrive = Environment.GetEnvironmentVariable("SystemDrive");
            string tempPath = Environment.GetEnvironmentVariable("TEMP");
            Console.WriteLine($"Системный диск: {systemDrive}");
            Console.WriteLine($"Путь к временной папке: {tempPath}");

            ChangeSystemColors(); // Функция для изменение системных цветов на кастомный

            Console.WriteLine("\nРабота со временем:");
            DateTime currentTime = DateTime.Now;
            Console.WriteLine($"Текущее время: {currentTime}");

            DateTime futureTime = currentTime.AddHours(5);
            Console.WriteLine($"Через 5 часов будет: {futureTime}");

            ChangeToOriginalSystemColors(); // Функция для изменение системных цветов на оригинальный

            CPINFO cpInfo;
            if (GetCPInfo(1251, out cpInfo))
            {
                Console.WriteLine($"\nМаксимальный размер символа в странице: {cpInfo.MaxCharSize[0]}");
            }

            short keyState = GetKeyState(0x20);
            Console.WriteLine($"Пробел не нажат: {(keyState < 0 ? "Pressed" : "Not Pressed")}");

            int lastError = GetLastError();
            Console.WriteLine($"Последние ошибки: {lastError}");

            IntPtr hkl = new IntPtr(0x04090409);
            bool unloadResult = UnloadKeyboardLayout(hkl);
            Console.WriteLine($"Результат UnloadKeyboardLayout: {unloadResult}");

            Console.ReadKey();
        }

        static void ChangeSystemColors()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        static void ChangeToOriginalSystemColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}