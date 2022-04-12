# Сборка мусора

[Сборка мусора](http://msdn.microsoft.com/ru-ru/library/0xy59wtx.aspx)

[Основы сборки мусора](http://msdn.microsoft.com/ru-ru/library/ee787088.aspx#generations )

[класс GC](http://msdn.microsoft.com/ru-ru/library/system.gc.aspx )

## Деструктор

```c#
class MyClass
{
    // Деструктор(или методы Финализаторы)
    // Деструкторы не могут вызываться явно в С#, они не наследуются. 
    // Класс может иметь только один деструктор.
    // Когда вызывается финализатор объекта, то вызывается каждый финализатор в цепочке наследования — от последнего к первому.    
    // Выполняются в отдельном  потоке CLR. 
    ~MyClass()
    {
        Console.WriteLine("Hello, From destructor");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
}
```

## Паттерн IDisposable

[интерфейс IDisposable](http://msdn.microsoft.com/ru-ru/library/system.idisposable.aspx )

Шаблон Microsoft для освобождения ресурсов.
Данный паттерн гарантирует, что пользователь, сможет многократно вызывать метод Dispose().

```c#
public class ResourceWrapper : IDisposable
{
    // Флаг показывающий вызов метода Dispose()
    private bool disposed = false;

    public void Dispose()
    {
        // Вызов вспомогательного метода.
        // Если true, значит очистку инициировал пользователь объекта.
        CleanUp(true);

        // SuppressFinalize() - устанавливает флаг запрещения завершения для объектов
        // которые в противном случае могли бы быть завершены сборщиком мусора.
        // Отменяет работу деструктора для данного класса.
        GC.SuppressFinalize(this);
    }

    // Сборщик мусора вызывает Деструктор, если пользователь объекта забудет вызвать метод Dispose().
    ~ResourceWrapper()
    {
        Console.WriteLine("Finalise!!!!!!!!!!!");
        CleanUp(false);
    }

    // Метод для избежания дублирования кода в Деструкторе и методе Dispose().
    private void CleanUp(bool clean)
    {
        // Проверка, что ресурсы еще не освобождены.
        if (!this.disposed)
        {
            // Если clean равно true, освободить все управляемые ресурсы.
            if (clean)
            {
                Console.Write("Освобождение ресурсов");

                for (int i = 0; i < 40; i++)
                {
                    Console.Write("F");
                }
            }
            Console.WriteLine("Finalized");
        }
        this.disposed = true;
    }
}
   
static void Main(string[] args)
{
    using (var wrapper = new ResourceWrapper())
    {
        Console.WriteLine(wrapper);
    }
    Console.WriteLine(new string('-', 20));

    var wrapper2 = new ResourceWrapper();
    Console.WriteLine(wrapper2);

    wrapper2.Dispose();
    wrapper2.Dispose();
    wrapper2.Dispose();
    wrapper2.Dispose();

    var wrapper3 = new ResourceWrapper();

    Console.ReadKey();
    Console.WriteLine("Press any key to dispose");
}
```
