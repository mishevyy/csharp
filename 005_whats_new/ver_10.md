# Новые возможности C# 10.0

## Естественный тип для лямбда выражении

[лямбда]( https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/lambda-expressions#natural-type-for-lambda-expressions)

```c#
var op = (string s) => Console.WriteLine(s);
op("asdasd");
```

## Структуры записей

Возможность объявлять типы записей как структуры или ссылочный тип
В оставшейся части этой статьи обсуждаются типы record class и record struct. Различия подробно описаны в каждом разделе. Выберите между record class и record struct, как вы выбираете между class и struct. Термин запись используется для описания поведения, которое применяется ко всем типам записей. record struct или record class используется для описания поведения, которое применяется только к типам структур или классов соответственно.
Раньше можно было создавать записи только ссылочного типа record `MyRecordS(double X, double Y);`
Типы записей могут запечатывать ToString

```c#
record struct RecordStruct(double X, double Y);
readonly record struct ReadonlyRecordStruct(double X, double Y);
record class MyRecordClass();
```

## Конструкторы без параметров и инициализаторы полей для типов структур

```c#
public readonly struct Measurement
{
    public Measurement()
    {
        Value = double.NaN;
        Description = "Undefined";
    }

    public Measurement(double value, string description)
    {
        Value = value;
        Description = description;
    }

    public double Value { get; init; }
    public string Description { get; init; }

    public override string ToString() => $"{Value} ({Description})";
}
```

## Глобальные директивы using

`global using <fully-qualified-namespace>;`
