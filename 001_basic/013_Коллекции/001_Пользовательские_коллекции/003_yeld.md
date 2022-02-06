# Инструкция Yield

[Yield ](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/yield)

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

