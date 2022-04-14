# События

[События](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/events/)

Событийно-ориентированное программирование (event-driven programming) — парадигма программирования, в которой выполнение программы определяется событиями — действиями пользователя (клавиатура, мышь, сообщениями других программ и потоков, событиями операционной системы (например, поступлением сетевого пакета).

**Событийно-ориентированное программирование, как правило, применяется в трех случаях:**

1. При построении пользовательских интерфейсов (в том числе графических)
2. При создании серверных приложений в случае, если по тем или иным причинам нежелательно порождение обслуживающих процессов
3. При программировании игр, в которых осуществляется управление множеством объектов

События позволяют классу или объекту уведомлять другие классы или объекты о возникновении каких-либо ситуаций. Класс, отправляющий (или вызывающий) событие, называется издателем.  Классы, принимающие (или обрабатывающие) событие, называются подписчиками.

**События имеют следующие свойства:**

- Издатель определяет момент вызова события, подписчики определяют предпринятое ответное действие.
- У события может быть несколько подписчиков. Подписчик может обрабатывать несколько событий от нескольких издателей.
- События, не имеющие подписчиков, никогда не возникают.
- Обычно события используются для оповещения о действиях пользователя, таких как нажатия кнопок или выбор меню и их пунктов в графическом пользовательском интерфейсе.
- Если событие имеет несколько подписчиков, то при его возникновении происходит синхронный вызов обработчиков событий.
- В библиотеке классов .NET Framework в основе событий лежит делегат EventHandler и базовый класс EventArgs.

**Сигнатура обработчика событий должна соответствовать следующим соглашениям:**

- Метод обработчик события принимает ровно два параметра.
- Первый параметр называется `sender` и имеет тип `Object`.  Это объект, вызвавший событие.
- Второй параметр называется - `e` и имеет тип `EventArgs` или тип производного класса от EventArgs. Это данные, специфичные для события.
- Тип возвращаемого значения метода обработчика  всегда `void`.

**Чтобы класс мог породить событие, необходимо подготовить три следующих элемента:**

- Класс, предоставляющий данные для события.
- Делегат события.
- Класс, порождающий событие.

События это особый тип многоадресных делегатов, которые можно вызвать только из класса или структуры, в которой они объявлены (класс издателя). Если на событие подписаны другие классы или структуры, их методы обработчиков событий будут вызваны, когда класс издателя инициирует событие.  Для объявления события в классе издателя применяется ключевое слово `event`

- События можно пометить как открытые public, private, protected, internal или protected internal.
- Событие можно объявить как статическое событие при помощи ключевого слова static. При этом событие становится доступным для вызова в любое время, даже если экземпляр класса отсутствует.
- Событие может быть помечено как виртуальное событие при помощи ключевого слова virtual. Это позволяет производным классам переопределять поведение события при помощи ключевого слова override.
- События могут быть абстрактными.
- Контекстно-зависимое ключевое слово add используется для определения пользовательского метода доступа к событию, вызываемому при подписке клиентского кода к событию. Если указан пользовательский метод доступа add, то необходимо также указать метод доступа remove. Контекстно-зависимое ключевое слово remove используется для определения пользовательского метода доступа к событию, вызываемому при отмене подписки клиентского кода от события.

## Простое использование событий

Создавать событие можно просто применив делегат, но для полноценных событий нужно использовать специальный тип делегата `EventHandler` и базовый класс `EventArgs` который содержит данные события

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
    instance.MyEvent += new EventDelegate(Handler); // Подписка на событие полная форма  

    instance.InvokeEvent();

    Console.WriteLine(new string('-', 50));

    instance.MyEvent -= Handler2; // Открепляем обрапотчик события.

    instance.InvokeEvent();
}

static private void Handler()
{
    Console.WriteLine("Обработчик события 1");
}
```

## Простое событие с использование EventHandler

```c#
public class SimpleEventHandler
{
    public event EventHandler MyEvent;

    public void Foo()
    {
        MyEvent?.Invoke(this, EventArgs.Empty);
    }
}
```

## Дженерик событие Используем делегат `EventHandler<T>`

```c#
// 1. Наследуемся от EventArgs, для определения собственного типа события
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

    // 2. Создание событий типа EventHandler<> и закрываем собственным типом
    public event EventHandler<CarEventManager> Exploted;
    public event EventHandler<CarEventManager> AboutToBlow;

    // 3. Метод в котором может возникнуть событие
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

## Паттерн подписчик издатель

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

## Паттерн слежение за изменением состояния объекта

```c#
class Test : INotifyPropertyChanged
{
    // Определяем поля за которым будем наблюдать
    private int prop;
    public int Property
    {
        get { return prop; }
        set 
        {
            if(prop != value) // Если значение поля изменяется вызываем событие
            {
                prop = value;
                PropertyChange();
            }                
        }
    }

    private void PropertyChange([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;       
}
```

