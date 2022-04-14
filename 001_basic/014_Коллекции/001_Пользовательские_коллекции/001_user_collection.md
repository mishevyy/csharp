# Создание пользовательской коллекции

## Реализация интерфейсов IEnumerable, IEnumerator, IDisposable

```c#
class UserCollection : IEnumerable, IEnumerator, IDisposable
{
    readonly Element[] elements = new Element[4];
    public Element this[int index]
    {
        get { return elements[index]; }
        set { elements[index] = value; }
    }    
}
```

**Реализация интерфейса IEnumerable:**

```c#
// Метод GetEnumerator возвращает текущий объект, задача GetEnumerator вернуть объект в котором буду находится метод MoveNext и поле Current
public IEnumerator GetEnumerator()
{
    return this;
}    
```

**Реализация интерфейса IEnumerator: свойство Current, метод MoveNext() и метод Reset().**

```c#
// Начальное состояние
private int position = -1;

// Текущий элемент коллекции
public object Current
{
    get { return elements[position]; }
}

// Получение следующего элемента
public bool MoveNext()
{
    if (position < elements.Length - 1)
    {
        position++;
        return true;
    }
    return false;
}

// Возврат в начало коллекции
public void Reset()
{
    position = -1;
}
```

**Реализация интерфейса IDisposable:**

```c#
public void Dispose()
{
    Reset();
}
```
