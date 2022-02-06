# Делегаты

[делегаты](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/delegates/)

[ключевое слов делегат](https://docs.microsoft.com/ru-ru/previous-versions/visualstudio/visual-studio-2008/900fyy8e(v=vs.90)?redirectedfrom=MSDN)

[анонимные методы](https://docs.microsoft.com/ru-ru/previous-versions/visualstudio/visual-studio-2008/0yw3tz5k(v=vs.90)?redirectedfrom=MSDN)

В C# обратные вызовы выполняються в безопасной к типам объектно-оринтированной манере с использованием делегатов. В сущности, делегат - это безопасный в отношении типов обект, указывающий на другой метод или возможно на список методов приложения, которые могут быть вызванны в более позднее время. В частности, делегат поддерживает три важных порции информации.

- адрес метода, к которому он делает вызовы;
- аргументы (если они есть) вызываемогог метода;
- возвращаемое значение (если есть) вызываемого метода. 

Все делегаты унаследованны MulticastDelegate, который унаследован от Delegate и по своей сути являются классам содержащий в себе метод

Сигнатура делегата

delegate Тип_возвращаемого_значения Имя_делегата (параметры делегата)

_Сигнатура делегата должна совпадать с сигнатурой вызываемого метода._

```c#
public delegate void MyStringDelegate(string s);
public delegate int MyIntDelegate(int a, int b);
```

_Длинная записи инициализации делегата._

```c#
MyStringDelegate stringDelegate = new MyStringDelegate(TestStringDelegate);
stringDelegate.Invoke("Hello, World");
```

_Добавление и удаление вызываемых методов в делегат._

```c#
// Добавление 
stringDelegate += TestStringDelegate;

// Удаление
stringDelegate -= TestStringDelegate;
```

_Техника предположения делегата._

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

## Лямбда методы

_Анонимный метод._

```c#
Action<int> action = delegate (int x)
{
    Console.WriteLine(x);
};
action(10);
```

_Лямбда выражение._

```c#
Action<int> action2 = (x) => Console.WriteLine(x);
action2(10);
```
