# List<T>

Представляет строго типизированный список объектов, доступных по индексу. 
Поддерживает методы для поиска по списку, выполнения сортировки и других операций со списками.
https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.list-1

## Работа с коллекцией на примере класса

```c#
class Element : IComparable<Element>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CompareTo(Element element)
    {
        if (element == null)
            return 0;
        else return Id.CompareTo(element.Id);
    }
    
    public override string ToString()
    {
        return string.Format($"ID: {Id} - Name: {Name}");
    }
}
```

*Инициализация коллекции.*

```c#
List<Element> elements = new List<Element>();

 // Добавление елементов
elements.Add(new Element { Id = 1, Name = "One" });
elements.Add(new Element { Id = 3, Name = "Three" });
elements.Add(new Element { Id = 2, Name = "Two" });
```

*Если известно необходимое количество элементов то можно его задать в свойстве Capacity это ускорит работу с коллекцией.*

```c#
elements.Capacity = 100;
```

*Удаление элемента из коллекции по индексу (индекс не может быть больше размера коллекции).*

```c#
elements.RemoveAt(2);
```

*Вставка элемента по индексу (возможно вставить только в свободный индекс)*

```c#
elements.Insert(2, new Element { Id = 4, Name = "Four" });
```

*Если количество Count больше размера Capacity можно оптимизировать коллекцию TrimExcess()*

```
elements.TrimExcess();
```

*Сортировка. Для использования метода Sort Необходимо что бы объект реализовывал интерфейс IComparable<Element>*

```c#
elements.Sort();
```

*Сортировка с применением делегата Comparison<T>*

```c#
elements.Sort((x, y) =>
{
    if (x.Name == null && y.Name == null) return 0;
    else if (x.Name == null) return -1;
    else if (y.Name == null) return 1;
    return x.Name.CompareTo(y.Name);
});
```

*Перебор элементов в анонимном методе.*

```c#
elements.ForEach(x => Console.WriteLine(x));
```

*TrueForAll проверяет все ли элементы коллекции удовлетворяют некоторому условию.*

```c#
bool test = elements.TrueForAll(x => x.Id.GetType() == typeof(int));
```

