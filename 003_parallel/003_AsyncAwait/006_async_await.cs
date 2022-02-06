class AwaitableTestTaskScheduler : TaskScheduler
{
    private int counter = 0;
    string[] names = { "ПЕРВЫЙПОТОК", "ВТОРОЙПОТОК", "ТРЕТИЙПОТОК", "ЧЕТВЕРТЫЙПОТОК" };

    protected override void QueueTask(Task task)
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"QueueTask сработал для задачи - {task.Id}");
        Console.ResetColor();
        new Thread(_ => base.TryExecuteTask(task)) { IsBackground = true, Name = names[counter++] }.Start();
        //ThreadPool.QueueUserWorkItem(_ => base.TryExecuteTask(task));
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"TryExecuteTaskInline сработал для задачи - {task.Id}");
        Console.ResetColor();
        return base.TryExecuteTask(task);
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return null;
    }
}

class Program
{
    private static async Task Main()
    {
        Console.SetWindowSize(90, 40);


        ShowData("Main выполнился до await");

        var mainTask = new Task<Task>(MethodAsync);
        mainTask.Start(new AwaitableTestTaskScheduler());
        var result = await mainTask;                    // await await mainTask;
        await result;

        ShowData("Main выполнился после await");
    }

    private static async Task MethodAsync()
    {
        ShowData("MethodAsync выполнился до await");

        var task = new Task(TestMethod);
        task.Start();

        await task;

        ShowData("MethodAsync выполнился после await");
    }

    private static void TestMethod()
    {
        ShowData("TestMethod выполнился");
    }

    private static void ShowData(string description)
    {
        Console.WriteLine($"{description}");

        Console.WriteLine($"Имя потока: {Thread.CurrentThread.Name} ");
        Console.WriteLine($"Id потока: {Thread.CurrentThread.ManagedThreadId}. Поток из пула потоков: {Thread.CurrentThread.IsThreadPoolThread}");
        Console.WriteLine($"Id задачи: {Task.CurrentId}");
        Console.WriteLine($"Текущий планировщик задач: {typeof(TaskScheduler).GetProperty("InternalCurrent", BindingFlags.Static | BindingFlags.NonPublic).GetValue(typeof(TaskScheduler))}");

        Console.WriteLine(new string('-', 80));
        Console.WriteLine();
    }
}