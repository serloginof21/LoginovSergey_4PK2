using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace PZ_4
{
    internal class Program
    {
        //Библиотеки
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, 
            IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, bool bInheritHandle, uint dwOptions);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "1: Вывести все\n" +
                    "2: По имени\n" +
                    "3: По полному имени\n" +
                    "4: По дескриптору\n" +
                    "5: Информация о процессе\n" +
                    "6: Информация о процессах, потоках, модулях\n" +
                    "0: Выход"
                );

                char pressedKey = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.Clear();
                switch (pressedKey)
                {
                    case '1':
                        ShowAllInfo();
                    break;
                    case '2':
                        Console.Write("Введите имя процесса: ");
                        string processName = Console.ReadLine();
                        ShowInfoByName(processName);
                    break;
                    case '3':
                        Console.Write("Введите полное имя процесса: ");
                        string fullProcessName = Console.ReadLine();
                        ShowInfoByFullName(fullProcessName);
                    break;
                    case '4':
                        Console.Write("Введите дескриптор процесса: ");
                        string processDescriptor = Console.ReadLine();
                        if (int.TryParse(processDescriptor, out int descriptorValue))
                        {
                            ShowInfoByDescriptor(descriptorValue);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод дескриптора.");
                        }
                    break;
                    case '5':
                        Console.Write("Введите имя процесса для получения дополнительной информации: ");
                        string processInfoName = Console.ReadLine();
                        ShowProcessInfo(processInfoName);
                    break;
                    case '6':
                        Console.Write("Введите название процесса: ");
                        string processNameForThreads = Console.ReadLine();
                        ShowProcessesThreadsModulesInfo(processNameForThreads);
                    break;
                    case '0':
                        Environment.Exit(0);
                    break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите существующий вариант.");
                    break;
                }
            }
        }


        //Вывод полной информации текущего процесса
        static void ShowAllInfo()
        {
            int currentProcessId = Process.GetCurrentProcess().Id; //Функция для получения информации о процессе
            IntPtr currentProcessPseudoHandle = GetCurrentProcess();

            Console.WriteLine($"Идентификатор текущего процесса: {currentProcessId}");
            Console.WriteLine($"Псевдодескриптор текущего процесса: {currentProcessPseudoHandle}");

            IntPtr currentProcessHandle;

            //Сохранение дескриптора, out currentProcessHandle - это переменная, в которую будет сохранен новый дескриптор, если операция дублирования пройдет успешно
            bool success = DuplicateHandle(currentProcessPseudoHandle, currentProcessPseudoHandle, currentProcessPseudoHandle, out currentProcessHandle , 0, false, 2);

            if (success)
            {
                Console.WriteLine($"Дескриптор текущего процесса: {currentProcessHandle}");
                CloseHandle(currentProcessHandle);
            }
            else
            {
                Console.WriteLine("Не удалось получить дескриптор текущего процесса.");
            }
        }

        //Вывод информации о процессе, введеный пользователем
        static void ShowInfoByName(string processName)
        {
            //Получение всех текущих запущенных процессов на компьютере в виде массива
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                Console.WriteLine($"Имя процесса: {process.ProcessName}");
                Console.WriteLine($"Идентификатор процесса: {process.Id}");
            }
        }


        //Вывод по полному имени
        static void ShowInfoByFullName(string fullProcessName)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                    if (string.Equals(process.MainModule.FileName, fullProcessName, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Имя процесса: {process.ProcessName}");
                        Console.WriteLine($"Идентификатор процесса: {process.Id}");
                    }
                }
                catch (Exception) //При ограничении доступа, программа продолжает выполнение
                {
                    
                }
            }
        }

        //Вывод информации при вводе номера дескриптора
        static void ShowInfoByDescriptor(int descriptorValue)
        {
            IntPtr processHandle = new IntPtr(descriptorValue);

            Process process = Process.GetProcessById((int)processHandle);

            if (process != null)
            {
                Console.WriteLine($"Имя процесса: {process.ProcessName}");
                Console.WriteLine($"Идентификатор процесса: {process.Id}");
            }
            else
            {
                Console.WriteLine($"Процесс с дескриптором {descriptorValue} не найден.");
            }
        }

        //Получение дополнительной информации
        static void ShowProcessInfo(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                Console.WriteLine($"Имя процесса: {process.ProcessName}");
                Console.WriteLine($"Идентификатор процесса: {process.Id}");
                Console.WriteLine($"Базовый приоритет: {process.BasePriority}");
                Console.WriteLine($"Приоритет процессора: {process.PriorityClass}");
                Console.WriteLine($"Общее время процессора: {process.TotalProcessorTime}");
            }
        }

        static void ShowProcessesThreadsModulesInfo(string processNameForThreads)
        {
            Process[] processes = Process.GetProcessesByName(processNameForThreads);
            foreach (Process process in processes)
            {
                Console.WriteLine($"Имя процесса: {process.ProcessName}");
                Console.WriteLine($"Идентификатор процесса: {process.Id}");

                Console.WriteLine("Потоки:");
                foreach (ProcessThread thread in process.Threads)
                {
                    Console.WriteLine($"Идентификатор потока: {thread.Id}");
                }
                Console.ReadLine();
            }
        }
    }
}