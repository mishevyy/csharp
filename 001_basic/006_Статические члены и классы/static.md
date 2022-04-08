# Статические члены и классы

## Статические члены

Свойства статических членов

* Статическое поле одно, для всех экземпляров класса
* Для статических полей, нужно реализовывать  статические свойства

Статический конструктор

* Статический конструктор не имеет модификаторов доступа, и не может иметь параметров
* Статический конструктор вызывается первым, для инициализации статических полей (не будет вызываться если объект уже существует)

```c#
class MyClass
{         
    public static string StaticField { get; set; }
    
    private static int _count;
    public static int Count
    {
        get => _count;
        set => _count = value;
    }
         
    static MyClass()
    {
        StaticField = "Hello static";
        Console.WriteLine(StaticField);
    }

    public static void Method()
    {
        Console.WriteLine("Hello");
    }
}
```

## Статические классы

* Статические класс не может наследовать и не может наследоваться
* Статический класс может содержать только статические члены

```c#
static class MyStaticClass
{
    public static int StaticField { get; set; }
    
    static MyStaticClass()
    {
        Console.WriteLine("Static Constructor");
    }

    public static void StaticMethod()
    {
        Console.WriteLine("StaticMethod");
    }  
}
```
