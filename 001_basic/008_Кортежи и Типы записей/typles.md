# КОРТЕЖИ

[Кортежи](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/value-tuples) — это упрощенные структуры данных, содержащие несколько полей для представления элементов данных. Поля не проверяются, и собственные методы определять нельзя. Типы кортежей в C# поддерживают `==` и `!=`.

Кортежи вместо ссылочных типов имеют новый тип ValueTyple, сберегая значительный объем памяти.

Тип ValueTyple создает разные структуры на основе колличества свойств для кортежа.

_Инициализация кортежа._

```c#
(string, int, string) values = ("a", 5, "c");
var values = ("a", 5, "c");
(int a, int b, bool bb) c3 = (1, 2, true);
```

_Обращение к членам кортежа._

```c#
Console.WriteLine($"{values.Item1} {values.Item2} {values.Item3}");
```

_Именованная инициализация кортежа._

```c#
var vNamed = (FirstItem: "", IntItem: 4);
// Теперь обращаться к членам кортежа можно так:
Console.WriteLine($"{vNamed.FirstItem} {vNamed.IntItem}");
```

_Использование метода._

```c#
var vTuple = SplitCortezh();
Console.WriteLine($"{vTuple.first} {vTuple.middle}");
```

_Применение техники отбрасывания._

```c#
var (first, _) = SplitCortezh();
Console.WriteLine(first);
```

_Метод возвращающий тип кортеж._

```c#
static (string first, string middle) SplitCortezh()
{
    return ("Hello", "World");
}
```

## Деконструирование объекта в кортеж

```c#
class Person
{
    public string FirstName { get; set; }
    public string Name { get; set; }
    public string City { get; set; }

    public Person(string fName, string name, string city)
    {
        FirstName = fName;
        Name = name;
        City = city;
    }

    // Метод деконструктор кортежа
    public void Deconstruct(out string firstName, out string name, out string city)
    {
        firstName = FirstName;
        name = Name;
        city = City;
    }
}
```

 _Деконструирование кортеж возможна благодаря методу Deconstruct в классе Person._

```c#
Person person = new Person("Adam", "Jhon", "Boston");
var (fName, name, city) = person; 
```
