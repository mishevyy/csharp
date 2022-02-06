# Директивы

_Вывод сообщений в список задач._

```c#
// TODO: Посмотреть в Task List
// HACK: Посмотреть Task List
```

_Определение региона кода._

```c#
#region Вывод информации
    Console.WriteLine("Hello, World!");
#endregion
```

_Выполнение команды в зависимости типа запуска и конфигурации._

```c#
#if DEBUG
    Console.WriteLine("Debug version");
#endif

#if !DEBUG
    Console.WriteLine("Release version");
#endif

#if (DEBUG && !VC_V7)
            Console.WriteLine("DEBUG is defined");
#elif (!DEBUG && VC_V7)
    Console.WriteLine("VC_V7 is defined");
#elif (DEBUG && VC_V7)
    Console.WriteLine("DEBUG and VC_V7 are defined");
#else
    Console.WriteLine("DEBUG and VC_V7 are not defined");
#endif
```

_Определениеошибки._

```c#
#error Ошибка определенная пользователем.
Console.WriteLine(1);
```

_Определение предупреждение первого уровня из определенного места в коде._

```c#
#warning Пользовательское предупреждение.
Console.WriteLine("Hello World!");
```
