# События

[События](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/events/)
[Ключевое слово](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/event)
[Подписка на события](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/events/how-to-subscribe-to-and-unsubscribe-from-events)

Событийно-ориентированное программирование (event-driven programming) — парадигма программирования, в которой выполнение программы определяется событиями — действиями пользователя (клавиатура, мышь, сообщениями других программ и потоков, событиями операционной системы (например, поступлением сетевого пакета).

Событийно-ориентированное программирование, как правило, применяется в трех случаях:
    1. При построении пользовательских интерфейсов (в том числе графических);
    2. При создании серверных приложений в случае, если по тем или иным причинам нежелательно порождение обслуживающих процессов;
    3. При программировании игр, в которых осуществляется управление множеством объектов.

События позволяют классу или объекту уведомлять другие классы или объекты о возникновении каких-либо ситуаций. Класс, отправляющий (или вызывающий) событие, называется издателем.  Классы, принимающие (или обрабатывающие) событие, называются подписчиками.
Используйте ключевое слово `event` для объявления события в классе издателя.

События имеют следующие свойства:
    1. Издатель определяет момент вызова события, подписчики определяют предпринятое ответное действие.
    2. У события может быть несколько подписчиков. Подписчик может обрабатывать несколько событий от нескольких издателей.
    3. События, не имеющие подписчиков, никогда не возникают.
    4. Обычно события используются для оповещения о действиях пользователя, таких как нажатия кнопок или выбор меню и их пунктов в графическом пользовательском интерфейсе.
    5. Если событие имеет несколько подписчиков, то при его возникновении происходит синхронный вызов обработчиков событий.
    6. В библиотеке классов .NET Framework в основе событий лежит делегат EventHandler и базовый класс EventArgs.

Сигнатура обработчика событий должна соответствовать следующим соглашениям:
    1. Метод обработчик события принимает ровно два параметра.
    2. Первый параметр называется sender и имеет тип Object. Это объект, вызвавший событие.
    3. Второй параметр называется - e и имеет тип EventArgs или тип производного класса от EventArgs. Это данные, специфичные для события.
    4. Тип возвращаемого значения метода обработчика — Void.

Чтобы класс мог породить событие, необходимо подготовить три следующих элемента:
    1. Класс, предоставляющий данные для события.
    2. Делегат события.
    3. Класс, порождающий событие.

События это особый тип многоадресных делегатов, которые можно вызвать только из класса или структуры, в которой они объявлены (класс издателя). Если на событие подписаны другие классы или структуры, их методы обработчиков событий будут вызваны, когда класс издателя инициирует событие.
События можно пометить как открытые public, private, protected, (nternal или protected internal.
Событие можно объявить как статическое событие при помощи ключевого слова static. При этом событие становится доступным для вызова в любое время, даже если экземпляр класса отсутствует.
Событие может быть помечено как виртуальное событие при помощи ключевого слова virtual. Это позволяет производным классам переопределять поведение события при помощи ключевого слова override.
События могут быть абстрактными.
Контекстно-зависимое ключевое слово add используется для определения пользовательского метода доступа к событию, вызываемому при подписке клиентского кода к событию. Если указан пользовательский метод доступа add, то необходимо также указать метод доступа remove.
Контекстно-зависимое ключевое слово remove используется для определения пользовательского метода доступа к событию, вызываемому при отмене подписки клиентского кода от события. Если указан пользовательский метод доступа remove, то необходимо также указать метод доступа add.

```c#
public delegate void EventDelegate();

class MyClass
{
    EventDelegate myEvent = null;
    public event EventDelegate MyEvent
    {
        add { myEvent += value; }
        remove { myEvent -= value; }
    }

    public void InvokeEvent()
    {
        myEvent.Invoke();
    }
}

static void Main()
{
    MyClass instance = new MyClass();
    instance.MyEvent += new EventDelegate(Handler1);    // Подписка на событие полная форма
    instance.MyEvent += Handler2;                       // Подписка на событие краткая форма (предположение делегата)

    instance.InvokeEvent();

    Console.WriteLine(new string('-', 50));

    instance.MyEvent -= Handler2; // Открепляем обрапотчик события.

    instance.InvokeEvent();
}

static private void Handler1()
{
    Console.WriteLine("Обработчик события 1");
}
static private void Handler2()
{
    Console.WriteLine("Обработчик события 2");
}
```

***

## События (abstract and virtual)

```c#
public delegate void EventHandler();

interface IInterface
{
    event EventHandler MyEvent;
}

class BaseClass : IInterface
{
    EventHandler myEvent;

    public virtual event EventHandler MyEvent
    {
        add { myEvent += value; }
        remove { myEvent -= value; }
    }

    public void InvokeEvent()
    {
        myEvent.Invoke();
    }
}

class DerivedClass : BaseClass
{
    public override event EventHandler MyEvent
    {
        add
        {
            base.MyEvent += value;
            Console.WriteLine("К событию базового класса был прикреплен обработчик - {0}", value.Method.Name);
        }
        remove
        {
            base.MyEvent -= value;
            Console.WriteLine("От события базового класса был прикреплен обработчик - {0}", value.Method.Name);
        }
    }
}

static void Main()
{
    DerivedClass instance = new DerivedClass();

    instance.MyEvent += new EventHandler(Handler1);
    instance.MyEvent += Handler2;

    instance.InvokeEvent();
}

// Методы обработчики события.
static private void Handler1()
{
    Console.WriteLine("Обработчик события 1");
}

static private void Handler2()
{
    Console.WriteLine("Обработчик события 2");
}
```

***

## Дженерик событие

```c#

//Используем делегат EventHandler<T>
public class CarEventManager : EventArgs
{
    public readonly string msg;

    public CarEventManager(string msg)
    {
        this.msg = msg;
    }
}

class Car
{
    public int CurrentSpeed { get; set; }
    public int MaxSpeed { get; set; }
    public string PetName { get; set; }
    private bool carIsDead;
    public Car() {}    

    public Car(string name, int maxSpeed, int curSpeed)
    {
        PetName = name;
        MaxSpeed = maxSpeed;
        CurrentSpeed = curSpeed;
    }

    // 1. Создание событий типа EventHandler<>
    public event EventHandler<CarEventManager> Exploted;
    public event EventHandler<CarEventManager> AboutToBlow;

    // 2. Метод в котором может возникнуть событие
    public void Accelerate(int delta)
    {
        if (carIsDead)
        {
            Exploted?.Invoke(this, new CarEventManager("Sorry, this car is dead..."));
        }
        else
        {
            CurrentSpeed += delta;

            if (10 == (MaxSpeed - CurrentSpeed))
            {
                AboutToBlow?.Invoke(this, new CarEventManager("Carefull buddy! Gonna blow!"));
            }

            if (CurrentSpeed >= MaxSpeed)
            {
                carIsDead = true;
            }
            else
            {
                Console.WriteLine("Current Speed = {0}", CurrentSpeed);
            }

        }
    }
}

static void Main()
{
    Car car = new Car("Zhuk", 100, 10);

    // Подписываемся на события
    car.Exploted += Car_Exploted;
    car.AboutToBlow += Car_Exploted;

    for (int i = 0; i < 6; i++)
    {
        car.Accelerate(20);
    }
}

public static void Car_Exploted(object sender, CarEventManager e)
{
    // Можно взаимодействовать с объектом, приведя его к типу отправившим этот объект
    Car c = sender as Car;
    Console.WriteLine("{0} says: {1}", c.PetName, e.msg);
}
```

***

## Подписка на событие

```c#
// Определите класс для хранения пользовательской информации о событии
public class CustomEventArgs : EventArgs
{
    public string Message { get; set; }
    public CustomEventArgs(string message)
    {
        Message = message;
    }
}

// Класс, публикующий событие
class Publisher
{
    // Объявление события с помощью EventHandler <T>
    public event EventHandler<CustomEventArgs> RaiseCustomEvent;

    public void DoSomething()
    {
        // Напишите код, который делает здесь что-нибудь полезное 
        // затем вызываем событие. Вы также можете поднять событие 
        // перед выполнением блока кода.
        OnRaiseCustomEvent(new CustomEventArgs("Event triggered"));
    }

    protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
    {
        // Делаем временную копию события, чтобы избежать возможности 
        // состояние гонки, если последний подписчик отписался 
        // сразу после проверки на null и до возникновения события.
        EventHandler<CustomEventArgs> raiseEvent = RaiseCustomEvent;

        if (raiseEvent != null)
        {
            e.Message += $" ar {DateTime.Now}";
            // Вызов, чтобы инициировать событие.
            raiseEvent(this, e);
        }

        //RaiseCustomEvent?.Invoke(this, e);
    }
}

// Класс, который подписывается на событие
class Subscriber
{
    private readonly string _id;

    public Subscriber(string id, Publisher pub)
    {
        _id = id;
        // Подпишемся на событие
        pub.RaiseCustomEvent += HandleCustomEvent;
    }

    // Определяем, какие действия предпринимать при возникновении события.
    void HandleCustomEvent(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"{_id} received this message: {e.Message}");
    }
}

static void Main()
{
    var pub = new Publisher();
    var sub1 = new Subscriber("sub1", pub);
    var sub2 = new Subscriber("sub2", pub);

    // Call the method that raises the event.
    pub.DoSomething();
}
```
