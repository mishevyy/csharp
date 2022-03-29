# Обобщенные делегаты

Дженерик делегат. `<T>` - тип, котрый будет принимать делегат, или возвращать.

1. Указатель типа, является входным параметром
`delegate void GenericDelegateVar1<T>(T t);`

2. Указатель типа, является выходным параметром
`delegate T GenericDelegateVar2<T>();`

3. Указатель типа, T - входной параметр, R - выходной параметр
`delegate R GenericDelegateVar3<T, R>(T r);`

4. Указатель типа, несколько входных параметров. Важно указывать выходной тип, последним параметром.
`delegate R GenerciDelegateVar4<T1, T2, R>(T1 t1, T2 t2);`

5. Явное указывание вида указателя.
`delegate void GenericDelegateVar5In<in T>(T t);`
`delegate T GenericDelegeteVar5Out<out T>();`

## Делегаты Func и Action

Action - не имеет возвращаемого значения, может иметь до 16 дженерик типов

Func -  имеет возвращаемое значения, может иметь до 16 дженерик типов

```c#
static void Main()
{
    // Пример вызова и работы Action
    Action<string, ConsoleColor, int> actionMessage = DisplayMessage;
    actionMessage.Invoke("Hello, World!", ConsoleColor.Magenta, 5);

    // Пример вызова и работы Func. Последний параметр - вызываемое значение
    Func<int, int, int> func = Add;
    int res = func.Invoke(5, 7);

    Console.WriteLine(res);
}

static void DisplayMessage(string msg, ConsoleColor txtColor, int printCount)
{
    ConsoleColor previous = Console.ForegroundColor;

    Console.ForegroundColor = txtColor;

    for (int i = 0; i < printCount; i++)
    {
        Console.WriteLine(msg);
    }

    Console.ForegroundColor = previous;
}

static int Add(int a, int b)
{
    return a + b;
}
```

## Ковариантность и контрвариантность в делегатах

```c#
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public override string ToString()
    {
        return string.Format($"Name: {Name}, Age: {Age}");
    }
}

class Employee : Person
{
    public string Profession { get; set; }
    public override string ToString()
    {
        return string.Format($"Name: {Name}, Age: {Age}, Profession: {Profession}");
    }
}
   
static void Main()
{
    // Контрвариантность означает, что метод может принимать параметр,
    // который является базовым для типа параметра
    Action<Employee> a = new Action<Person>(MethodPerson);

    // Ковариантность означает, что метод может возвратить тип,
    // производный от типа возвращаемого делегата
    Func<Person> f = new Func<Employee>(MethodEmpployee);
}

static void MethodPerson(Person p) { }
static Employee MethodEmpployee() { return new Employee(); }
```
