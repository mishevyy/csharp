# Реализация дженерик пользовательской коллекции

```c#
class UserCollection<T> : IEnumerable<T>, IEnumerator<T>, IDisposable
{
    readonly T[] elements = new T[4];
    public T this[int index]
    {
        get { return elements[index]; }
        set { elements[index] = value; }
    }
}
```

**Реализация интерфейса IEnumerable<T>**

```c#
IEnumerator IEnumerable.GetEnumerator()
{
    return this;
}

IEnumerator<T> IEnumerable<T>.GetEnumerator()
{
    return this;
}
```

**Реализация интерфейса IEnumerator<T>**

```c#
private int position = -1;

object IEnumerator.Current
{
    get { return elements[position]; }
}

T IEnumerator<T>.Current
{
    get { return elements[position]; }
}

bool IEnumerator.MoveNext()
{
    if (position < elements.Length - 1)
    {
        position++;
        return true;
    }
    return false;
}

void IEnumerator.Reset()
{
    position = -1;
}
```

**Реализация интерфейса IDisposable:** 

```c#
void IDisposable.Dispose()
{
    Reset();
}
```

