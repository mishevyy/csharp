# Функциональное программирование

## Замыкание

Прием, функиця в теле которой присутствует ссылка на переменную, объявленные вне тела этой функции и не в качестве ее параметров.

Анонимные методы могут захватывать контекст своего лексического окружения. Такая техника называется захватом переменной или на функциональном языке - замыканием.

```c#
// Переменная delta и делегат func формируют замыкание.
int delta = 100;
Func<int, int> func = x => delta * x;
int result = func(5);
```

***

## Объединение

_Комбинирует два и более делегата в один._

```c#
Func<double, double> f2f1 = Chain((double x) => x * 3, x => x + 3.1415);
Console.WriteLine(f2f1(3));
```

_Метод принимает два делегата и производит третий делегат, комбинируя первые два._

```c#
Func<T1, T2> Chain<T1, R, T2>(Func<T1, R> f1, Func<R, T2> f2)
{
    return x => f2.Invoke(f1.Invoke(x));
}
```

***

## Карирование (currying)

Преобразование функции от пары аргументов в функцию, берущую свои аргументы по одному.

Декарирование вводится как оббратное преобразование.

```c#
public static class CurryExtension
{
    // Каррирование декомпозирует функцию на функции от одного аргумента. 
    public static Func<T2, Func<T1, R>> Curry<T1, T2, R>(this Func<T1, T2, R> func)
    {
        return y => x => func.Invoke(x, y);
    }
}

class Program
{
    static void Main()
    {
        var myList = new List<double> { 1.0, 3.4, 5.4, 6.5 };
        var newList = new List<double>();

        // Обычная функция, до преобразования
        Func<double, double, double> func = (x, y) => x + y;

        // Здесь - каррирование.
        // В отличие от частичного применения мы не передаем никаких дополнительных аргументов в метод Curry, 
        // кроме преобразуемой функции
        // (x, y) => x + y   ----------> y => x => func(x, y)
        Func<double, Func<double, double>> curried = func.Curry();

        foreach (var item in myList)
        {
            newList.Add(curried(3)(item));
            // newList.Add(func(3, item));
        }

        foreach (double item in newList)
            Console.Write("{0} ", item);

        Console.ReadKey();
    }
}
```

***

## Мемоизация

Специальная техника, которая позволяет увеличить скорость выполнения программ. Данная методика заключается в том, что бы исключить повторное вычисление результатов предыдущих вызовов.

```c#
public static class Memoizer
{
    public static Func<T, R> Memoize<T, R>(this Func<T, R> func)
    {
        var cache = new Dictionary<T, R>();

        return x =>
        {
            R result = default(R);
            if (cache.TryGetValue(x, out result))
                return result;

            result = func(x);
            cache[x] = result;
            return result;
        };
    }
}

class Program
{
    static void Main()
    {
        Func<uint, ulong> fib = null;

        fib = x => x > 1 ? fib(x - 1) + fib(x - 2) : x;
        fib = fib.Memoize();

        for (uint i = 0; i < 95; i++)
        {
            Console.WriteLine("{0:D2}-е число {1}", i + 1, fib.Invoke(i));
        }
    }
}
```
