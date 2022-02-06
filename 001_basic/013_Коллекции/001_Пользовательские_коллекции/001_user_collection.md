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
public IEnumerator GetEnumerator()
{
    return this;
}    
```

**Реализация интерфейса IEnumerator: Свойство Current и Метод MoveNext()**

```c#
private int position = -1;

public object Current
{
    get { return elements[position]; }
}

public bool MoveNext()
{
    if (position < elements.Length - 1)
    {
        position++;
        return true;
    }
    return false;
}  
```

**Реализация интерфейса IEnumerator: метод Reset().**

```c#
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

