# Класс Console

[Консоль](https://docs.microsoft.com/ru-ru/dotnet/api/system.console?view=net-5.0)

*Установить цвет фона консоли.*

```c#
Console.BackgroundColor = ConsoleColor.Red;
```

*Установить цвет текста консоли.*

```c#
Console.ForegroundColor = ConsoleColor.Cyan;
```

*Издает сигнал спикерфона.*

```c#
Console.Beep();
```

*Устанавливает позицию курсора.*

```c#
Console.SetCursorPosition(15, 5);
```

**Определение нажатой клавиши.**

```c#
private static void PressKeyDefinition()
{
    ConsoleKeyInfo cki;
    Console.TreatControlCAsInput = true;
    Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
    Console.WriteLine("Press the Escape (Esc) key to quit: \n");
    do
    {
        cki = Console.ReadKey();
        Console.Write(" --- You pressed ");
        if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
        if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");
        if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
        Console.WriteLine(cki.Key.ToString());
    } while (cki.Key != ConsoleKey.Escape);
}
```

**Перенаправление потока консоли на запись в файл.**

```c#
private static void RedirectionConsoleStream()
{            
    Console.WriteLine("Hello World");
    FileStream fs = new FileStream("Test.txt", FileMode.Create);
    TextWriter tmp = Console.Out;
    StreamWriter sw = new StreamWriter(fs);
    Console.SetOut(sw);
    Console.WriteLine("Hello file");
    Console.SetOut(tmp);
    Console.WriteLine("Hello World");
    sw.Close();
}
```


```c#
// Вывод в консоль с отступами
Console.WriteLine("{0, -10} {1, -5}", 123, 872321);
Console.WriteLine("{0, -10} {1, -5}", 123234, 215);
Console.WriteLine("{0, -10} {1, -5}", 12312, 2167);
Console.WriteLine("{0, -10} {1, -5}", 12323, 2112);
Console.WriteLine("{0, -10:X} {1, -5}", 12323, 2112);
Console.WriteLine("{0, -10:D} {1, -5}", 12323, 2112);
Console.WriteLine("{0, -10:C} {1, -5}", 12323, 2112);
```