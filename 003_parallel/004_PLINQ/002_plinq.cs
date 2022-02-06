using System;
using System.Linq;
using System.Collections.Generic;

namespace PLINQ
{
    internal class Program
    {
        private static void Main()
        {
            // AsOrdered, AsUnordered

            Console.SetBufferSize(100, 10000);
            IEnumerable<int> range = Enumerable.Range(0, 100_000);

            var query1 = from num in range
                            .AsParallel()
                            .AsOrdered()
                         where num % 2 == 0
                         select num;

            foreach (var item in query1)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine($"\n\n");
            Console.ReadKey();

            var query2 = from num in query1
                         .AsUnordered()
                         where (num & (num - 1)) == 0
                         select num;

            foreach (var item in query2)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine($"\n\nКонец работы метода Main.");
            Console.ReadKey();
        }
    }
}