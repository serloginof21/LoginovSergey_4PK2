namespace PZ_5
{
    internal class Program
    {
        static object lockObject = new object();
        static bool isThread2Blocked = false;
        static int pauseVal = 50;
        static int criticalValue = 100;
        int value = 2;

        static void Main(string[] args)
        {
            Program p = new Program();

            Thread thread1 = new Thread(p.Thread1Method);
            Thread thread2 = new Thread(p.Thread2Method);

            thread1.Start();
            thread2.Start();
        }
        public void Thread1Method()
        {
            while (true)
            {
                Thread.Sleep(1200);
                Console.WriteLine("Поток 1: Получено из потока 2: " + value);

                if (value >= criticalValue)
                {

                    Console.WriteLine("Остановка Потока 2.");
                    StopThread2();
                    break;
                }
            }
        }
        void Thread2Method()
        {
            while (true)
            {
                value *= 2;
                Thread.Sleep(1000);

                if (value >= criticalValue)
                {
                    Console.WriteLine("Остановка потока 1.");
                    StopThread1();
                    break;
                }
            }
        }
        static void StopThread1()
        {
            lock (lockObject)
            {
                Monitor.Pulse(lockObject);
                isThread2Blocked = true;
            }
        }
        static void StopThread2()
        {
            lock (lockObject)
            {
                Monitor.Pulse(lockObject);
            }
        }
    }
}