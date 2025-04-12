using System.Threading;
using System;
using System.Reflection;

namespace threads
{
    class Program
    {
        static int threadsAmount;                           //<---кількість потоків
        public Thread[] threads = new Thread[threadsAmount];
        public int[] timer = new int[threadsAmount];
        bool[] canStop = new bool[threadsAmount];
        static void Main(string[] args)
        {
            Console.Write("Введiть кiлькiсть потокiв: ");
            threadsAmount = Int32.Parse(Console.ReadLine());
            (new Program()).Start();
        }
        void Start()
        {
            Console.Write($"Введiть час роботи для {threadsAmount} потокiв: ");
            string[] times = Console.ReadLine().Trim().Split();
            for (int i = 0; i < threads.Length; i++)
            {
                timer[i] = Int32.Parse(times[i]) * 1000;
            }

            for (int i = 0; i < threads.Length; i++)
            {
                int j = i;
                threads[i] = new Thread(() => Sum(j));
                threads[i].Name = $"Потiк {i}";
                threads[i].Start();
                canStop[i] = false;
            }
            (new Thread(Stoper)).Start();
        }
        void Sum(int index)
        {
            int step = 1;                                           //<---крок
            long sum = 0;
            long curr = 0;
            long components = 0;
            do
            {
                sum += curr;
                curr += step;
                components++;
            } while (!canStop[index]);

            PrintResut(sum, components, timer[index]);
        }
        void PrintResut(long sum, long comp,int time)
        {

            Console.WriteLine($"------{Thread.CurrentThread.Name}------ " +
                $"\nid: {Thread.CurrentThread.ManagedThreadId} " +
                $"\nсума: {sum} " +
                $"\nкiлькiсть доданкiв: {comp} " +
                $"\nчас: {time/1000} секунд");
        }

        public void Stoper()
        {
            int mainTime = (timer.Max())*1000;
            int currentTime = 0;
            while (currentTime<=mainTime){
                for (int i = 0; i < threads.Length; i++)
                {
                    if (currentTime >= timer[i])
                    {
                        canStop[i] = true;
                    }
                }

                
                if (canStop.All(stop => stop))
                {
                    break;
                }

                Thread.Sleep(1000);
                currentTime += 1000;
            }

            
        }
    }
}

