# Новые возможности C# 9.0

## Типы записей

В C# 9.0 появились ***типы записей***. Вы можете использовать ключевое слово `record` для определения ссылочного типа, который предоставляет встроенные возможности для инкапсуляции данных. Вы можете создавать типы записей с неизменяемыми свойствами, используя позиционные параметры или стандартный синтаксис свойств:

```c#
public record Person(string FirstName, string LastName);
public record TestDelegateRecord<T>(int count, Action<T> action);

public record Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

Несмотря на поддержку изменения, записи предназначены в первую очередь для неизменяемых моделей данных. Тип записи предоставляет следующие возможности:

- [Краткий синтаксис для создания ссылочного типа с неизменяемыми свойствами.](https://docs.microsoft.com/ru-ru/dotnet/csharp/whats-new/csharp-9#positional-syntax-for-property-definition)
- Поведение, полезное для ссылочного типа, ориентированного на данные:
  - [Равенство значений](https://docs.microsoft.com/ru-ru/dotnet/csharp/whats-new/csharp-9#value-equality)
  - [Краткий синтаксис для обратимого изменения.](https://docs.microsoft.com/ru-ru/dotnet/csharp/whats-new/csharp-9#nondestructive-mutation)
  - [Встроенное форматирование для отображения.](https://docs.microsoft.com/ru-ru/dotnet/csharp/whats-new/csharp-9#built-in-formatting-for-display)
- [Поддержка иерархий наследования.](https://docs.microsoft.com/ru-ru/dotnet/csharp/whats-new/csharp-9#inheritance)

На основе [типов структур](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/struct) можно создать типы, ориентированные на данные, которые поддерживают равенство значений и почти не определяют поведение. Но в сравнительно больших моделях данных типы структур имеют определенные недостатки:

- Они не поддерживают наследование.
- Они менее эффективны при определении равенства значений. Для типов значений метод [ValueType.Equals](https://docs.microsoft.com/ru-ru/dotnet/api/system.valuetype.equals) использует отражение для поиска всех полей. Для записей компилятор создает метод `Equals`. На практике реализация равенства значений в записях работает заметно быстрее.
- В некоторых сценариях они используют больше памяти, так как каждый экземпляр содержит полную копию всех данных. Типы записей являются [ссылочными типами](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/reference-types), то есть каждый экземпляр записи содержит только ссылку на данные.

### Позиционный синтаксис для определения свойств

Позиционные параметры позволяют объявить свойства записи и инициализировать значения свойств при создании экземпляра:

```c#
public record Person(string FirstName, string LastName);

public static void Main()
{
    Person person = new("Nancy", "Davolio");
    Console.WriteLine(person);
    // output: Person { FirstName = Nancy, LastName = Davolio }
}
```

### Неизменяемость

Тип записи не обязательно является неизменяемым. Вы можете объявить свойства с методами доступа `set` и полями без атрибута `readonly`. Но несмотря на поддержку изменения, записи лучше всего подходят для создания неизменяемых моделей данных. Свойства, создаваемые с использованием позиционного синтаксиса, являются неизменяемыми.

### Равенство значений

Равенство значений означает, что две переменные типа записи считаются равными, если совпадают типы и значения всех свойств и полей.

```c#
public record Person(string FirstName, string LastName, string[] PhoneNumbers);

public static void Main()
{
    var phoneNumbers = new string[2];
    Person person1 = new("Nancy", "Davolio", phoneNumbers);
    Person person2 = new("Nancy", "Davolio", phoneNumbers);
    Console.WriteLine(person1 == person2); // output: True

    person1.PhoneNumbers[0] = "555-1234";
    Console.WriteLine(person1 == person2); // output: True

    Console.WriteLine(ReferenceEquals(person1, person2)); // output: False
}
```

### Обратимое изменение

Если нужно изменить неизменяемые свойства экземпляра записи, вы можете с помощью выражения `with` выполнить *обратимое изменение*. Выражение `with` создает новый экземпляр записи, который является копией существующего экземпляра записи, и изменяет в этой копии указанные свойства и поля. Для указания требуемых изменений используется синтаксис [инициализатора объектов](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers), как показано в следующем примере:

```c#
public record Person(string FirstName, string LastName)
{
    public string[] PhoneNumbers { get; init; }
}

public static void Main()
{
    Person person1 = new("Nancy", "Davolio") { PhoneNumbers = new string[1] };
    Console.WriteLine(person1);
    // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }

    Person person2 = person1 with { FirstName = "John" };
    Console.WriteLine(person2);
    // output: Person { FirstName = John, LastName = Davolio, PhoneNumbers = System.String[] }
    Console.WriteLine(person1 == person2); // output: False

    person2 = person1 with { PhoneNumbers = new string[1] };
    Console.WriteLine(person2);
    // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }
    Console.WriteLine(person1 == person2); // output: False

    person2 = person1 with { };
    Console.WriteLine(person1 == person2); // output: True
}
```

### Встроенное форматирование для отображения

ипы записей имеют создаваемый компилятором метод [ToString](https://docs.microsoft.com/ru-ru/dotnet/api/system.object.tostring), который отображает имена и значения открытых свойств и полей. Метод `ToString` возвращает строку в следующем формате: 
`<record type name> { <property name> = <value>, <property name> = <value>, ...}`

### Наследование

Запись может наследовать от другой записи.

```c#
public abstract record Person(string FirstName, string LastName);
public record Teacher(string FirstName, string LastName, int Grade)
    : Person(FirstName, LastName);
public static void Main()
{
    Person teacher = new Teacher("Nancy", "Davolio", 3);
    Console.WriteLine(teacher);
    // output: Teacher { FirstName = Nancy, LastName = Davolio, Grade = 3 }
}
```

## Методы задания только инициализации

***Методы задания только для инициализации*** обеспечивают единообразный синтаксис для инициализации членов объекта. Инициализаторы свойств позволяют ясно понять, какое значение задает то или иное свойство. Недостаток заключается в том, что эти свойства должны быть устанавливаемыми. Начиная с C# 9.0, для свойств и индексаторов можно создавать методы доступа `init`, а не методы доступа `set`.

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

## Улучшения сопоставления шаблонов

C# 9 включает новые улучшения сопоставления шаблонов.

- ***Шаблоны типов*** проверяют соответствие переменной определенному типу.
- ***Шаблоны в круглых скобках*** усиливают или подчеркивают приоритет сочетаний шаблонов.
- ***В шаблонах конъюнкций `and`*** требуется соответствие обоих шаблонов.
- ***В шаблонах дизъюнкций `or`*** требуется соответствие хотя бы одного из шаблонов.
- В ***шаблонах `not` с отрицанием*** требуется несоответствие данного шаблона.
- ***В шаблонах сравнения*** требуется, чтобы входные данные были меньше, больше, меньше или равны, больше или равны данной константе.

Эти шаблоны обогащают синтаксис шаблонов. Рассмотрим следующие примеры.

```c#
public static bool IsLetter(this char c) =>
    c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

public static bool IsLetterOrSeparator(this char c) =>
    c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';
```

## Функции подбора и завершения

Многие другие функции позволяют более эффективно писать код. В C# 9.0 можно опустить тип в [выражении `new`](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/new-operator), если тип созданного объекта уже известен. Наиболее часто это используется в объявлениях полей.

```c#
private List<WeatherObservation> _observations = new();

var forecast = station.ForecastFor(DateTime.Now.AddDays(2), new());

WeatherStation station = new() { Location = "Seattle, WA" };
```
