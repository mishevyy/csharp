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
