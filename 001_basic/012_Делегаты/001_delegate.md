# Делегаты

[делегаты](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/delegates/)

Делегат — это тип, который представляет ссылки на методы с определенным списком параметров и типом возвращаемого значения. Делегаты используются для передачи методов в качестве аргументов к другим методам. Обработчики событий — это ничто иное, как методы, вызываемые с помощью делегатов.

В частности, делегат поддерживает три важных порции информации.

- адрес метода, к которому он делает вызовы;
- аргументы (если они есть) вызываемого метода;
- возвращаемое значение (если есть) вызываемого метода.

Все делегаты унаследованы MulticastDelegate, который унаследован от Delegate и по своей сути являются классам содержащий в себе метод

*Сигнатура делегата должна совпадать с сигнатурой вызываемого метода.*

```c#
public delegate void MyStringDelegate(string s);
public delegate int MyIntDelegate(int a, int b);
```

*Длинная запись и инициализации делегата.*

```c#
// Создание экземпляра делегата и сообщение ему метода TestStringDelegate
MyStringDelegate stringDelegate = new MyStringDelegate(TestStringDelegate);

// Вызов делегата
stringDelegate.Invoke("Hello, World");
```

*Добавление и удаление вызываемых методов в делегат.*

```c#
// Добавление 
stringDelegate += TestStringDelegate;

// Удаление
stringDelegate -= TestStringDelegate;
```

*Техника предположения делегата.*

```c#
MyStringDelegate stringDelegate2 = TestStringDelegate;
stringDelegate2.Invoke("Hello, World");

MyIntDelegate intDelegate = TetsIntDelegate;
int result = intDelegate.Invoke(5, 7);

Console.WriteLine(result);

static void TestStringDelegate(string message)
{
    Console.WriteLine(message);
}

static int TetsIntDelegate(int a, int b)
{
    return a + b;
}
```

## Лямбда методы и анонимные методы

*Анонимный метод.*

```c#
Action<int> action = delegate (int x)
{
    Console.WriteLine(x);
};
action(10);
```

*Лямбда выражение.*

```c#
Action<int> action2 = (x) => Console.WriteLine(x);
action2(10);
```

## Обобщенные делегаты

Дженерик делегат. `<T>` - тип, который будет принимать делегат, или возвращать.

1. Указатель типа, является входным параметром
`delegate void GenericDelegateVar1<T>(T t);`

2. Указатель типа, является выходным параметром
`delegate T GenericDelegateVar2<T>();`

3. Указатель типа, T - входной параметр, R - выходной параметр
`delegate R GenericDelegateVar3<T, R>(T r);`

4. Указатель типа, несколько входных параметров. Важно указывать выходной тип, последним параметром.
`delegate R GenerciDelegateVar4<T1, T2, R>(T1 t1, T2 t2);`

5. Явное указывание вида указателя
`delegate void GenericDelegateVar5In<in T>(T t);`
`delegate T GenericDelegeteVar5Out<out T>();`

### Делегаты Func и Action

В BCl существует 2 универсальных делегата, подходящих почти на все случаи жизни, их использование избовляет от написания собственных делегатов

`Action` - не имеет возвращаемого значения, может иметь до 16 дженерик типов

`Func` -  имеет возвращаемое значения, может иметь до 16 дженерик типов

```c#
static void Main()
{
    // Пример вызова и работы Action
    Action<string, ConsoleColor, int> actionMessage = DisplayMessage;
    actionMessage.Invoke("Hello, World!", ConsoleColor.Magenta, 5);

    // Пример вызова и работы Func. Последний параметр - вызываемое значение
    Func<int, int, int> func = Add;
    int res = func.Invoke(5, 7);

    Console.WriteLine(res);
}

static void DisplayMessage(string msg, ConsoleColor txtColor, int printCount)
{
    ConsoleColor previous = Console.ForegroundColor;
    Console.ForegroundColor = txtColor;

    for (int i = 0; i < printCount; i++)
    {
        Console.WriteLine(msg);
    }

    Console.ForegroundColor = previous;
}

static int Add(int a, int b)
{
    return a + b;
}
```
