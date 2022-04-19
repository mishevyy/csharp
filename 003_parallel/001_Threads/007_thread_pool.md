# Пул потоков CLR

CLR способна управлять собственным пулом потоков, то есть набором готовых потоков, доступных для использования приложения. Для каждого экземпляра CLR существует свой пул, используемый всеми доменами приложений, находящимися под управлением экземпляра CLR. Если в один процесс загружается несколько экземпляров CLR, для каждого из них формируется собственный пул.

При инициализации CLR пул потоков пуст. При выполнений асинхронной задачи, выполняется соответствующий запрос на размещении в пуле потоков. Код пула извлекает записи из очереди и распределяет их среди потоков из пула. Если пул пуст, создается новый поток. При завершении операции поток не удаляется, а возвращается в пул.

Для добавления в очередь пула потоков применяется статический класс `ThreadPool.QueueUserWorkItem`, он имеет делегаты с такими же перегрузками как класс `Thread`

```c#
static void Main()
{
    Console.WriteLine("Main thread: queuing an asynchronous operation");
	ThreadPool.QueueUserWorkItem(ComputeBoundeOp, 5);

    Console.WriteLine("Main thread: Doing other work here...");
    Thread.Sleep(10000);
}

static void ComputeBoundeOp(object? state)
{
    Console.WriteLine("In ComputeBoundOp: state={0}", state);
    Thread.Sleep(1000);
} 
```

## Скоординированная отмена

Платформа .Net предлагает стандартный паттерн операций отмены. Этот паттерн является скоординированным, то есть требует явной поддержки отмены операции.

```c#
static void Main()
{
    // Для начала потребуется объект CancellationTokenSource
    // Он содержит все состояния необходимые, для управления отмены.
    CancellationTokenSource cts = new CancellationTokenSource();

    // Для предотвращения отмены можно передать CancellationToken.None вместо cts.Token
    // ThreadPool.QueueUserWorkItem(o => Count(CancellationToken.None, 1000));
    
    ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));

    // При желании можно зарегистрировать один или несколько методов, которые выполнятся при отмене
    cts.Token.Register(() => Console.WriteLine("Canceled 1"));
    cts.Token.Register(() => Console.WriteLine("Canceled 2"));


    Thread.Sleep(1000);
    // Вызываем отмену
    cts.Cancel(); // Если метод Count уже вернул управление Cancel не окажет никакого эффекта
    
    // Так же можно вызвать отмену через определенное время
    cts.CancelAfter(TimeSpan.FromSeconds(5));

}

static void Count(CancellationToken token, int countTo)
{
    for(int count = 0; count < countTo; count++)
    {
        if (token.IsCancellationRequested)
        {
            Console.WriteLine("Count is canceled");
            break;
        }

        Console.WriteLine(count);
        Thread.Sleep(200);
    }

    Console.WriteLine("Count is done");
}
```

### Так же можно объединить токены отмены

```c#
static void Main()
{
    // Можно связать отменяемые токены
    var cts1 = new CancellationTokenSource();
    cts1.Token.Register(() => Console.WriteLine("cts1 canceled"));

    var cts2 = new CancellationTokenSource();
    cts2.Token.Register(() => Console.WriteLine("cts2 canceled"));

    // Создание нового объекта, который так же будет отменен при вызове токенов cts1 или cts2
    var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);
    linkedCts.Token.Register(() => Console.WriteLine("linkedCts canceled"));

    cts2.Cancel();

    Console.WriteLine("cts1 canceled={0}, cts2 canceled={1}, linkedCts={2}",
                          cts1.IsCancellationRequested, 
                          cts2.IsCancellationRequested,
                          linkedCts.IsCancellationRequested);
}
```



**Пример создания объекта для возврата результата работы метода.**

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
        ThreadPool.QueueUserWorkItem(ThreadExecution, state);
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
