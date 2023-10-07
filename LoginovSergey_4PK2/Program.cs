using System.Runtime.InteropServices;
using System.Text;

namespace PZ_2
{
    internal class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetComputerName(StringBuilder nameBuffer, ref int bufferSize);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetVersion();

        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public UInt16 wServicePackMajor;
            public UInt16 wServicePackMinor;
            public UInt16 wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        static void Main(string[] args)
        {
            var version = GetVersion();
            OSVERSIONINFOEX a = new OSVERSIONINFOEX();
            a.dwOSVersionInfoSize = Marshal.SizeOf(GetVersion());
            Console.WriteLine("Версия ОС: " + a.dwOSVersionInfoSize);

            a.dwMajorVersion = Convert.ToUInt32(version & 0x00000FF);
            Console.WriteLine("Мажорная версия: " + a.dwMajorVersion);

            a.dwMinorVersion = Convert.ToUInt32(version & 0x0000FF00) >> 8;
            Console.WriteLine("Минорная версия: " + a.dwMinorVersion);

            if (version < 0x80000000)
                a.dwBuildNumber = Convert.ToUInt32(version & 0xFFFF0000) >> 16;

            Console.WriteLine("Build: " + a.dwBuildNumber);

            string MachName = Environment.MachineName;
            Console.WriteLine("Имя компьютера: " + MachName);

            string pathFolderWindows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string pathFolderFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string pathFolderData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            Console.WriteLine("Пути к системным каталогам Windows: \n" + pathFolderWindows + "\n" + pathFolderFiles + "\n" + pathFolderData);

            string osVersion = Environment.OSVersion.ToString();
            Console.WriteLine("Версия ос: " + osVersion);
        }
    }
}