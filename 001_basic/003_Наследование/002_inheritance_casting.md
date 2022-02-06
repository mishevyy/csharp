# Приведение типов Casting и UpCasting

CLR всегда знает какого типа объект. Метод GetType всегда может сказать какого типа объект.
Приведение к базовому типу является безопасным и может происходить неявно. Для приведения к производному типу, нужно произвести явное приведение.

Приведение к базовому типу называется Upcast  и всегда безопасно.

Приведение к производному типу называется Downcast.

Downcast невозможен без предварительного Upcasta так как наследование имеет отношение является, то есть производный класс содержит в себе базовый класс, в свою очередь базовый класс ничего не знает о классе который наследовался от него.
От одного класса может быть наследовано несколько разных классов, и они все будут являться этим базовым классом, в свою очередь базовый класс не будет являться ни одним из этих классов пока один из классов не будет приведен к базовому классу

```c#
class BaseClass
{
    public virtual void Method1()
    {
        Console.WriteLine("Method1 from BaseClass");
    }

    public virtual void Method2()
    {
        Console.WriteLine("Method2 from BaseClass");
    }
}

class DerrivedClass : BaseClass
{
    // переопределение метода базового класса
    public override void Method1()
    {
        Console.WriteLine("Method1 from DerivedClass");
    }

    // замещение метода базового класса
    public new void Method2()
    {
        Console.WriteLine("Method2 from DerivedClass");
    }
}
```

```c#
DerrivedClass instance = new DerrivedClass();
instance.Method1();

// UpCast (приведение к базовому типу)
BaseClass instanceUp = instance;
instanceUp.Method1();

// DownCast (приведение к производному типу. Невозможен без предварительного UpCast)    
DerrivedClass instanceDown = (DerrivedClass)instanceUp;
instanceDown.Method1();
```

## Кастинг с помощью операторов is и as

Оператор is - проверяет совместимость объекта с заданным типом. 
Если предоставленный объект может быть приведен к предоставленному типу не вызывая исключение, выражение is принимает значение true.

```c#
BaseClass bs = new BaseClass();
DerrivedClass dc = new DerrivedClass();

if (bs is DerrivedClass)
{
    // DownCast
    dc = (DerrivedClass)bs;
}
```

Оператор as используется для выполнения преобразований между совместимыми ссылочными типами.
Оператор as подобен оператору приведения. Однако, если преобразование невозможно, as возвращает значение null, а не вызывает исключение.

```c#
dc = bs as DerrivedClass; // DownCast
bs = dc as BaseClass; // UpCast
```

Паттерн is матчинг

```c#
DerrivedClass dc3 = new DerrivedClass();
if (dc3 is BaseClass bc3)
{
    bc3.Method2();
}
```
