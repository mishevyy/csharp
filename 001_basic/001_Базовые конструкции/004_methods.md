# Методы

## Процедуры и функции

Метод - это именованная часть программы, которая может вызываться из других частей программы столько раз, сколько необходимо.

Методы делятся на процедуры и функции ( Функция имеет возвращаемое значение, процедура нет (или точнее возвращает void) ). Методы, которые возвращают логическое значение, называют методами-предикатами

```c#
static void Procedure()
{
    // какое -то действие
    return; // необязательно
}

static string Function()
{
    // какое -то действие
    return "Hello, World!";
}

// Методы, которые возвращают логическое значение, называют методами-предикатами.
static bool And(bool a, bool b)
{
    return a && b;
}

// Вызов методов
Procedure();
int f = Function();
bool isTrue = And(true, true);
```

## Перегрузка методов

Перегруженные методы могут отличаться типом и количеством аргументов, а также ref и out параметрами.
Перегрузка по возвращаемому значению не допустима.

```c#
static void Function()                 // 1-я перегрузка.   
{
    Console.WriteLine("Hello!");
}

static void Function(int i)            // 2-я перегрузка.
{
    Console.WriteLine(i);
}

static void Function(ref int i, string s) // 3-я перегрузка.
{
    Console.WriteLine(i + s);
}
```

## Методы с изменяемыми параметрами (ref = in/out)

Ключевое слово `ref` указывает, что значение передается по ссылке.

* Если **ссылочный тип** будет передан с ключевым словом ref, то можно заменить сам передаваемый объект.
* Если **значимый тип** будет передан с ключевым словом ref и в теле метода выполнится изменение переменной, ее значение будет изменено везде.

```c#
// после работы метода Foo operand = 5
int operand = 2;
int result = Foo(ref operand);
static void Foo(ref int a)
{
    a = 5;
}
```

Ключевым словом ref можно получить ссылку на значение в массиве или на поле объекта

```c#
string[] strArray = { "one", "two", "three" };
ref var refOutput = ref ReturnRef(strArray, 2); // в refOutput будет помещена ссылка на значение в массиве strArray
refOutput = "new";
Console.WriteLine("{0} {1} {2}", strArray[0], strArray[1], strArray[2]);

// Метод вернет ссылку на значение (через ключевое слово ref)
static ref string ReturnRef(string[] array, int pos)
{
    return ref array[pos];
}
```

```c#
MyClass mc1 = new MyClass(10);
ref var d  = ref Foo(ref mc1.a); // помещаем в переменную d ссылку на поле a класса MyClass
d = 33; // изменяем значение d, оно так же измениться в поле a класса MyClass

Console.WriteLine(mc1.a);

ref int Foo(ref int g)
{
    return ref g;
}
```

## Методы с выходными параметрами (out)

*out - позволяет передавать в метод не проинициализированные переменные, которая в итоге будет инициализирована в методе.*

```c#
int a;
Method(out int a, "10");
static void Method(out int a, string s)
{
    // Выходные параметры должны быть изменены в теле метода, иначе будет ошибка.
    a = Convert.ToInt32(s);
}
```

Существующий синтаксис, поддерживающий параметры `out`, был улучшен в C# 7. Переменные `out` можно объявлять в списке аргументов в вызове метода, не записывая отдельный оператор объявления:

```c#
if (int.TryParse(input, out int result))
    Console.WriteLine(result);
else
    Console.WriteLine("Could not parse input");
```

## Модификатор параметров (in)

*Ключевое слово in вызывает передачу аргументов по ссылке, но гарантирует, что аргумент не будет изменен. Это может потребоваться для передачи тяжеловестных структур.*

```c#
int readonlyArgument = 44;
InArgExample(readonlyArgument);
void InArgExample(in int number)
{
    // Uncomment the following line to see error CS8331
    // number = 19;
}
```

## Ключевое слово params

*С помощью ключевого слова params можно указать параметр метода, принимающий переменное число аргументов одного типа. Тип параметра должен быть одномерным массивом.*

```c#
void UseParams(params int[] list)
{
    for (int i = 0; i < list.Length; i++)
    {
        Console.Write(list[i] + " ");
    }
    Console.WriteLine();
}

// возможно передатть переменное колличесво значений
UseParams(1, 2, 3, 4);
// или не передовать
UseParams();
// или передать массив
int[] myIntArray = { 5, 6, 7, 8, 9 };
UseParams(myIntArray);
```

## Опциональные (Необязательные) и именованные параметры

*Метод вызывается так же, если бы это были перегрузки.*

```c#
void Operation(string value1 = "val", int value2 = 10, double value3 = 12.2)
{
    Console.WriteLine("{0}, {1}, {2}", value1, value2, value3);
}

Operation();                  // 1-я перегрузка.
Operation("val");             // 2-я перегрузка.
Operation("val", 10);         // 3-я перегрузка.
Operation("val", 10, 12.2);   // 4-я перегрузка.

// Вызов метода через именованные параметры
// Они состоят из указания имени параметра, двоеточия и значения, которое мы передаем.
Operation("val", value3: 12.2);
Operation(value2: 33, value3: 12.2);
```

## Рекурсия простая

Простая рекурсия -  вызов методом самого себя (самовызов). При каждом вызове строится новая копия метода.

```c#
void Recursion(int counter)
{
    counter--;
    Console.WriteLine("Первая половина метода: {0}", counter);

    if (counter != 0) // для правильной работы должно быть обязательно условие для выхода из рекурсии
        Recursion(counter);

    Console.WriteLine("Вторая половина метода: {0}", counter);
}
```

## Рекурсия сложная

Сложная рекурсия -  вызов методом себя, через другой метод.

```c#
void Recursion(int counter)
{
    counter--;
    Console.WriteLine("Первая половина метода Recursion: {0}", counter);

    if (counter != 0)
        Method(counter);

    Console.WriteLine("Вторая половина метода Recursion: {0}", counter);
}

void Method(int counter)
{
    Console.WriteLine("Первая половина метода Method: {0}", counter);
    Recursion(counter);
    Console.WriteLine("Вторая половина метода Method: {0}", counter);
}
```

### Локальные функции

Модели многих классов включают методы, вызываемые только из одного места. Эти дополнительные закрытые методы делают каждый метод небольшим и направленным. *Локальные функции* позволяют объявлять методы в контексте другого метода. Локальные функции позволяют читателям класса легче увидеть, что локальный метод вызывается только из контекста, в котором он объявлен.

```c#
public static IEnumerable<char> AlphabetSubset(char start, char end)
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

