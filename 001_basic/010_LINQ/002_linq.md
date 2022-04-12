# LINQ

LINQ обозначает целый набор технологий, создающих и использующих возможности интеграции запросов непосредственно в язык C#. Традиционно запросы к данным выражаются в виде простых строк без проверки типов при компиляции или поддержки IntelliSense

Запрос — это набор инструкций, которые описывают, какие данные необходимо извлечь из указанного источника (или источников) данных, а также описывают форму и организацию извлекаемых данных. Запрос отличается от полученного с его помощью результата.

LINQ поддерживает синтаксис выражения запросов и синтаксис методов.

Запрос не будет выполняться, пока вы не начнете обращаться к переменной запроса, например, с помощью инструкции `foreach`.
Во время компиляции выражения запроса преобразуются в вызовы метода стандартного оператора запроса. Для преобразования используются правила, заданные в спецификации C#
Мы рекомендуем при написании запросов LINQ использовать синтаксис запросов везде, где это возможно, а синтаксис метода — только если это совершенно необходимо. Между этими формами синтаксиса нет никакой разницы в семантике или производительности. Выражения запросов обычно более удобочитаемыми, чем аналогичные выражения с использованием синтаксиса метода.
Выражения запросов могут компилироваться в деревья выражений или в делегаты, в зависимости от типа, к которому применяется конкретный запрос. Запросы `IEnumerable<T>` компилируются в делегаты. Запросы `IQueryable` и `IQueryable<T>` компилируются в деревья выражений.

В выражении запроса иногда требуется сохранить результат вложенного выражения, который будет использоваться в последующих предложениях. Это можно сделать с помощью ключевого слова `let`, которое создает новую переменную диапазона и инициализирует ее, используя результат предоставленного выражения.

```c#
string[] strings =
{
    "A penny saved is a penny earned.",
    "The early bird catches the worm.",
    "The pen is mightier than the sword."
};

// Split the sentence into an array of words
// and select those whose first letter is a vowel.
var earlyBirdQuery =
    from sentence in strings
    let words = sentence.Split(' ')
    from word in words
    let w = word.ToLower()
    where w[0] == 'a' || w[0] == 'e'
    || w[0] == 'i' || w[0] == 'o'
    || w[0] == 'u'
    select word;

// Execute the query.
foreach (var v in earlyBirdQuery)
{
    Console.WriteLine("\"{0}\" starts with a vowel", v);
}
```

## Функции генерации последовательностей

```c#
public void RangeOfInteger()
{
    int[] numbers = Enumerable.Range(0, 10).ToArray();
    Array.ForEach(numbers, item => Console.WriteLine(item));
}

public void RepeatNumber()
{
    int[] numbers = Enumerable.Repeat(7, 10).ToArray();
    Array.ForEach(numbers, item => Console.WriteLine(item));
}
```

## Функции конвертации

```c#
public void ConverToArray()
{
    byte[] array = new byte[10];
    new Random(42).NextBytes(array);

    var sortedArray = (from a in array
                        select a).ToArray();
}

public void ConverToList()
{
    string[] words = { "cherry", "apple", "blueberry" };

    var listWords = (from w in words
                        select w).ToList();
}

public void ConvertToDictionary()
{
    var scoreRecords = new[]
    { 
        new {Name = "Alice", Score = 50},
        new {Name = "Bob"  , Score = 40},
        new {Name = "Cathy", Score = 45}
    };

    var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);
}

public void ConvertSelectedItems()
{
    object[] numbers = { null, 1.0, "two", 3, "four", 5, "six", 7.0 };

    var doubles = numbers.OfType<double>();

    Console.WriteLine("Numbers stored as doubles:");
    foreach (var d in doubles)
    {
        Console.WriteLine(d);
    }
}
```

## Операции объединения

```c#
public void CrossJoinQuery()
{
    string[] categories = {
        "Beverages",
        "Condiments",
        "Vegetables",
        "Dairy Products",
        "Seafood"
    };

    List<Product> products = GetProductList();

    var q = from c in categories
            join p in products on c equals p.Category
            select (Category: c, p.ProductName);
}

public void GroupJoinQquery()
{
    string[] categories = {
        "Beverages",
        "Condiments",
        "Vegetables",
        "Dairy Products",
        "Seafood"
    };

    List<Product> products = GetProductList();
    var q = from c in categories
            join p in products on c equals p.Category into ps
            select (Category: c, Products: ps);

    foreach (var v in q)
    {
        Console.WriteLine(v.Category + ":");
        foreach (var p in v.Products)
        {
            Console.WriteLine("   " + p.ProductName);
        }
    }            
}

public void CrossGroupJoin()
{
    string[] categories = {
        "Beverages",
        "Condiments",
        "Vegetables",
        "Dairy Products",
        "Seafood"
    };

    List<Product> products = GetProductList();

    var q = from c in categories
            join p in products on c equals p.Category into ps
            from p in ps
            select (Category: c, p.ProductName);

    foreach (var v in q)
    {
        Console.WriteLine(v.ProductName + ": " + v.Category);
    }        
}

public void LeftOuterJoin()
{
    string[] categories = {
        "Beverages",
        "Condiments",
        "Vegetables",
        "Dairy Products",
        "Seafood"
    };

    List<Product> products = GetProductList();

    var q = from c in categories
            join p in products on c equals p.Category into ps
            from p in ps.DefaultIfEmpty()
            select (Category: c, ProductName: p == null ? "(No products)" : p.ProductName);

    foreach (var v in q)
    {
        Console.WriteLine($"{v.ProductName}: {v.Category}");
    }          
}
```

## Операции над последовательностями

```c#
public void FirstElement()
{
    string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    string f1 = strings.First();

    // В случае не найденого элемента сгененрирует ошибку
    string f2 = strings.First(f => f.StartsWith("ree"));

    /****************************************************************/

    string fod = strings.FirstOrDefault();

    // В случае не найденого элемента вернет значение по умолчанию для типа
    string fod2 = strings.FirstOrDefault(f => f.StartsWith("lg"));

    /****************************************************************/

    // Возвращение элемента по индексу
    string ela = strings.ElementAt(0);
    string ela2 = strings.ElementAtOrDefault(0);
}

public void Quatifiers()
{
    string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    // Все элементы последовательности удовлетворяют условию
    bool isAll = strings.All(a => a.StartsWith("f"));

    // Хотя бы один элмент удовлетворяет условию
    bool isAny = strings.Any(a => a.StartsWith("f"));

    // Вернут true если последовательность содержит элементы
    bool isAny2 = strings.Any();
}

public void Restricted()
{
    string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    string[] str = strings.Where(w => w.StartsWith("z")).ToArray();

}

public void Ordering()
{
    string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    string[] ordered = strings.OrderBy(w => w).ToArray();
    string[] orderedDesc = strings.OrderByDescending(w => w).ToArray();
}

public void SequenceOperations()
{
    int[] vectorA = { 0, 2, 4, 5, 6 };
    int[] vectorB = { 1, 3, 5, 7, 8 };

    // Сложение
    int dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
    Console.WriteLine($"Dot product: {dotProduct}");

    // Склеивание
    var allNumbers = vectorA.Concat(vectorB);

    // Сравнение
    bool match = vectorA.SequenceEqual(vectorB);

    // Пересечения
    var intersectNumbers = vectorA.Intersect(vectorB);
    var exceptNumbers = vectorA.Except(vectorB);

    // Уникальные значения
    var dist = exceptNumbers.Distinct();

    // Объединение двух массивов
    var union = vectorA.Union(vectorB);

    // Разбивает элементы последовательности на блоки размером не более
    var chunked = vectorA.Concat(vectorB).Chunk(3);
}

public void AgregateOperation()
{
    int[] vectorA = { 0, 2, 4, 5, 6 };

    int sum = vectorA.Sum();
    int min = vectorA.Min();
    int max = vectorA.Max();
    int count = vectorA.Count();
    double average = vectorA.Average(); //Среднее

    double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

    // ПРименяет к последовательности агрегатную функцию
    double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);
}

public void Projections()
{
    IEnumerable<Customer> customers = CustomerData.CustomerList;

    var proectionSiquence = customers.Select(s => new { s.CustomerID, s.Address });

    foreach (var item in proectionSiquence)
    {
        Console.WriteLine(item.CustomerID + " " + item.Address);
    }
}

public void Partition()
{
    int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

    int[] taked = numbers.Take(3).ToArray();
    int[] skiped = numbers.Skip(5).ToArray();
    int[] takeWhile = numbers.TakeWhile(t => t % 2 == 0).ToArray();
    int[] skipWhile = numbers.SkipWhile(t => t % 2 == 0).ToArray();
}

public void Group()
{
    IEnumerable<Product> products = ProductData.ProductList;

    var query = products
        .GroupBy(g => g.ProductName)
        .Select(s => new { Product = s.Key, SumProd = s.Sum(f => f.UnitPrice) });

    foreach (var q in query)
    {
        Console.WriteLine(q.Product + " " + q.SumProd);
    }
}
```

```c#
public void Test()
{
    string[] words = { "one", "two", "three", "zero", };
    int[] numbers = { 1, 2, 3, 4, 5, };

    // Агрегирование
    int sum = numbers.Sum();
    int count = numbers.Count();
    double average = numbers.Average();
    long longCount = numbers.LongCount();
    int min = numbers.Min();
    int max = numbers.Max();

    string agg = words.Aggregate("seed",
        (current, item) => current + item,
        resultSelector => resultSelector.ToUpper());

    // Конкатенация
    int[] newArray = numbers.Concat(new int[] { 1, 2, 3, 4, 5, }).ToArray();

    // Преобразование
    object[] allStrings = { "These", "are", "all", "string" };
    object[] notAllStrings = { "Number", "at", "the", "end", 5 };

    string[] castString = allStrings.Cast<string>().ToArray();
    string[] ofTypeString = notAllStrings.OfType<string>().ToArray();

    var toDict = words.ToDictionary(k => k.Substring(0, 2));
    var toLookup = words.ToLookup(word => word[0]);

    // Операции элементов
    var el1 = words.ElementAt(2);
    var el2 = words.ElementAtOrDefault(10);
    var el3 = words.First();
    var el4 = words.First(w => w.Length == 3);
    var el5 = words.Last();
    // var el6 = words.Single();
    // var el7 = words.SingleOrDefault();

    // Эквивалентность
    var equ = words.SequenceEqual(new[] { "zero", "one", "two" });

    // Генерация
    var g1 = numbers.DefaultIfEmpty();
    var g2 = Enumerable.Range(0, 100);
    var g3 = Enumerable.Repeat(5, 2);
    var g4 = Enumerable.Empty<int>();

    // Группировка
    var gr1 = words.GroupBy(g => g.Length);
    var gr2 = words.GroupBy(g => g.Length, (key, h) => key + ": " + h.Count());

    // Соединение
    string[] names = { "Robing", "Ruth", "Bob", "Emma" };
    string[] colors = { "Red", "Blue", "Beige", "Green" };

    var jn1 = names.Join(
        colors,
        name => name[0],
        color => color[0],
        (name, color) => name + " - " + color);

    var jn2 = names.GroupJoin(
        colors,
        name => name[0],
        color => color[0],
        (name, matches) => name + ": " + string.Join("/", matches.ToArray()));

    // Разделение
    var take = words.Take(2);
    var skip = words.Skip(1);
    var takeWile = words.TakeWhile(w => w.Length > 3);

    // Проецирование
    var sl1 = words.Select(s => new { Word = s });
    var sl2 = words.SelectMany(word => word.ToCharArray());
    var sl3 = names.Zip(colors, (x, y) => x + " " + y);

    // Квантификаторы
    var qt1 = words.All(w => w.Length > 3);
    var qt2 = words.Any();
    var qt3 = words.Contains("four");

    // Фильтрация
    var fl1 = words.Where(w => w.Length == 2);

    // Операции основанные на множествах
    string[] abcd = { "a", "b", "c", "d" };
    string[] cd = { "c", "d" };

    var mn1 = abcd.Distinct();
    var mn2 = abcd.Intersect(cd);
    var mn3 = abcd.Union(cd);
    var mn4 = abcd.Except(cd);

    // Сортировка
    var sort1 = words.OrderBy(o => o);
    var sort2 = words.OrderBy(o => o[2]);
    var sort3 = words.OrderByDescending(o => o);
    var sort4 = words.OrderBy(o => o.Length).ThenBy(tb => tb);
    var sort5 = words.Reverse();
}
```
