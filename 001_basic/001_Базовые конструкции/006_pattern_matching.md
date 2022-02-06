
## Соответствие шаблону

Сопоставление шаблонов поддерживает выражения `is` и `switch`. Каждое из них позволяет проверять объект и его свойства и определять, соответствует ли этот объект искомому шаблону. Для добавления правил в шаблон используется ключевое слово `when`.

Выражение шаблона `is` позволяет использовать знакомый [оператор `is`](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/is) для запроса объекта о типе и присваивания результата в одной инструкции.

```c#
if (input is int count)
    sum += count;
```

Выражение сопоставления со switch имеет знакомый синтаксис, основанный на операторе `switch`, который уже является частью языка C#.

```c#
public static int SumPositiveNumbers(IEnumerable<object> sequence)
{
    int sum = 0;
    foreach (var i in sequence)
    {
        switch (i)
        {
            case 0:
                break;
            case IEnumerable<int> childSequence:
            {
                foreach(var item in childSequence)
                    sum += (item > 0) ? item : 0;
                break;
            }
            case int n when n > 0:
                sum += n;
                break;
            case null:
                throw new NullReferenceException("Null found in sequence");
            default:
                throw new InvalidOperationException("Unrecognized type");
        }
    }
    return sum;
}
```

- `case 0:` — [шаблон константы](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/patterns#constant-pattern).
- `case IEnumerable<int> childSequence:` — [шаблон объявления](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/patterns#declaration-and-type-patterns).
- `case int n when n > 0:` — шаблон объявления с дополнительным условием `when`.
- `case null:` — шаблон константы `null`.
- `default:` — знакомый вариант по умолчанию.


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