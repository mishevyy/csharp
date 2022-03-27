# Новые возможности в C# версий 7.0–7.3

## Кортежи и пустые переменные

[Кортежи](https://docs.microsoft.com/ru-ru/dotnet/csharp/whats-new/csharp-7#tuples-and-discards) — это упрощенные структуры данных, содержащие несколько полей для представления элементов данных. Поля не проверяются, и собственные методы определять нельзя. Типы кортежей в C# поддерживают `==` и `!=`.

```c#
(string Alpha, string Beta) namedLetters = ("a", "b");
Console.WriteLine($"{namedLetters.Alpha}, {namedLetters.Beta}");
```

*Пустые переменные.*

Пустая переменная представляет собой доступную только для записи переменную с именем `_` (знак подчеркивания). Вы можете назначить одной переменной все значения, которые не потребуются в дальнейшем. Пустая переменная является аналогом не присвоенной переменной и не может использоваться в коде где-либо, за исключением оператора присваивания.

Пустые переменные поддерживается в следующих случаях.

- При деконструкции кортежей или пользовательских типов.

  ```c#
  var (_, pop, _) = QueryCityData("New York City");
  ```

- При вызове методов с параметрами [out](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/out-parameter-modifier).

- В операции сопоставления шаблонов с помощью [оператора `is`](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/is) и [оператора `switch`](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement).

- В качестве автономного идентификатора в тех случаях, когда требуется явно идентифицировать значение присваивания как пустую переменную.

  ```c#
  _ = res == null ? res = 100 : res;
  ```

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

## Элементы воплощающие выражения

В C# версии 6 появились элементы, воплощающие выражение, для функций-членов и свойств, доступных только для чтения. В C# 7.0 расширен список допустимых членов, которые могут быть реализованы как выражения. В C# 7.0 можно реализовать *конструкторы*, *методы завершения*, а также методы доступа `get` и `set` для *свойств* и *индексаторов*.

```c#
// Выражение в конструкторе
public ExpressionMembersExample(string label) => this.Label = label;

private string label;

// Выражения в свойствах
public string Label
{
    get => label;
    set => this.label = value ?? "Default label";
}

// throw
public string Name
{
    get => name;
    set => name = value ??
        throw new ArgumentNullException(paramName: nameof(value), message: "Name cannot be null");
}

```

### Другие изменения

#### Асинхронный метод Main

```c#
static async Task Main(){}
// или
static async Task<int> Main(){}
```

#### Локальные функции

Модели многих классов включают методы, вызываемые только из одного места. Эти дополнительные закрытые методы делают каждый метод небольшим и направленным. *Локальные функции* позволяют объявлять методы в контексте другого метода. Локальные функции позволяют читателям класса легче увидеть, что локальный метод вызывается только из контекста, в котором он объявлен.

```c#
public static IEnumerable<char> AlphabetSubset3(char start, char end)
{
    if (start < 'a' || start > 'z')
        throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
    if (end < 'a' || end > 'z')
        throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

    if (end <= start)
        throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

    return alphabetSubsetImplementation();

    IEnumerable<char> alphabetSubsetImplementation()
    {
        for (var c = start; c < end; c++)
            yield return c;
    }
}
```

#### Литеральное выражение default

Литеральные выражения по умолчанию — это усовершенствование выражения значения по умолчанию. Эти выражения инициализируют переменную до значения по умолчанию.

```c#
int i = default;
```

#### Усовершенствования в синтаксисе числовых литералов

Неправильное толкование числовых констант затрудняет понимание кода при первом прочтении. Битовые маски или другие символьные значения могут вызывать затруднения. C# 7.0 содержит две новые возможности для записи чисел в удобочитаемом виде: *двоичные литералы* и *разделители цифр*.

```c#
public const int Sixteen =   0b0001_0000;
public const int ThirtyTwo = 0b0010_0000;
public const int SixtyFour = 0b0100_0000;
public const int OneHundredTwentyEight = 0b1000_0000;

public const long BillionsAndBillions = 100_000_000_000;
```

#### Переменные out

Существующий синтаксис, поддерживающий параметры `out`, был улучшен в C# 7. Переменные `out` можно объявлять в списке аргументов в вызове метода, не записывая отдельный оператор объявления:

```c#
if (int.TryParse(input, out int result))
    Console.WriteLine(result);
else
    Console.WriteLine("Could not parse input");
```

#### Модификатор доступа private protected

Новый составной модификатор доступа `private protected` указывает, что доступ к члену может осуществляться содержащим классом или производными классами, которые объявлены в рамках одной сборки.

## Ограничение дженерика типом Enum

```c#
enum Rainbow
{
    Red,
    Orange,
    Yellow        
}

static Dictionary<int, string> EnumNamedValues<T>() where T : Enum
{
    var result = new Dictionary<int, string>();
    var values = Enum.GetValues(typeof(T));

    foreach (int item in values)
        result.Add(item, Enum.GetName(typeof(T), item));

    return result;
}

var map = EnumNamedValues<Rainbow>();

foreach (var pair in map)
    Console.WriteLine($"{pair.Key}:\t{pair.Value}");
```
