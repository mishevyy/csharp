# Минимальные требования для работы foreach
`foreach` требует наличия в классе коллекций открытого метода GetEnumerator() и открытый метод MoveNext и поле Current или метода GetEnumerator(), который возвращает тип в котором размещены методы MoveNext и поле Current для того что бы в foreach вызвался метод Reset нужно  унаследовать интерфейс IDisposable, в реализации метода Dispose который вызывает методв Reset()

```c#
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class PersonCollection 
{
    private readonly Person[] persons;

    public PersonCollection()
    {
        persons = new Person[2];
        persons[0] = new Person("Jhon", 30);
        persons[1] = new Person("Adam", 43);
    }        
    
    public Person this[int index]
    {
        get { return persons[index]; }
        set { persons[index] = value; }
    }
    
    private int position = -1;
    public PersonCollection GetEnumerator()
    {
        return this;
    }
    
    public Person Current
    {
        get { return persons[position]; }
    }
    
    public bool MoveNext()
    {
        if (position < persons.Length - 1)
        {
            position++;
            return true;
        }
    
        return false;
    }
}
```

