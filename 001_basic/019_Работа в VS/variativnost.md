# Вариативность

[Ковариантность и контрвариантность](https://docs.microsoft.com/ru-Ru/dotnet/standard/generics/covariance-and-contravariance)

Ковариантность - тип может быть преобразован от к своему базовому типу (UpCas).
Контрвариантность - базовый тип может быть преобразован к одному из своих производных типов (DownCast).

Все объекты в c# являются ковариантными, то есть могут быть безопасно приведены к своему базовому классу, и не поддерживают контрвариантность.
Контрвариантность возможна только в параметризированных делегатах и интерфейсах

```c#
public class Cap
{
    public string CapName { get; private set; }
    public Cap(string capName)
    {
        CapName = capName;
    }
    public void TakeCap()
    {
        Console.WriteLine($"Взять {CapName} кружку");
    }   
}

public class CapOfCoffee : Cap
{
    public CapOfCoffee(string capName)
        : base(capName) { }

    public void Drink()
    {
        Console.WriteLine("Выпить кофе");
    }
}
```

## Ковариантность массивов

Массивы поддерживают ковариантность, то есть массив элементов производного типа, можно привести к массиву элементов базового типа.

```c#
void CovariantArray()
{
    // Ковариантность - тип может быть приведен к одному из его базовых типов
    // Массивы поддерживают ковариантность

    CapOfCoffee[] capsOfCoffe =
    {
        new CapOfCoffee("Red Cap"),
        new CapOfCoffee("Yellow Cap"),
    };

    // Тип CapOfCoffee приводится к базовому типу Cap
    // во время перебора элементов
    foreach (Cap item in capsOfCoffe)
    {
        item.TakeCap();
    }
}
```

## Ковариантность и контрвариантность в делегатах

### Ковариантность

Ковариантность в делегатах, делегат может возвращать тип производный от типа, который указан в возвращаемом типе делегата.
Ковариантность поддерживается только в возвращаемых параметрах типа, то есть в параметрах которые обазначены ключевым словом out
`delegate Func<out T>()` - одна из перегрузок делегата Func

``` c#
void CovariantDelegate()
{
    // Ковариантность в делегатах, делегат может возвращать тип
    // производный от типа, который указан в возвращаемом типе делегата.
    // Ковариантность поддерживается только в возвращаемых параметрах типа, то есть в параметрах
    // которые обазначены ключевым словом out
    // delegate Func<out T>() - одна из перегрузок делегата Func

    Func<Cap> func = new Func<CapOfCoffee>(VerboseCovCap);
    Cap cap = func();

    if(cap is CapOfCoffee ofCoffee)
    {
        ofCoffee.Drink();
    }
    
}

static CapOfCoffee VerboseCovCap()
{
    return new CapOfCoffee("Cap Of Coffe");
}
```

### Контрвариантность

Контрвариантность в делегатах, делегат может принимать параметр типа который являеться базовым для типа параметра.
Контрвариантность поддерживается только во входящих параметрах, то есть в параметрах которые обозначены ключевым словом in
`delegate Action<in T>` - одна из перегрузок делегата Action

```c#
void ContrvariantDelegate()
{
    // Контрвариантность в делегатах, делегат может принимать параметр типа
    // который являеться базовым для типа параметра.
    // Контрвариантность поддерживается только во входящих параметрах, то есть в параметрах
    // которые обозначены ключевым словом in
    // delegate Action<in T> - одна из перегрузок делегата Action

    Action<CapOfCoffee> action = new Action<Cap>(VerboseCap);
    action(new CapOfCoffee("Red Cap"));
}

static void VerboseCap(Cap cap)
{
    cap.TakeCap();
}

```

## Ковариантность и контрвариантность в интерфейсах

### Контрвариантность интерфейсов

```c#
// Контрвариантный интерфейс
interface ICap<in T>
{
    void VerboseCap(T t);
}

class CapTest<T> : ICap<T>
{
    public void VerboseCap(T t)
    {
        throw new NotImplementedException();
    }
}
   
void ContrvariantInterface()
{
    ICap<CapOfCoffee> cap = new CapTest<Cap>();
    cap.VerboseCap(new CapOfCoffee(""));
}
```

## Ковариантность интерфейсов

```c#

// Ковариантный интерфейс
interface IContainer<out T>
{
    T Figure { get; }
}

class CapOfTest<T> : ICapOf<T>
{
    public T VerboseCap()
    {
        throw new NotImplementedException();
    }
}

void CovariantInterface()
{
    ICapOf<Cap> capOf = new CapOfTest<CapOfCoffee>();
    Cap cap = capOf.VerboseCap();
}
```
