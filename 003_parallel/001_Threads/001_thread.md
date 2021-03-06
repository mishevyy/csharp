# Потоки

Многозадачность - свойство операционной системы или среды программирования обеспечивать возможность параллельной (или псевдопараллельной) обработки нескольких процессов. CLR назначает каждому потоку свой стек и методы имеют свои собственные локальные переменные.

Компьютер с одним процессором может одновременно выполнять только что-то одно. Следовательно, операционная система должна распределять физический процессор между всеми своими потоками (логическими процессами).

В произвольный момент времени ОС передает процессору на исполнение один поток. Этот поток исполняется в течении некоторого временного интервала, иногда называемого тактом. После завершения этого интервала контекст ос переключается на другой поток. После переключения контекста процессор исполняет выбранный поток, пока не истечет выделенное время потока, после этого снова происходит переключение контекста. 

Время переключения контекста зависит от архитектуры и быстродействия процессора, поэтому оценить трудозатраты достаточно сложно, но важно помнить, что при разработке переключение контекста нужно по возможности избегать, так как они потребляют память и требуют время для своего создания, управления и завершения. С другой стороны потоки обеспечивают отзывчивость и увеличивают время реакции приложения. Так же не стоит забывать, что компьютер с несколькими процессорами исполняет несколько потоков одновременно.

## Управление потоками

Основным классом управления потоками является класс `Thread`. Он позволяет создать экземпляр и передать ему через параметр делегат `ThreadStart` или параметризированный делегат `ParameterizedThreadStart`, ограничением является то что ParameterizedThreadStart принимает один аргумент типа `object?`. Для создания физического потока, следует воспользоваться методом Start, класса Thread.

```c#
static void Main()
{
    Console.WriteLine("Метод Main начал работу");
    Thread dedicatedThread = new Thread(ComputeBoundOp);
    dedicatedThread.Start(5);

    Console.WriteLine("Main выполняет свою работу");
    Thread.Sleep(10000); // Имитация другой работы (10 сек)

    // Ожидание завершения потока, те другие потоки не начнут свою работу пока не завершиться текущий
    dedicatedThread.Join();
}

static void ComputeBoundOp(object? state) // Метод выполняемый выделеным потоком
{
    Console.WriteLine("In ComputeBoundOp: state={0}", state);
    Thread.Sleep(1000);

    // После возвращения методом работы выделеный поток завершается
}
```

### Получение информации о потоке

`Thread.CurrentThread` - возвращает ссылку на экземпляр текущего потока.

```c#
static void ThreadInfo()
{
    Thread thread = Thread.CurrentThread;

    // Присваиваем потоку имя.
    thread.Name = "Secondary";

    // Выводим на экран информацию о текущем потоке.
    Console.WriteLine("ID потока {0}: {1}", thread.Name, thread.GetHashCode());

    for (int counter = 0; counter < 10; counter++)
    {
        Console.WriteLine(new string(' ', 15) + thread.Name + " " + counter);
        // Приостанавливаем выполнение текущего потока.
        Thread.Sleep(1000);
    }
}
```

### ThreadStatic

*Переменная становится статичной для каждого потока, а не всего приложения.*

```c#
class Program
{
    [ThreadStatic]
    public static int counter;
}
```

### Основные и фоновые потоки

Есть два варианта работы потоков Foreground и Background:

1. Foreground - Будет работать после завершения работы первичного потока.
2. Background - Завершает работу вместе с первичным потоком.

По умолчанию свойство IsBackground равно false.  IsBackground - устанавливает поток как фоновый.

```c#
static void Main()
{
    Thread t = new Thread(Worker);
    t.IsBackground = true;
    t.Start();

    // В случае активного потока приложение проработает 10 секунд,
    // в случае фонового прекратит работу сразу.
    Console.WriteLine("Return from Main");
}

static void Worker()
{
    Thread.Sleep(10000);
    Console.WriteLine("Return from Worker");
}
```

### Прерывание выполнения потока

При вызове Abort() -  сгенерируется исключение ThreadAbortException! Не рекомендуется использовать этот метод!

```c#
Thread thread = new Thread(Foo);
thread.Start();
thread.Abort();
```

## Объединение потоков

*Метод Join.*

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
    
        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(20);
            Console.Write("-");
        }
        
        Console.WriteLine("\nПервичный поток завершился.");
    }
    
    private static void ThreadMethod()
    {
        Console.WriteLine("ID Вторичного потока: {0}", Thread.CurrentThread.ManagedThreadId);
    
        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(100);
            Console.WriteLine(".");
        }
        
        Console.WriteLine("Вторичный поток завершился.");
    }
}
```

## Приоритеты потоков

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

## Ключевое слово volatile

Поля, объявленные как volatile, не проходят оптимизацию компилятором, которая предусматривает доступ посредством отдельного потока. Это гарантирует наличие наиболее актуального значения в поле в любое время. Ключевое слово гарантирует что при чтении и записи  манипуляция будет происходить непосредственно с памятью а не со значениями, которые кэшированы в регистры процессора.

Можно применять к полям следующих типов:

* Ссылочные типы.
* Типы, такие как sbyte, byte, short, ushort, int, uint, char, float и bool.
* Тип перечисления с одним из следующих базовых типов: byte, sbyte, short, ushort, int или uint.
* Параметры универсальных типов, являющиеся ссылочными типами.
* Ключевое слово volatile можно применить только к полям класса или структуры.
* Локальные переменные не могут быть объявлены как volatile.

```c#
class Program
{
    static volatile bool stop; // Без JIT оптимизации.
    // static bool stop; // С JIT оптимизацией.
    
    static void Main()
    {
        Console.WriteLine("Main: запускается поток на 2 секунды.");
        var thread = new Thread(Worker);
        thread.Start();
    
        Thread.Sleep(2000);
    
        stop = true;
        Console.WriteLine("Main: ожидание завершения потока");
        thread.Join();
    }
    
    private static void Worker(Object o)
    {
        // При компиляции данного кода JIT компилятор обнаружит, что переменная stop не меняется в методе.
        // JIT Компилятор может создать код, заранее проверяющий состояние переменной stop
        // и если она true сразу вывести результат "Worker: остановлен при x = 0"
        // В противном случае JIT компилятор создает код входящий в бесконечный цикл и бесконечно увеличивающий переменную x.
    
        // ВНИМАНИЕ! Оптимизация не производиться в режиме отладки (DEBUG).
        int x = 0;
    
        while (!stop)
        {
            // checked
            {
                x++;
            }
        }
    
        // Код после JIT оптимизации, если stop не voatile:
        // (Если stop - volatile - то оптимизация JIT компилятором производиться не будет.)
        // int x = 0;         
        // if (stop != true)
        // {
        //     while (true)
        //     {
        //         x++;
        //     }
        // }            
    
        Console.WriteLine("Worker: остановлен при x = {0}.", x);
    }
}
```
