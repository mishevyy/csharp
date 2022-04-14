# Инструкция Yield

[Yield](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/yield)

Использование в операторе yield слова yield означает, что метод, оператор или метод доступа get, в котором присутствует это ключевое слово, является итератором. Использование yield для определения итератора исключает необходимость применения явного дополнительного класса (в котором содержится состояние перечисления; в качестве примера см. `IEnumerator<T>`) при реализации шаблонов IEnumerable и IEnumerator для пользовательского типа коллекции.
Оператор yield return используется для возврата каждого элемента по одному.

Ключевое слово yeld является синтаксическим сахаром и представляется компилятором,
как объект хранящий текущее состояние объекта

*Итератор. Может существовать только внутри класса.*

```c#
public IEnumerator GetEnumerator()
{
    for (int i = 0; i < 10; i++)
    {
        yield return i;
    }
}
```

*Именованый итератор. Может существовать вне класса.*

```c#
public IEnumerable GetNumbers()
{
    for (int i = 0; i < 10; i++)
    {
        yield return i;
    }
}
```

*Именованый итератор и ключевое слово break.*  

```c#
 public IEnumerable GetNumbersTo()
 {
     for (int i = 0; i < 10; i++)
     {
         if (i == 5)
         {
             yield break;
         }
         yield return i;
     }
 }
```

*Использование итератора и именованного итератора.*

```c#
var cl = new MyClass();

foreach (var item in cl)
{
    Console.WriteLine(item);
}

foreach (var item in cl.GetNumbers())
{
    Console.WriteLine(item);
}
```

*Обработка исключений.*

* Оператор yield return нельзя размещать в блоке try-catch. Оператор yield return можно размещать в блоке try оператора try-finally.

* Оператор yield break можно размещать в блоке try или catch, но не в блоке finally.

* Если тело foreach или finally (за пределами метода итератора) вызывает исключение, выполняется блок await foreach в методе итератора.

```c#
static IEnumerable<int> IntDevSeq(int a)
{
    if (a == 0) throw new Exception("Деление на 0");
    return IntDev(a);    
}

static IEnumerable<int> IntDev(int a)
{
    for (int i = 5; i > 0; i--)
    {
        try
        {
            yield return i / a;
        }
        finally { }
    }
}
```
