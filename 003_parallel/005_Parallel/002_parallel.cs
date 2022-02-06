using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("У Вашего процессора {0} ядер.", Environment.ProcessorCount);

            Stopwatch sw = new Stopwatch();
            int[] values = new int[5] { 1, 2, 3, 4, 5 };

            double[] data = new double[10_000_000];

            sw.Start();
            // Параллельный вариант инициализации массива в цикле.
            //       for (int i = 0; i < data.Length; i++)
            Parallel.For(0, data.Length, (i) =>
            {
                // Имитация сложных вычислений...
                double element = i * values.Sum() * 250 * 350 * 450;
                data[i] = element;
            });
            sw.Stop();

            Console.WriteLine($"Параллельно выполняемый цикл инициализации: {sw.ElapsedMilliseconds:N0}.");
            sw.Reset();

            double[] data2 = new double[10_000_000];
            sw.Start();
            // Последовательный вариант инициализации массива в цикле.
            for (int i = 0; i < data2.Length; i++)
            {
                // Имитация сложных вычислений...
                double element = i * values.Sum() * 250 * 350 * 450;
                data2[i] = element;
            }
            sw.Stop();

            Console.WriteLine($"Последовательно выполняемый цикл инициализации: {sw.ElapsedMilliseconds:N0}");

            // Delay.
            Console.ReadKey();
        }
    }
}
