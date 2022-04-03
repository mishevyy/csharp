# Обобщения (generic)

Обобщения (generic) элемент кода, способный адаптироваться для выполнения сходных действий над различными типами.

Универсальные шаблоны были добавлены в язык C# версии 2.0 и среду CLR. Эта возможность CTS (Common Type System — общая система типов), названа обобщениями (generics).

* Обобщения обеспечивают безопасность типов, так как содержат только тот тип который указан при объявлении
* Обобщения обеспечивают большую производительность, так как не происходит упаковки-распаковки при использовании значимых типов
* Обобщения позволяют создавать открытые типы, которые преобразуются в закрытые при объявлении
* Идентификатор T – это указатель места заполнения, вместо которого подставляется любой тип.
* Перегрузки обобщенных типов различаются количеством параметров типа, а не их именами. Правильная перегрузка:  `MyClass T { }`, `MyClass T,R { }`

Общие сведения об универсальных шаблонах:

1. Используйте универсальные типы для достижения максимального уровня повторного использования кода, безопасности типа и производительности.
2. Наиболее частым случаем использования универсальных шаблонов является создание классов коллекции.
3. Можно создавать собственные универсальные интерфейсы, классы, методы, события и делегаты.
4. Доступ универсальных классов к методам можно ограничить определенными типами данных

```c#
class MyClass<T>
{
    public T Field { get; set; }

    public void Method()
    {
        Console.WriteLine(Field.GetType());
    }
}

static void Main()
{
    MyClass<int> instance1 = new MyClass<int>();
    instance1.Field = int.MaxValue;
    instance1.Method();

    MyClass<string> instance2 = new MyClass<string>();
    instance2.Field = "Hello World!";
    instance2.Method();

    MyClass<long> instance3 = new MyClass<long>();
    instance3.Field = long.MaxValue;
    instance3.Method();
}
```

## Generic nullable pattern

```c#
static void Main()
{
    Nullable<int> a = 1;

    if (a.HasValue == true)
    {
        Console.WriteLine("a is {0}.", a.Value);
    }

    // Короткая нотация Nullable типа - "?", доступна только для C#.
    int? b = 1;

    if (b.HasValue == true)
    {
        Console.WriteLine("b is {0}", b.Value);
    }

    // Неявно типизированная локальная переменная не может быть - Nullable.
    // var? c = null;        
}
```

# Контрвариантность

[Ковариантность и контрвариантность](https://docs.microsoft.com/ru-Ru/dotnet/standard/generics/covariance-and-contravariance)
[Создание ковариантных и контрвариантных интерфейсов](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/concepts/covariance-contravariance/creating-variant-generic-interfaces)

Контрвариантность - параметр-тип может быть преобразован от класса к классу, производному от него.

В C# контрвариативный тип обозначается ключевым словом in и может находится только во входной позиции, в качестве аргумента.

```c#
// Контрвариантный делегат
delegate void MyDelegate<in T>(T val);

class Figure { public virtual void Method() => Console.WriteLine("figure"); }
class Circle : Figure { public override void Method() => Console.WriteLine("circle"); }
class Square : Figure { public override void Method() => Console.WriteLine("square"); }

// Контрвариантный интерфейс
interface IDrawer<in T>
{
    void DrawFigure();

    void FigureInfo(T val);
}

class Drawer<T> : IDrawer<T>
{
    private T _figure;

    public Drawer(T figure)
    {
        _figure = figure;
    }

    public void DrawFigure()
    {
        Console.WriteLine(_figure);
    }

    public void FigureInfo(T val)
    {
        Console.WriteLine(val);
    }
}
   
static void Main()
{
    Figure figure = new Circle();
    IDrawer<Circle> draw = new Drawer<Figure>(figure);
    draw.DrawFigure();

    MyDelegate<Figure> myFigureDel = new MyDelegate<Figure>(CatUser);
    MyDelegate<Circle> cirecleDel = myFigureDel;
    myFigureDel(new Figure());
    myFigureDel(new Circle());

    // Приммеры из Рихтера
    // Коваринтность
    Func<Object, ArgumentException> fn1 = null; // Явного приведения не требуется
    Func<string, Exception> fn2 = fn1;
    Exception e = fn2(""); 
}

public static void CatUser(Figure figure)
{
    Console.WriteLine(figure.GetType().Name);
}
```

# Ковариантность

[Ковариантность и контрвариантность](https://docs.microsoft.com/ru-Ru/dotnet/standard/generics/covariance-and-contravariance)

Ковариантность - аргумент-тип может быть преобразован от класса к одному из его базовых классов.

В C# ковариантность обозначается  ключевым словом `out` и может находится только в выходной позици, т.е в качестве возвращаемого значения

```c#
delegate T MyDelegate<out T>();   // out - Для возвращаемого значения.

public abstract class Shape { }
public class Circle : Shape { }

public interface IContainer<out T>
{
    T Figure { get; }
}

public class Container<T> : IContainer<T>
{
    private T figure;

    public Container(T figure)
    {
        this.figure = figure;
    }

    public T Figure
    {
        get { return figure; }
    }
}
    
static void Main()
{
    Circle circle = new Circle();
    IContainer<Shape> container = new Container<Circle>(circle);
    Console.WriteLine(container.Figure.ToString());

    MyDelegate<Circle> delegateCircle = new MyDelegate<Circle>(CircleCreator);
    // От производного к базовому.                      
    MyDelegate<Shape> delegateShape = delegateCircle;    

    Shape animal = delegateShape.Invoke();
    Console.WriteLine(animal.GetType().Name);
}

public static Circle CircleCreator()
{
    return new Circle();
}
```

# Ограничения параметров типа

_where T : class  -   Аргумент типа должен иметь ссылочный тип, это также распространяется на тип любого класса, интерфейса, делегата или массива._

```c#
class MyClassA<T> where T : class
{
    public T variable;
}
```

_where T : struct  -  Аргумент типа должен иметь тип значения. Допускается указание любого типа значения, кроме Nullable._

```c#
class MyClassB<T> where T : struct
{
    public T variable;
}
```

_where T : new()  -  Аргумент типа должен иметь открытый конструктор без параметров._
При использовании с другими ограничениями ограничение new() должно устанавливаться последним:

```c#
class MyClass<T> where T : class, new()   { /* ... */ }
class MyClassC<T> where T : new()
{
    public T instance = new T();
}
```

_where T : Base -  Аргумент типа должен являться или быть производным от указанного базового класса._

```c#
class Base { /* ... */ }
class MyClassD<T> where T : Base { /* ... */ }
```

_where T : IInterface - Аргумент типа должен являться или реализовывать указанный интерфейс._

```c#
interface IInterface { /* ... */ }
class Derived : IInterface { /* ... */ }
class MyClassD<T> where T : IInterface { /* ... */ }
```

_Можно установить несколько ограничений интерфейса. Ограничивающий интерфейс также может быть универсальным._

```c#
class MyClassE<T> where T : IInterface, IInterface<object> { /* ... */ }
```

_Аргумент типа, предоставляемый в качестве T, должен совпадать с аргументом, предоставляемым в качестве U, или быть производным от него._
Это называется ограничением типа "Naked".

```c#
class MyClassF<T, R, U> where T : U { /* ... */ }
```
