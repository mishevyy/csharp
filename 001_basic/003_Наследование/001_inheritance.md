# Наследование

Наследование механизм ООП позволяющий описать новый класс на основе уже существующего при этом функциональность базового класса заимствуется новым производным классом.

Для того что-бы наследоваться от класса нужно поставит `:` и написать имя класса от которого хотим наследоваться. У производного класса уровень доступа должен быть таким же как у класса родителя или более строгий.

```c#
class BaseClass {}

class DerivedClass : BaseClass{}
```

_Ключевое слово `sealed` - запрещает наследование от класса_

```c#
sealed class MyClass { }
```

## Виртуальные члены и их переопределение

Ключевые слова:

* `virtual` - поля и методы помеченные этим словом возможно переопределить в классе наследнике
* `override` - переопределяет члены базового класса, для этого они должны быть помечены как virtual
* `sealed` - запрещает дальнейшее переопределение члена, для этого они должны быть помечены как virtual
* `new` - замещает реализацию из класса наследника

## Override и New

Если член переопределен в наследнике, то его реализация будет использовать в случае создания класса наследника и в случае когда наследник приводится к базовому классу. Замещенные методы принадлежат только конкретному классу.

```c#
class BaseClass
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Конструктор по умолчанию обязателен при наследовании от базоваого класса, 
    // если есть пользовательский конструктор,
    // и в наследуемом классе необходимо использовать конструктор по умолчанию.
    public BaseClass()
    {
        Console.WriteLine("BaseClass .ctor");
    }

    public BaseClass(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public virtual void Method1()
    {
        Console.WriteLine("Method1 from BaseClass");
    }

    public virtual void Method2()
    {
        Console.WriteLine("Method2 from BaseClass");
    }

    public virtual void Method3()
    {
        Console.WriteLine("Method3 from BaseClass");
    }

    public virtual void Method4()
    {
        Console.WriteLine("Method4 from BaseClass");
    }
}
```

```c#
class DerivedClass : BaseClass
{
    public string DerivedProprty { get; set; }

    // Пользовательский конструктор.
    // При создании объекта производного класса, конструктор производного класса 
    // автоматически вызывает конструктор по умолчанию из базового класса.
    // Конструктор базового класса, присвоит всем данным какие-то свои безопасные значения.
    // После этого начнет работу конструктор производного класса, который повторно
    // будет определять значения для унаследованых членов. (ДВОЙНАЯ РАБОТА)!

    // Для избежания этого, нужно вызывать конструктор из базового класса, 
    // с помощбю ключевого слова base
    // Вызывается пользовательский конструктор базового класса, при этом не нужно, 
    // присваивать значения, унаследованным членам в конструкторе производного класса.
    public DerivedClass(string derivedProp, string name, int age) 
        : base(name, age)
    {
        this.DerivedProprty = derivedProp;
    }

    // override - переопределяет метод базового класса, для этого
    // метод в базовом классе должен быть определен как virtual
    public override void Method1()
    {
        Console.WriteLine("Method1 from DerivedClass");
    }

    // base - вызывает реализацию метода из базового класса
    public override void Method2()
    {
        base.Method2();
    }

    // Ключевое слово new - замещает метод из базового класса
    // таким образом, что метод в класс наследнике не имеет ничего общего с класом родителем
    public new void Method3()
    {
        Console.WriteLine("Method3 from DerivedClass");
    }

    // Ключевое слово sealed запрещает дальнейшее переопределение метода
    public override sealed void Method4()
    {
        Console.WriteLine("Method4 from DerivedClass");
    }
}
```
## Приведение типов Casting и UpCasting

CLR всегда знает какого типа объект. Метод GetType всегда может сказать какого типа объект.
Приведение к базовому типу является безопасным и может происходить неявно. Для приведения к производному типу, нужно произвести явное приведение.

Приведение к базовому типу называется Upcast  и всегда безопасно.

Приведение к производному типу называется Downcast.

Downcast невозможен без предварительного Upcasta так как наследование имеет отношение `является`, то есть производный класс содержит в себе базовый класс и знает о нем, в свою очередь базовый класс ничего не знает о классе который наследовался от него.
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

Паттерн матчинг (выражение is)

```c#
DerrivedClass dc3 = new DerrivedClass();
if (dc3 is BaseClass bc3)
{
    bc3.Method2();
}
```
