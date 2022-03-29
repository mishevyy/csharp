# `Dictionary<TKey,TValue>`

Представляет коллекцию ключей и значений.
[Dictionary](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.dictionary-2?view=net-5.0)

*Создание словаря*.

```c#
Dictionary<string, string> openWith = new Dictionary<string, string>();
```

*Добавление элементов в словарь.*

```c#
openWith.Add("txt", "notepad.exe");
openWith.Add("bmp", "paint.exe");
openWith.Add("dib", "paint.exe");
openWith.Add("rtf", "wordpad.exe");
```

*Попытка добавить элемент с существующим ключом приведет к ошибке.*

```c#
try
{
    openWith.Add("txt", "winword.exe");
}
catch (ArgumentException)
{
    Console.WriteLine("Элемент с ключом txt уже существует");
}
```

*Получение значения по ключу.*

```c#
string item = openWith["rtf"];
```

*Изменение значение по ключу.*

```c#
openWith["rtf"] = "winword.exe";
```

Если ключ не существует, то он создастся.

```c#
openWith["doc"] = "winword.exe";
```

*Обращение к несуществующему индексу приведет к ошибке.*

```c#
try
{
    string ow = openWith["tif"];
}
catch (KeyNotFoundException)
{
    Console.WriteLine("Такого ключа не существует");
}
```

*Получение, добавление и удаление значений.*
*Получение.*

```c#
string value = "";
openWith.TryGetValue("ht", out value);
```

*Добавление.*

```c#
openWith.TryAdd("ht", "hyperterm.ht");
```

*Удаление.*

```c#
if (!openWith.ContainsKey("ht"))
{
    openWith.Remove("ht");
}
```

*Чтение словаря в foreach указывайте явно тип.*

```c#
foreach (KeyValuePair<string, string> k in openWith)
{
    Console.WriteLine(k.Value);
}
```

*Получение коллекции значений.*

```c#
Dictionary<string, string>.ValueCollection valuesCollection = openWith.Values;
```

*Получение коллекции ключей.*

```c#
Dictionary<string, string>.KeyCollection keyCollection = openWith.Keys;
```
