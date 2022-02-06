# Перечисления

Перечисления - это набор именованных констант, которые хранят числовые значения

Все перечисления унаследованн от базового класса System.Enum

При компиляции - компилятор подставляет вместо имен, установленные им значения.

По умолчанию типом данных для перечислений будет int, но можно использовать любой целый тип данных C# (byte, sbyte, short, ushort, int, uint, long, ulong)

```c#
enum TestEnum // : byte - указание явного типа
{
    Zero,
    One,
    Two,
    Three = 3,  // Указание явных значений
    Ten = 10
}
```

***

## Получение описание Enum

```c#
enum Gender
{
    [Description("Мужской")]
    Male,
    [Description("Женский")]
    Female
}

Type type = typeof(Gender);
var values = Enum.GetValues(type);
foreach (var val in values)
{
    var memberInfo = type.GetMember(type.GetEnumName(val));

    var description = memberInfo[0]
        .GetCustomAttributes(typeof(DescriptionAttribute), false)
        .FirstOrDefault() as DescriptionAttribute;

    Console.WriteLine(description);
}
```
