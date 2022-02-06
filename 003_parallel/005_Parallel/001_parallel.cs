using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ParallelProgramming
{
    internal class Program
    {
        private static object syncRoot = new object();

        private static void Main()
        {
            // Parallel Invoke

            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.TaskScheduler = new ParallelTaskScheduler();
            parallelOptions.MaxDegreeOfParallelism = 2;

            Action a1 = () => WriteChar(ConsoleColor.DarkRed);
            Action a2 = () => WriteChar(ConsoleColor.DarkYellow);
            Action a3 = () => WriteChar(ConsoleColor.DarkCyan);
            Action a4 = () => WriteChar(ConsoleColor.DarkGreen);

            Parallel.Invoke(parallelOptions, a1, a2, a3, a4);
            //Parallel.Invoke(a1, a2, a3, a4);

            Console.WriteLine($"\n\n\nМетод Main завершен");

            Console.ReadKey();
        }

        private static void WriteChar(ConsoleColor consoleColor)
        {
            Console.WriteLine($"Id задачи {Task.CurrentId}, id потока: {Thread.CurrentThread.ManagedThreadId}. Поток из ThreadPool - {Thread.CurrentThread.IsThreadPoolThread}. ");
            Thread.Sleep(500);

            for (int i = 0; i < 40; i++)
            {
                lock (syncRoot)
                {
                    Console.BackgroundColor = consoleColor;
                    Console.Write($" ");
                    Console.ResetColor();
                    Console.Write($" ");
                }
                Thread.Sleep(100);
            }
        }
    }

    internal class ParallelTaskScheduler : TaskScheduler
    {
        protected override IEnumerable<Task> GetScheduledTasks() => null;

        protected override void QueueTask(Task task)
        {
            Console.WriteLine($"Параллельное выполнение задачи {task.Id}");

            ThreadPool.QueueUserWorkItem((_) =>
            {
                base.TryExecuteTask(task);
            });
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Console.WriteLine($"Синхронное выполнение задачи {task.Id}");
            return base.TryExecuteTask(task);
        }
    }
}
