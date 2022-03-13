# Объединение потоков

## *Метод Join*

```c#
class Program
{
    static void Main()
    {        
        Thread thread = new Thread(ThreadMethod);

        Console.WriteLine("ID Первичного потока: {0} \n", Thread.CurrentThread.GetHashCode());
        Console.WriteLine("Запуск нового потока...");
    
        thread.Start();
    
        Console.WriteLine(Thread.CurrentThread.GetHashCode());
    
        // Ожидание первичным потоком завершения вторичного потока
        thread.Join();
    
        Console.ForegroundColor = ConsoleColor.Green;
    
        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(20);
            Console.Write("-");
        }
    
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\nПервичный поток завершился.");
    }
    
    private static void ThreadMethod()
    {
        Console.WriteLine("ID Вторичного потока: {0}", Thread.CurrentThread.ManagedThreadId);
        Console.ForegroundColor = ConsoleColor.Yellow;
    
        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(100);
            Console.WriteLine(".");
        }
    
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Вторичный поток завершился.");
    }
}
```