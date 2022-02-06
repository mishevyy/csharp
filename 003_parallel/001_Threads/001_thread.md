# Потоки

Многозадачность - свойство операционной системы или среды программирования обеспечивать возможность параллельной (или псевдопараллельной) обработки нескольких процессов. CLR назначает каждому потоку свой стек и методы имеют свои собственные локальные переменные.

## Thread

Основным классом управления потоками является класс `Thread`. Он позволяет создать экземпляр и передать ему через параметр делегат `ThreadStart` или параметризированный делегат ParameterizedThreadStart, ограничением является то что ParameterizedThreadStart принимает один аргумент типа `object?`

### *Инициализация потока, и сообщение ему метода.*

```c#
Thread thread = new Thread(Foo);
thread.Start();  

static void Foo()
{
}
```

### *Инициализация потока с параметром.*

```c#
Thread parametrizeThread = new Thread(ParametrizeThread);
parametrizeThread.Start('*');

static void ParametrizeThread(object? temp)
{    
}
```

### *Передача анонимного метода в поток.*

```c#
Thread thread = new Thread((object argument) => 
{
    Console.WriteLine(new string(' ', 30) + "{0}", (int)argument); 
});
thread.Start(5);
```

### *Получение информации о потоке.*

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

### [ThreadStatic]

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
Thread thread = new Thread(Foo);
thread.Start();  

thread.IsBackground = true; // Не ждем завершения вторичного потока в данном случае.
```

### Прерывание выполнения потока

При вызове Abort() -  сгенерируется исключение ThreadAbortException! Не рекомендуется использовать этот метод!

```c#
Thread thread = new Thread(Foo);
thread.Start();
thread.Abort();
```
