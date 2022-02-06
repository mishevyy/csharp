using System;
using System.Linq;
using System.Collections.Generic;

namespace PLINQ
{
    internal class Program
    {
        private static void Main()
        {
            // ForAll

            Console.SetWindowSize(80, 35);
            IEnumerable<int> range = ParallelEnumerable.Range(0, 100_000_000);

            var query = from num in range
                                    .AsParallel()
                        where (num & (num - 1)) == 0
                        select num;

            query.ForAll((item) =>
            {
                Console.WriteLine($"{item:N}");
            });

            Console.WriteLine($"\n\nКонец работы метода Main.");
            Console.ReadKey();
        }
    }
}
