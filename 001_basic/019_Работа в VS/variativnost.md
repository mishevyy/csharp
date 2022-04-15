# Вариативность

## Контрвариантность

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

## Ковариантность

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


## Ковариантность и контрвариантность в делегатах

```c#
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public override string ToString()
    {
        return string.Format($"Name: {Name}, Age: {Age}");
    }
}

class Employee : Person
{
    public string Profession { get; set; }
    public override string ToString()
    {
        return string.Format($"Name: {Name}, Age: {Age}, Profession: {Profession}");
    }
}
   
static void Main()
{
    // Контрвариантность означает, что метод может принимать параметр,
    // который является базовым для типа параметра
    Action<Employee> a = new Action<Person>(MethodPerson);

    // Ковариантность означает, что метод может возвратить тип,
    // производный от типа возвращаемого делегата
    Func<Person> f = new Func<Employee>(MethodEmpployee);
}

static void MethodPerson(Person p) { }
static Employee MethodEmpployee() { return new Employee(); }
```