using System;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;

namespace PLINQ
{
    internal class Program
    {
        private static ConcurrentDictionary<int, int> dictionary = new ConcurrentDictionary<int, int>();

        private static bool WhereFilter(int number)
        {
            dictionary.AddOrUpdate(Thread.CurrentThread.ManagedThreadId, 1, (key, value) => ++value);
            return number % 2 == 0;
        }

        private static void Main()
        {
            // WithDegreeOfParallelism

            var query = from num in Enumerable.Range(0, 1000)
                            .AsParallel()
                            .WithDegreeOfParallelism(Environment.ProcessorCount / 2)
                        where WhereFilter(num)
                        select num;

            foreach (var item in query)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine($"\n\nРезультаты обработки потоков:");
            foreach (var item in dictionary)
            {
                Console.WriteLine($"Поток #{item.Key} обработал {item.Value} элементов.");
            }

            Console.WriteLine($"\n\nКонец работы метода Main.");
            Console.ReadKey();
        }
    }
}
