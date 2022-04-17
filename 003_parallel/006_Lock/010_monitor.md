# Блокировки. Монитор

Монитор является самой распространенным объектом для управления блокировками потоков

```c#
class Program
{
    // Объект для блокировки.
    static private readonly object block = new object();

    // Счетчик потоков.
    static private int counter;
    static private readonly Random random = new Random();
    
    // Выполняется в отдельном потоке.
    private static void Function()
    {
        // Управляющий поток увеличивает счетчик и ожидает
        // произвольный период времени от 1 до 12 секунд.
        try
        {
            Monitor.Enter(block); // Начало блокировки.
            ++counter;
        }
        finally
        {
            Monitor.Exit(block);  // Конец блокировки.
        }
    
        int time = random.Next(1000, 12000);
        Thread.Sleep(time);
    
        try
        {
            Monitor.Enter(block); // Начало блокировки.
            --counter;
        }
        finally
        {
            Monitor.Exit(block);  // Конец блокировки.
        }
    }
    
    // Мониторинг количества запущеных потоков.
    private static void Report()
    {
        while (true)
        {
            int count;    
            try
            {
                Monitor.Enter(block);// Начало блокировки.
                count = counter;
            }
            finally
            {
                Monitor.Exit(block);
            }
    
            Console.WriteLine("{0} поток(ов) активно", count);
            Thread.Sleep(100);
        }
    }
    
    static void Main()
    {
        var reporter = new Thread(Report) { IsBackground = true };
        reporter.Start();
    
        var threads = new Thread[150];
    
        for (uint i = 0; i < 150; ++i)
        {
            threads[i] = new Thread(Function);
            threads[i].Start();
        }
    
        Thread.Sleep(15000);
    }
}
```
