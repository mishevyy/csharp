# Semaphore

Класс Semaphore - используется для управления доступом к пулу ресурсов.
Потоки занимают слот семафора, вызывая метод WaitOne(),  и освобождают занятый слот вызовом метода Release().

```c#
public class Program
{
    private static Semaphore pool;
    private static void Work(object number)
    {
        pool.WaitOne();
    
        Console.WriteLine("Поток {0} занял слот семафора.", number);
        Thread.Sleep(1000);
        Console.WriteLine("Поток {0} -----> освободил слот.", number);
    
        pool.Release();
    }
    
    public static void Main()
    {
        // Первый аргумент:
        // Задаем количество слотов для использования в данный момент (не более максимального клоличества).
        // Второй аргумент:
        // Задаем максимальное количество слотов для данного семафора.
        pool = new Semaphore(2, 4, "MySemafore65487563487");
    
        for (int i = 1; i <= 8; i++)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(Work));
            thread.Start(i);
        }
        Thread.Sleep(2000);
        pool.Release(2);
    }
}
```
