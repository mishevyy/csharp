// Параллелизм данных
// Библиотека параллельных задач (TPL) поддерживает параллелизм данных с помощью класса System.Threading.Tasks.Parallel
// Вы пишете логику цикла для Parallel.For или Parallel.ForEach в значительной степени так же, как пишете последовательный цикл
```c#
namespace tpl
{
    class Program
    {
        static void Main()
        {
            int[] stuff = Enumerable.Range(0, 100).ToArray();

            // Последовательная обработка
            foreach (int i in stuff)
                Process(i);

            Console.WriteLine(new String('*', 25));

            // Паралельная foreach
            Parallel.ForEach(stuff, iterator => Process(iterator));

            // Паралельная for
            Parallel.For(0, stuff.Length, iterator => Process(iterator));

            Parallel.Invoke(
                () => Process(2),
                () => Process(4),
                () => Process(6));

        }

        static void Process(int i)
        {
            Console.WriteLine(Math.Pow(i, 2));
        }
    }
}



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
```

```c#
namespace edu;

class Program
{
    static void Main()
    {

    }

    static int DirectedBytes(string path, string searchPattern, SearchOption searchOption)
    {
        var files = Directory.EnumerateFiles(path, searchPattern, searchOption);
        int masterTotal = 0;

        ParallelLoopResult result = Parallel.ForEach<string, int>(files,
            () => { return 0; },
            (file, loopState, index, taskLocalTotal) =>
            {
                int fileLength = 0;
                FileStream fs = null;
                try
                {
                    fs = File.OpenRead(file);
                }
                catch (IOException) { }
                finally
                {
                    if (fs != null)
                        fs.Dispose();
                }

                return taskLocalTotal + fileLength;
            },
            taskLocalTotal => { Interlocked.Add(ref masterTotal, taskLocalTotal); }
            );

        return masterTotal;
    }
}
```