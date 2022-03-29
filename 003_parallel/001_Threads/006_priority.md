# Приоритеты потоков

Не рекомендуется ручное управление приоритетами!

```c#
class ThreadWork
{
    public Thread RunThread;
    static bool _stop;
    readonly ConsoleColor color;

    public int Count { get; set; }
    
    public ThreadWork(string name, ConsoleColor color)
    {
        RunThread = new Thread(Run) { Name = name };
        this.color = color;
        Console.ForegroundColor = this.color;
        Console.WriteLine("Поток " + RunThread.Name + " начат.");
    }
    
    void Run()
    {
        do
        {
            Count++;
        }
        while (_stop == false && Count < 20000000);
    
        _stop = true;
        Console.WriteLine("\nПоток " + RunThread.Name + " завершен.");
    }
    
    public void BeginInvoke()
    {
        RunThread.Start();
    }
    
    public void EndInvoke()
    {
        RunThread.Join();
    }
    
    public ThreadPriority Priority
    {
        set { RunThread.Priority = value; }
    }
}

class Program
{
    static void Main()
    {
        Console.SetWindowSize(80, 40);

        var work1 = new ThreadWork("с высоким приоритетом", ConsoleColor.Red);
        var work2 = new ThreadWork("с низким приоритетом", ConsoleColor.Yellow);
    
        // Установить приоритеты для потоков. 
        work1.Priority = ThreadPriority.Highest;
        work2.Priority = ThreadPriority.Lowest;
    
        work2.BeginInvoke();
        work1.BeginInvoke();
    
        work1.EndInvoke();
        work2.EndInvoke();
    
        Console.WriteLine();
        Console.WriteLine("Поток " + work1.RunThread.Name + " досчитал до " + work1.Count);
        Console.WriteLine("Поток " + work2.RunThread.Name + " досчитал до " + work2.Count);
    }
}
```
