# Классы индексаторы

Класс индексатор позволяет работать с объектом, как с массивом [Классы индексаторы](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/indexers/using-indexers)
Индексаторы позволяют индексировать экземпляры класса или структуры так же, как массивы. Индексаторы напоминают свойства, но их методы доступа принимают параметры.

```c#
class MyIndexer
{
    private int[] t_stuff;
    public int Length { get => t_stuff.Length; }

    public int this[int i]
    {
        get
        {
            if(i > Length)
                throw new IndexOutOfRangeException();
            
            return t_stuff[i];
        }
        set
        {
            if (i > Length)
                throw new IndexOutOfRangeException();

            t_stuff[i] = value;
        }
    }

    public MyIndexer()
    {
        t_stuff = new int[10];

        for (int i = 0; i < Length; i++)
        {
            t_stuff[i] = i * 2;
        }
    }
}  
```

```c#
MyIndexer myIndexer = new MyIndexer();
for (int i = 0; i < myIndexer.Length; i++)
{
    Console.WriteLine(myIndexer[i]);
}
```
