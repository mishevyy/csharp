# Управление пулом потоков

Рекомендуется не создавать потоки в ручную через класс Thread, а использовать пул потоков

```c#
public class ThreadPoolWorker<TResult>
{
    public readonly Func<object, TResult> _func;
    private TResult result;

    public ThreadPoolWorker(Func<object, TResult> func)
    {
        _func = func ?? throw new ArgumentException(nameof(func));
        result = default;
    }
    
    public bool Success { get; private set; } = false;
    public bool Completed { get; private set; } = false;
    public Exception Exception { get; private set; } = null;
    
    public TResult Result
    {
        get
        {
            while (Completed == false)
            {
                Thread.Sleep(50);
            }
    
            return Success == true && Exception == null ? result : throw Exception;
        }
    }
    
    public void Start(object state)
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), state);
    }
    
    private void ThreadExecution(object state)
    {
        try
        {
            result = _func.Invoke(state);
            Success = true;
        }
        catch (Exception ex)
        {
            Exception = ex;
            Success = false;
        }
        finally
        {
            Completed = true;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ThreadPoolWorker<int> threadPoolWorker = new ThreadPoolWorker<int>(SumNumber);
        threadPoolWorker.Start(1000);
    
        while (threadPoolWorker.Completed == false)
        {
            Console.Write("*");
            Thread.Sleep(35);
        }
    
        Console.WriteLine();
        Console.WriteLine($"Результат асинхронной операции = {threadPoolWorker.Result:N}");
    }
    
    private static int SumNumber(object arg)
    {
        int number = (int)arg;
        int sum = 0;    
        for (int i = 0; i < number; i++)
        {
            sum += i;
            Thread.Sleep(1);
        }
        return sum;
    }
}
```

