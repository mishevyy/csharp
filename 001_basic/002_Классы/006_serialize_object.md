# Сериализация объектов

[Сериализация объектов](https://docs.microsoft.com/ru-ru/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0)

**Сериализация** объектов используется для копирования объектов, или сохранения объектов, например в файле или БД, что бы можно было восстановить объект позже. Обратный процесс называется **Десиарелизацией**.

_Реализация интерфейса ICloneable._

```c#
[Serializable]
class MyClass : ICloneable
{
    public int Field { get; set; }
    public string Field2 { get; set; }

    public object Clone()
    {
        string tmp = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<object>(tmp);
    }
}
```

_Сериализация с помощью JSON объекта._

```c#
public MyClass CopyFromJsonSerializer(MyClass instance)
{
    string jsonTemp = JsonSerializer.Serialize(instance);
    return JsonSerializer.Deserialize<MyClass>(jsonTemp);
}
```

_Использование бинарной сериализации, является не безопасной и устаревшей._

```c#
public MyClass CopyBinarySerializer(MyClass instanse)
{
    MyClass copy;
    using (Stream stream = new MemoryStream())
    {
        BinaryFormatter serialize = new BinaryFormatter();
        serialize.Serialize(stream, instanse);
        stream.Position = 0;
        copy = (MyClass)serialize.Deserialize(stream);
    }
    return copy;
}
```
