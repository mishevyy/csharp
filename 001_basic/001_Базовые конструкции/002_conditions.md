# Условные конструкции

## Условная конструкция IF

### if

```c#
if (b > a)
{
    Console.WriteLine($"{b} > {a}");
}
```

### if - else

```c#
if (a > b)
{
    Console.WriteLine($"{a} > {b}");
}
else
{
    Console.WriteLine($"{b} > {a}");
}
```

### if - else if

```c#
if (a > b)
{
    Console.WriteLine($"{a} > {b}");
}
else if (a < b)
{
    Console.WriteLine($"{b} > {a}");
}
else
{
    Console.WriteLine($"{b} = {a}");
}
```

## Условная конструкция SWITCH-CASE

Оператор многозначного выбора `switch - case`  выполняет определенный код, в зависимости от выбора.

```c#
switch (val)
{
    case 1:
        {
            Console.WriteLine("Понедельник");
            break;
        }
    case 2:
        {
            Console.WriteLine("Вторник");
            break;
        }
    // ...    
    case 7:
        {
            Console.WriteLine("Восткресенье");
            break;
        }    
    default: // Блок default - необязателен, используется для выполнения кода по умолчанию.
        {
            Console.WriteLine("Ввод не коректен");
            break;
        }
}
```

В конструкции switch - case Можно использовать проваливание

```c#
switch (val)
{
    case 1:
    case 2:
    case 3:
    case 4:
    case 5:
        {
            Console.WriteLine("Сегодня рабочий день");
            break;
        }
    case 6:
    case 7:
        {
            Console.WriteLine("Сегодня выходжной!");
            break;
        }
}
```

## Тернарный (троичный) оператор [ ? : ]

Тернарный оператор является сокращенной формой конструкции `if ... else`.

```c#
int a = Int32.Parse(Console.ReadLine());
int b = Int32.Parse(Console.ReadLine());
int c = a > b ? a : b;
```

## Оператор поглощения NULL

Оператор поглощения `??` предназначен для работы с переменными которые могут иметь значение null

```c#
int? h = null;
int g = h ?? 5;
```

## Логические операторы

`& |`

Логические операторы `&` и `| проверяют всю цепочку выражения

`&` - если слева ложь, то проверка все равно пойдет дальше (проверит выражение справа)

`|` - если слева правда, то проверка все равно пойдет дальше (проверит выражение справа)

`&& ||`

Условный операторы `&&` и `||` не проверяют условие дальше, если первое условие ложно для операнда.

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
