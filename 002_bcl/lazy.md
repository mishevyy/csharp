# Класс Lazy'<'T'>'

 [Lazy](https://docs.microsoft.com/ru-ru/dotnet/api/system.lazy-1?view=net-5.0)

Lazy  класс предостовляет отложенную инициализацию объекта те объект не будет инициализирован до тех пор пока не потребуется его вызов

```c#
class Library
{
    public string[] books = new string[100];
}

class Reader
{
    Lazy<Library> library = new Lazy<Library>();
    public void ReadBook()
    {
        Console.WriteLine(library.Value.books[4]);
    }
}
```
