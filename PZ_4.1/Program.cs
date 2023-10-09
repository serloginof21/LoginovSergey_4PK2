using System.Threading;

class DataBase
{
    public void SaveData(string text)
    {
        lock (this)
        {
            Console.WriteLine($"Сохранение данных: {text}");
            Thread.Sleep(1000); // Имитация долгой операции сохранения
            Console.WriteLine($"Данные сохранены: {text}");
        }
    }
}

namespace PZ_4._1
{
    internal class Program
    {
        public static DataBase db = new DataBase();
        public static void WorkerThreadMethod1()
        {
            Console.WriteLine("Поток 1 начал работу");
            for (int i = 0; i < 5; i++)
            {
                db.SaveData($"Поток 1, итерация {i}");
            }
            Console.WriteLine("Поток 1 завершил работу");
        }

        public static void WorkerThreadMethod2()
        {
            Console.WriteLine("Поток 2 начал работу");
            for (int i = 0; i < 5; i++)
            {
                db.SaveData($"Поток 2, итерация {i}");
            }
            Console.WriteLine("Поток 2 завершил работу");
        }

        static void Main(string[] args)
        {
            ThreadStart work1 = new ThreadStart(WorkerThreadMethod1);
            ThreadStart work2 = new ThreadStart(WorkerThreadMethod2);

            Thread thread1 = new Thread(work1);
            Thread thread2 = new Thread(work2);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Работа программы завершена");
        }

    }
}