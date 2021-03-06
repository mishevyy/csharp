# Классы

_Класс - основная конструкция ООП._

Класс - это набор данных и методов имеющих общую, целостную, хорошо определенную сферу ответственности. Класс хранит в себе состояние (поля и свойства) и поведение (методы, события). Классы нужны для описания объекта таким образом, каким бы мы могли описать его в реальном мире.
Реализация объекта определяется классом. Класс специфицирует внутренние данные объекта и его представление, а так же операции, которые объект может выполнять.

## Поля и свойства

```c#
class MyClass
{    
    // Поля, рекомендуется делать приватным и использовать свойства для управления доступом к полю
    private int id; 

    // Свойства это методы, для упарвления доступом к полям объекта
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    private string name;
    public string Name // В свойствах можно настравивать логику
    {
        get { return name; }
        set
        {
            if (string.IsNullOrEmpty(value))
                Console.WriteLine("Поле не может быть пустым, пожалуйста введите значение");

            name = value;
        }
    }
}
```

## Автоматически реализуемые свойства (Auto-Implemented properties)

Авто свойства это более лаконичная форма свойств, их рекомендуется использовать, когда в методах доступа (get и set) не требуется дополнительная логика. При создании автоматически реализуемых свойств, компилятор создаст закрытое, анонимное резервное поле, которое будет доступно с помощью методов get и set свойства.

```c#
class MyClass
{
    // Возможно использовать с модификаторами доступа
    public int Id { get; private set; }    
    public string Name { get; set; } 
}
```

## Поля только для чтения (readonly)

Поля с таким модификатором можно установить значение только при инициализации или в конструкторе. Далее поле будет недоступно для модификаций. Рекомендуется использовать `readonly` поля внутри класса за место констант.

```c#
class MyClass
{ 
    public readonly string description = "Это readonly поле";
}
```

## Конструкторы класса

Конструктор класса - специальный метод, который вызывается при инициализации экземпляра класса.

Существует два типа конструкторов: Конструктор по умолчанию и Пользовательский конструктор. Конструктор по умолчанию нужен для инициализации полей значениями по умолчанию. Пользовательский конструктор нужен для инициализации полей предопределенными пользователем значениями.

Если в классе не определен пользовательский конструктор, то конструктор по умолчанию можно не объявлять, но если в классе определен пользовательский конструктор, и нужно вызвать конструктор по умолчанию, то конструктор по умолчанию, нужно инициализировать явно.

```c#
class MyClass
{    
    private string name;
    private int age;
    private string education;

    // Конструктор по умолчанию (присутствует в классе не явно)       
    public Person()
    {
        Console.WriteLine("Привет, я конструктор по умолчанию!");
    }

    // Пользовательский конструктор
    public Person(string name)
    {
        this.name = name;
    }

    // Пользовательский конструктор может вызвать другой конструктор (this)
    public Person(string name, int age, string education)
        : this(name)
    {
        this.Age = age;
        this.Education = education;
    }
}
```

## Ключевое слово `new` для ссылочных типов

CLR требует, что бы все объекты создавались, через ключевое слов `new` оператор new выполняет следующие действия:

1. Вычисление количества байтов, необходимых для хранения всех его экземплярных полей и всех его базовых типов включая Object
2. Создаёт указатель на объект и индекс блока синхронизации, они необходимы CLR для управления объектом
3. Выделяет память для хранения объекта в управляемой куче
4. Инкапсулирует указатель на объект и индекс блоки синхронизации
5. Вызывает конструктор экземпляра с параметрами указанными при вызове
6. Возвращает ссылку на созданный экземпляр

## Экземпляр и объект

При первом создании экземпляра класса в управляемой куче создается объект и экземпляр в объекте хранятся все его методы, статические члены и константы, в экземпляре не статические поля.
Класс описывает объект, а при инициализации мы создаем экземпляр этого объекта (класса)

## Создание экземпляра класса

* `MyClass person = new();` Новый синтаксис инициализации доступный с версии net 5.0
* `var person = new MyClass();` Инициализация с ключевым словом var, является синтаксическим сахаром, можно использовать если тип справа известен

```c#
MyClass person1 = new MyClass(name: "Михаил")
{
    Id = 1,
    Name = "Михаил",
    Age = 22    
};
```

## Методы задания только инициализации

**Методы задания только для инициализации** обеспечивают единообразный синтаксис для инициализации членов объекта. Инициализаторы свойств позволяют ясно понять, какое значение задает то или иное свойство. Недостаток заключается в том, что эти свойства должны быть устанавливаемыми. Начиная с C# 9.0, для свойств и индексаторов можно создавать методы доступа `init`, а не методы доступа `set`.

```c#
public struct WeatherObservation
{
    public DateTime RecordedAt { get; init; }
    public decimal TemperatureInCelsius { get; init; }
    public decimal PressureInMillibars { get; init; }

    public override string ToString() =>
        $"At {RecordedAt:h:mm tt} on {RecordedAt:M/d/yyyy}: " +
        $"Temp = {TemperatureInCelsius}, with {PressureInMillibars} pressure";
}
```

## Функции подбора и завершения

Многие другие функции позволяют более эффективно писать код. В C# 9.0 можно опустить тип в [выражении `new`](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/new-operator), если тип созданного объекта уже известен. Наиболее часто это используется в объявлениях полей.

```c#
private List<WeatherObservation> _observations = new();

var forecast = station.ForecastFor(DateTime.Now.AddDays(2), new());

WeatherStation station = new() { Location = "Seattle, WA" };
```

## Классы индексаторы

Класс индексатор позволяет работать с объектом, как с массивом [Классы индексаторы](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/indexers/using-indexers)
Индексаторы позволяют индексировать экземпляры класса или структуры так же, как массивы. Индексаторы напоминают свойства, но их методы доступа принимают параметры.

```c#
class MyIndexer
{
    private int[] t_stuff;
    public int Length { get => t_stuff.Length; }

    public int this[int i]
    {
        get
        {
            if(i > Length)
                throw new IndexOutOfRangeException();
            
            return t_stuff[i];
        }
        set
        {
            if (i > Length)
                throw new IndexOutOfRangeException();

            t_stuff[i] = value;
        }
    }

    public MyIndexer()
    {
        t_stuff = new int[10];

        for (int i = 0; i < Length; i++)
        {
            t_stuff[i] = i * 2;
        }
    }
}  
```

```c#
MyIndexer myIndexer = new MyIndexer();
for (int i = 0; i < myIndexer.Length; i++)
{
    Console.WriteLine(myIndexer[i]);
}
```
