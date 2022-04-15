# Перегрузка операторов

Синтаксис перегрузки для арифметических операторов `public static T operator вид оператора(T t1, T t2){}`

Синтаксис перегрузки для операторов сравнения `public static bool operator вид оператора(T t1, T t2){}`

Перегружаемые операторы:

+, -, /, *

*>, <, >=, <=, ==, !=, ++, --* эти операторы обязательно перегружать попарно.

## Перегрузка операторов для класса

```c#
class Point : IComparable<Point>
{
    private int x, y;
    public int X { get => x; }
    public int Y { get => y; }

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
```

### Перегрузка простых операторов

```c#
public static Point operator +(Point p1, Point p2)
{
    return new Point(p1.X + p2.X, p1.Y + p2.Y);
}

public static Point operator -(Point p1, Point p2)
{
    return new Point(p1.X - p2.X, p1.Y - p2.Y);
}

public static Point operator /(Point p1, Point p2)
{
    return new Point(p1.X / p2.X, p1.Y / p2.Y);
}

public static Point operator *(Point p1, Point p2)
{
    return new Point(p1.X * p2.X, p1.Y / p2.Y);
}
```

### Перегрузка операторов инкремента и декремента

```c#
public static Point operator ++(Point p)
{
    return new Point(p.X + 1, p.Y + 1);
}
public static Point operator --(Point p)
{
    return new Point(p.X - 1, p.Y - 1);
}
```

### Перегрузка операторов сравнения (всегда перегружаются попарно)

```c#
public static bool operator >(Point p1, Point p2)
{
    return p1.CompareTo(p2) > 0;
}
public static bool operator <(Point p1, Point p2)
{
    return p1.CompareTo(p2) < 0;
}

public static bool operator >=(Point p1, Point p2)
{
    return p1.CompareTo(p2) >= 0;
}
public static bool operator <=(Point p1, Point p2)
{
    return p1.CompareTo(p2) <= 0;
}

public static bool operator ==(Point p1, Point p2)
{
    return p1.Equals(p2);
}
public static bool operator !=(Point p1, Point p2)
{
    return p1.Equals(p2);
}
```

### Для работы операторов сравнения реализуют интерфейс IComparable

```c#
public int CompareTo(Point p)
{
    if(this.x > p.X && this.Y > p.Y)
    {
        return 1;
    }
    else if(this.x < p.X && this.y > p.Y)
    {
        return -1;
    }
    else
    {
        return 0;
    }
}

public override bool Equals(object obj)
{
    if (obj is Point point)
    {
        if (point.X == this.x && point.Y == this.y)
            return true;
    }
    return false;
}

public override int GetHashCode()
{
    return this.x ^ this.y;
}
```
