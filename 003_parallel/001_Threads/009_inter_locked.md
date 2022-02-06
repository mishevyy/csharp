# Блокировки

## Ручная реализация блокировки

```c#
public class SpinLock
{
    // Указывает выполняется ли блок кода потоком. 0 - блок свободен. 1 - блок занят.
    private int block;
    //  Интервал через который потоки проверяют переменную block.
    private readonly int wait;

    public SpinLock(int wait)
    {
        this.wait = wait;
    }
    
    // Установить блокировку.
    public void Enter()
    {
        // Метод CompareExchange() 
        // 1. Сравнивает начальное значение первого аргумента с третьим аргументом.
        // 2. Если первый аргумент равен третьему аргументу, то в первый аргумент записывается значение второго аргумента.
        // 3. Иначе, если первый аргумент не равен третьему аргументу, то первый аргумент остается без изменения.
        // 4. Возвращает начальное значение первого аргумента (в любом случае).
        int result = Interlocked.CompareExchange(ref block, 1, 0);
    
        while (result == 1)
        {
            // Блокировка занята, ожидать.
            Thread.Sleep(wait);
            result = Interlocked.CompareExchange(ref block, 1, 0);
        }
    }
    
    // Сбросить блокировку.
    public void Exit()
    {
        Interlocked.Exchange(ref block, 0);
    }

}

// Логика работы lock
public class SpinLockManager : IDisposable
{
    private readonly SpinLock block;

    public SpinLockManager(SpinLock block)
    {
        this.block = block;
        block.Enter();
    }
    
    public void Dispose()
    {
        block.Exit();
    }

}

class Program
{
    static readonly Random random = new Random();
    static readonly SpinLock block = new SpinLock(10); // Интервал 10 млск.

    static readonly FileStream stream = File.Open("log.txt", FileMode.Append, FileAccess.Write, FileShare.None);
    static readonly StreamWriter writer = new StreamWriter(stream);
    
    // Будет запускаться в отдельном потоке.
    static void Function()
    {
        using (new SpinLockManager(block)) // Вызывается block.Enter();
        {
            writer.WriteLine("Поток {0} запускается.", Thread.CurrentThread.GetHashCode());
            writer.Flush(); // Очищает буфер writer и записывает данные в файл.
        }
    
        int time = random.Next(10, 200);
        Thread.Sleep(time); // Усыпляется поток на случайный период времени.
    
        using (new SpinLockManager(block)) // Вызывается block.Enter();
        {
            writer.WriteLine("Поток [{0}] завершается.", Thread.CurrentThread.GetHashCode());
            writer.Flush(); // Очищает буфер writer и записывает данные в файл.
        }
    }
    
    static void Main()
    {
        var threads = new Thread[50];
        for (uint i = 0; i < 50; ++i)
        {
            threads[i] = new Thread(Function);
            threads[i].Start();
        }
    }
}
```

