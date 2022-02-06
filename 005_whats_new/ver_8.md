# Новые возможности C# 8.0

## Методы интерфейса по умолчанию

Теперь вы можете добавлять члены в интерфейсы и предоставлять реализацию для этих членов. Эта возможность языка позволяет разработчикам API добавлять методы в интерфейс в более поздних версиях, не нарушая исходный код или совместимость на уровне двоичного кода с существующими реализациями этого интерфейса.



## Дополнительные шаблоны в нескольких расположениях

### Выражения switch

Часто инструкция [`switch`](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement) возвращает значение в каждом из блоков `case`. **Выражения switch** позволяет использовать более краткий синтаксис выражения

- Переменная расположена перед ключевым словом `switch`. Другой порядок позволяет визуально легко отличить выражение switch от инструкции switch.
- Элементы `case` и `:` заменяются на `=>`. Это более лаконично и интуитивно понятно.
- Случай `default` заменяется пустой переменной `_`.
- Тексты являются выражениями, а не инструкциями.

```c#
string FizzBuzz(int x) =>
(x % 3 == 0, x % 5 == 0) switch
{
        (true, true) => "FizzBizz",
        (true, _) => "Fizz",
        (_, true) => "Bizz",
        _ => x.ToString()
};

Enumerable.Range(1, 100)
    .Select(FizzBuzz).ToList()
    .ForEach(Console.WriteLine);
```



### Шаблоны свойств

**Шаблон свойств** позволяет сопоставлять свойства исследуемого объекта. 

```c#
public static decimal ComputeSalesTax(Address location, decimal salePrice) =>
    location switch
    {
        { State: "WA" } => salePrice * 0.06M,
        { State: "MN" } => salePrice * 0.075M,
        { State: "MI" } => salePrice * 0.05M,
        // other cases removed for brevity...
        _ => 0M
    };
```



### Шаблоны кортежей

Некоторые алгоритмы зависят от нескольких наборов входных данных. **Шаблоны кортежей** позволяют переключаться между несколькими значениями, выраженными как [кортежи](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/value-tuples). 

```c#
public static string RockPaperScissors(string first, string second)
    => (first, second) switch
    {
        ("rock", "paper") => "rock is covered by paper. Paper wins.",
        ("rock", "scissors") => "rock breaks scissors. Rock wins.",
        ("paper", "rock") => "paper covers rock. Paper wins.",
        ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
        ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
        ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
        (_, _) => "tie"
    };
```



## Позиционные шаблоны

Некоторые типы включают метод `Deconstruct`, свойства которого деконструируются на дискретные переменные. Если метод `Deconstruct` доступен, можно использовать **позиционные шаблоны** для проверки свойств объекта и использовать эти свойства для шаблона.

```c#
public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) => (X, Y) = (x, y);

    public void Deconstruct(out int x, out int y) =>
        (x, y) = (X, Y);
}
```

В следующем методе используется **позиционный шаблон** для извлечения значений `x` и `y`. Затем используется предложение `when` для определения `Quadrant` точки:

```c#
static Quadrant GetQuadrant(Point point) => point switch
{
    (0, 0) => Quadrant.Origin,
    var (x, y) when x > 0 && y > 0 => Quadrant.One,
    var (x, y) when x < 0 && y > 0 => Quadrant.Two,
    var (x, y) when x < 0 && y < 0 => Quadrant.Three,
    var (x, y) when x > 0 && y < 0 => Quadrant.Four,
    var (_, _) => Quadrant.OnBorder,
    _ => Quadrant.Unknown
};
```



## Объявления using

Теперь блок using необязательно оборачивать в дополнительные скобки

```c#
static void Foo()
{
    using var file = new System.IO.StreamWriter("WriteLines2.txt");    
    // ...
}
```



## Асинхронные потоки

В методе, который возвращает асинхронный поток, есть три свойства:

1. Он объявлен с помощью модификатора `async`.
2. Он возвращает интерфейс [IAsyncEnumerable](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.iasyncenumerable-1).
3. Метод содержит инструкции `yield return` для возвращения последовательных элементов в асинхронном потоке.

Для использования асинхронного потока требуется добавить ключевое слово `await` перед ключевым словом `foreach` при перечислении элементов потока. Для добавления ключевого слова `await` требуется, чтобы метод, который перечисляет асинхронный поток, был объявлен с помощью модификатора `async` и возвращал тип, допустимый для метода `async`. Обычно это означает возвращение структуры [Task](https://docs.microsoft.com/ru-ru/dotnet/api/system.threading.tasks.task) или [Task](https://docs.microsoft.com/ru-ru/dotnet/api/system.threading.tasks.task-1). Это также может быть структура [ValueTask](https://docs.microsoft.com/ru-ru/dotnet/api/system.threading.tasks.valuetask) или [ValueTask](https://docs.microsoft.com/ru-ru/dotnet/api/system.threading.tasks.valuetask-1). Метод может использовать и создавать асинхронный поток. Это означает, что будет возвращен интерфейс [IAsyncEnumerable](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.iasyncenumerable-1). 

```c#
public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
{
    for (int i = 0; i < 20; i++)
    {
        await Task.Delay(100);
        yield return i;
    }
}

await foreach (var number in GenerateSequence())
{
    Console.WriteLine(number);
}
```



## Индексы и диапазоны

Диапазоны и индексы обеспечивают лаконичный синтаксис для доступа к отдельным элементам или диапазонам в последовательности.

Поддержка языков опирается на два новых типа и два новых оператора:

- [System.Index](https://docs.microsoft.com/ru-ru/dotnet/api/system.index) представляет индекс в последовательности.
- Оператор `^` (индекс с конца), который указывает, что индекс указан относительно конца последовательности.
- [System.Range](https://docs.microsoft.com/ru-ru/dotnet/api/system.range) представляет вложенный диапазон последовательности.
- Оператор диапазона `..`, который задает начало и конец диапазона в качестве своих операндов.

Начнем с правил для использования в индексах. Рассмотрим массив `sequence`. Индекс `0` совпадает с `sequence[0]`. Индекс `^0` совпадает с `sequence[sequence.Length]`. Обратите внимание, что `sequence[^0]` создает исключение так же, как и `sequence[sequence.Length]`. Для любого числа `n` индекс `^n` совпадает с `sequence.Length - n`.

Диапазон указывает *начало* и *конец* диапазона. Начало диапазона является включающим, но конец диапазона является исключающим, то есть *начало* включается в диапазон, а *конец* не включается. Диапазон `[0..^0]` представляет весь диапазон так же, как `[0..sequence.Length]` представляет весь диапазон.

```c#
int[] nt = temperatures[1..4]; - Range (..)
int[] nt = temperatures[^2]; - Index
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
string[] peopleRange = people[..];		// Все элементы
string[] peopleRange = people[1..];		// Bob, Sam, Kate, Alice (Со 2 по последний)
string[] peopleRange = people[1..4];	// Bob, Sam, Kate (Со 2 по 3 включительно)
string[] peopleRange = people[..4];		// Tom, Bob, Sam, Kate (с 1 по 3 включительно)
string[] peopleRange = people[^2..];       // два последних - Kate, Alice
string[] peopleRange = people[..^1];       // начиная с предпоследнего - Tom, Bob, Sam, Kate
string[] peopleRange = people[^3..^1];     // два начиная с предпоследнего - Sam, Kate
```

Также можно объявить диапазоны как переменные:

```c#
Range phrase = 1..4;
string[] peopleRange = people[phrase];
```



## Структура Span<T>

[Структура Span](https://docs.microsoft.com/ru-ru/dotnet/api/system.span-1?view=net-5.0)
Span позволяет работать с памятью более эффективно и избежать ненужных выделений памяти. 
Так, используем вместо массивов Span:

```c#
int[] temperatures = new int[]
{
    10, 12, 13, 14, 15, 11, 13, 15, 16, 17,
    18, 16, 15, 16, 17, 14,  9,  8, 10, 11,
    12, 14, 15, 15, 16, 15, 13, 12, 12, 11
};
Span<int> temperaturesSpan = temperatures;
Span<int> firstDecade = temperaturesSpan.Slice(0, 10);    // нет выделения памяти под данные
Span<int> lastDecade = temperaturesSpan.Slice(20, 10);    // нет выделения памяти под данные
```

Для создания производных объектов Span применяется метод Slice, который из Spana выделяет часть и возвращает ее в виде другого объекта Span. 
Теперь объекты Span firstDecade и lastDecade работают с теми же данными, что и temperaturesSpan, а дополнительно память не выделяется.
То есть во всех трех случаях мы работаем с тем же массивом temperatures. Мы даже можем в одном Span изменить данные, и данные изменятся в другом:

Span < T > — Это структура ссылки , которая выделяется в стеке, а не в управляемой куче. Типы структур ref имеют ряд ограничений,  чтобы гарантировать, что их нельзя повысить до управляемой кучи, в том числе что они не могут быть упакованы, они не могут быть назначены переменным типа Object dynamic или любому типу интерфейса, они не могут быть полями в ссылочном типе и не могут использоваться в await и в yield границах. Кроме того, вызовы двух методов Equals(Object) и GetHashCode вызывают исключение NotSupportedException.



## Присваивание объединения со значением NULL

В C# 8.0 появился оператор присваивания объединения со значением NULL `??=`. Оператор `??=` можно использовать для присваивания значения правого операнда левому операнду только в том случае, если левый операнд принимает значение `null`.

```c#
List<int> numbers = null;
int? i = null;

numbers ??= new List<int>();
numbers.Add(i ??= 17);
numbers.Add(i ??= 20);

Console.WriteLine(string.Join(" ", numbers));  // output: 17 17
Console.WriteLine(i);  // output: 17
```

