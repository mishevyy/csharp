class Program
{
    static async Task Main()
    {
        Console.WriteLine($"Метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

        // Запуск метода асинхронно
        await WriteCharAsync('#');

        // Запуск метода синхронно
        WriteChar('*');

        Console.WriteLine($"\nМетод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");
    }

    private static async Task WriteCharAsync(char synbol)
    {
        Console.WriteLine($"Метод WriteCharAsync начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");

        await Task.Run(() => WriteChar(synbol));

        Console.WriteLine($"\nМетод WriteCharAsync закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}.");
    }

    private static void WriteChar(char symbol)
    {
        Console.WriteLine($"Id потока - [{Thread.CurrentThread.ManagedThreadId}]. Id задачи - [{Task.CurrentId}]");
        Thread.Sleep(100);

        for (int i = 0; i < 80; i++)
        {
            Console.Write(symbol);
            Thread.Sleep(100);
        }
    }
}