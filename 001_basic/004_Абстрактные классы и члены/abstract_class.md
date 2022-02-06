# Абстрактные классы

Абстрактный класс в ООП - это базовый класс, который не предпалагает создание экземпляра.
Ключевое слово abstract может использоваться с классами, методами, свойствами, индексаторами и событиями

Спецификация абстрактных классов:

1. Нельзя создать экземпляр абстрактного класса
2. Абстрактные классы могут содержать как абстрактные члены так и не абстрактные
3. Неабстрактный класс, реализующий абстрактный класс, должен иметь фактическую реализацию всех абстрактных членов
4. Абстрактный класс реализующий интерфейс, может отображать методы интерфейса в абстрактных методах
5. Абстрактный класс должен предоставлять реализацию для всех членов интерфейса

Спецификация абстрактных методоов:

1. Абстрактный метод является неявно виртуальным методом
2. Создание абстрактных членов допускается только в абстрактном классе
3. Тело абстрактного метода отсутсвует

Преимущества абстрактных классов:

1. Общий код в одной реализации в виде конкретных и абстрактных членов
2. Изменение значений полей или неабстрактных членов, приводит к соответствующему изменению во всех производных классах
3. Наличие реализации по умолчанию

```c#
abstract class MyAbstraction
{
    // Конструктор абстрактного класса всегда будет выполнятся первым
    public MyAbstraction()
    {
        Console.WriteLine("Hello, from abstract .ctor");
    }

    public abstract void AbstractMethod();
}

class MyClass : MyAbstraction
{
    public MyClass()
    {
        Console.WriteLine("Hello, from myclass .ctor");
    }

    public override void AbstractMethod()
    {
        Console.WriteLine("Implement abstract method");
    }
}
 
static void Main()
{
    MyAbstraction ma = new MyClass();
    ma.AbstractMethod();
    // Hello, from abstract .ctor
    // Hello, from myclass .ctor
    // Implement abstract method  

    MyClass mc = new MyClass();
    mc.AbstractMethod();
    // Hello, from abstract .ctor
    // Hello, from myclass .ctor
    // Implement abstract method 

    MyAbstraction m2 = mc;
    m2.AbstractMethod();
    // Implement abstract method
}
```
