using System;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;

namespace PLINQ
{
    internal class Program
    {
        private static ConcurrentDictionary<int, int> dictionary = new ConcurrentDictionary<int, int>();

        private static bool WhereFilter(int number, int index)
        {
            dictionary.AddOrUpdate(Thread.CurrentThread.ManagedThreadId, 1, (key, value) => ++value);
            return number % 2 == 0;
        }

        private static void Main()
        {
            // WithExecutionMode

            Console.SetWindowSize(100, 40);

            var parallelQuery = Enumerable.Range(0, 10000)
                                    .AsParallel()
                                    //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                    .Where(WhereFilter)
                                    .Select((num, index) => num);

            foreach (var item in parallelQuery)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine($"\n\nРезультаты обработки потоков:");
            foreach (var item in dictionary)
            {
                Console.WriteLine($"Поток #{item.Key} обработал {item.Value} элементов.");
            }


            Console.WriteLine($"\n\nКонец работы метода Main");
            Console.ReadKey();
        }
    }
}
