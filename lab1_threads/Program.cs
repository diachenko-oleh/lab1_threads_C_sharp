using System.Threading;
using System;

namespace threads
{
    class Program
    {
     
        static void Main(string[] args)
        {
            (new Program()).Start();
        }
        void Start()
        {
            int threadsAmount = 4;                           //<---кількість потоків
            Thread[] threads = new Thread[threadsAmount];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Sum);
                threads[i].Name = $"Потiк {i}";
                threads[i].Start();
            }
            (new Thread(Stoper)).Start();
        }
        void Sum()
        {
            int step = 2;                                           //<---крок
            long sum = 0;
            long curr = 0;
            long components = 0;                                     //<---кількість елементів послідовності
            while (!canStop)
            {
                sum += curr;
                curr += step;
                components++;
            }

            PrintResut(sum,components);
        }
        void PrintResut(long sum, long comp)
        {

            Console.WriteLine($"------{Thread.CurrentThread.Name}------ " +
                $"\nid: {Thread.CurrentThread.ManagedThreadId} " +
                $"\nсума: {sum} " +
                $"\nкiлькiсть доданкiв: {comp}");
        }

        private bool canStop = false;
        public bool CanStop { get => canStop; }

        public void Stoper()
        {
            Thread.Sleep(30 * 1000);
            canStop = true;
        }
    }
}

