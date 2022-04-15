# Рефлексия

РЕФЛЕКСИЯ - это процесс обнаружения типов и информации о типе во время работы программы.

## Type

Способы получения информации о классе.

```c#
Type type;
type = myClass.GetType();
type = Type.GetType("reflection1.Class1");// Полное квалифицированое имя типа в строковом представлении.
type = typeof(Class1);
```

```c#
public interface IInterface1
{
    void MethodA();
}
public interface IInterface2
{
    void MethodB();
}

public class Class1 : IInterface1, IInterface2
{
    public int myint;
    private string mystring = "Hello";

    public Class1() { }
    public Class1(int i)
    {
        this.myint = i;
    }

    public int myProp
    {
        get { return myint; }
        set { myint = value; }
    }

    public string MyString
    {
        get { return mystring; }
    }

    public void MethodA() { }
    public void MethodB() { }

    private void MethodC(string str, string str2)
    {
        Console.WriteLine(str + str2);
    }
    public void myMethod(int p1, string p2) { }
}
   
static void Main(string[] args)
{
    Class1 myClass = new Class1();

    #region Вывод информации о типе
    ListVariosStats(myClass); // Получаем разную информацию о Class1.
    ListMethods(myClass); // Получаем информацию об Именах всех методов Class1.
    ListFields(myClass); // Получаем информацию об Именах всех полей Class1.
    ListProps(myClass); // Получаем список всех Свойств Class1.
    ListInterfaces(myClass); // Получаем список всех Интерфейсов, поддерживаемых Class1.
    ListConstructors(myClass); // Получаем информацию об Именах всех конструкторов Class1.
    #endregion

    #region Обращение к закрытым членам (Инкапсуляция... Что такое инкапсуляция? :)
    Console.WriteLine(new string('-', 60));
    Type type = myClass.GetType();

    // Вызов private метода
    MethodInfo methodC = type.GetMethod("MethodC", BindingFlags.Instance | BindingFlags.NonPublic);
    methodC.Invoke(myClass, new object[] { "Hello", " world!" });

    // Запись значения в private поле
    FieldInfo mystring = type.GetField("mystring", BindingFlags.Instance | BindingFlags.NonPublic);
    mystring.SetValue(myClass, "Привет Мир!");

    Console.WriteLine(myClass.MyString);
    #endregion
}

// Получаем разную информацию о Class1.
static void ListVariosStats(Class1 cl)
{
    Console.WriteLine(new string('_', 30) + " Информация о Class1" + "\n");
    Type t = cl.GetType();

    Console.WriteLine("Полное Имя:             {0}", t.FullName);
    Console.WriteLine("Базовый класс:          {0}", t.BaseType);
    Console.WriteLine("Абстрактный:            {0}", t.IsAbstract);
    Console.WriteLine("Это COM объект:         {0}", t.IsCOMObject);
    Console.WriteLine("Запрещено наследование: {0}", t.IsSealed);
    Console.WriteLine("Это class:              {0}", t.IsClass);
}

// Получаем информацию об Именах всех методов Class1.
static void ListMethods(Class1 cl)
{
    Console.WriteLine(new string('_', 30) + " Методы класса Class1" + "\n");

    Type t = cl.GetType();
    MethodInfo[] mi = t.GetMethods(BindingFlags.Instance
            | BindingFlags.Static
            | BindingFlags.Public
            | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

    foreach (MethodInfo m in mi)
        Console.WriteLine("Method: {0}", m.Name);
}

// Получаем информацию об Именах полей Class1.
static void ListFields(Class1 cl)
{
    Console.WriteLine(new string('_', 30) + " Поля класса Class1" + "\n");

    Type t = cl.GetType();
    FieldInfo[] fi =
        t.GetFields(BindingFlags.Instance
            | BindingFlags.Static
            | BindingFlags.Public
            | BindingFlags.NonPublic);

    foreach (FieldInfo f in fi)
        Console.WriteLine("Field: {0}", f.Name);
}

// Получаем список всех Свойств Class1.
static void ListProps(Class1 cl)
{
    Console.WriteLine(new string('_', 30) + " Свойства класса Class1" + "\n");

    Type t = cl.GetType();
    PropertyInfo[] pi = t.GetProperties();

    foreach (PropertyInfo p in pi)
        Console.WriteLine("Свойство: {0}", p.Name);
}

// Получаем список всех Интерфейсов, поддерживаемых Class1.
static void ListInterfaces(Class1 cl)
{
    Console.WriteLine(new string('_', 30) + " Интерфейсы класса Class1" + "\n");

    Type t = cl.GetType();
    Type[] it = t.GetInterfaces();

    foreach (Type i in it)
        Console.WriteLine("Интерфейс: {0}", i.Name);
}

// Получаем информацию обо всех конструкторах Class1.
static void ListConstructors(Class1 cl)
{
    Console.WriteLine(new string('_', 30) + " Конструкторы класса Class1" + "\n");

    Type t = cl.GetType();
    ConstructorInfo[] ci = t.GetConstructors();

    foreach (ConstructorInfo m in ci)
        Console.WriteLine("Constructor: {0}", m.Name);
}
```

## TypeInfo

```c#
var personType = typeof(Person);
TypeInfo personInfo = personType.GetTypeInfo();

IEnumerable<PropertyInfo> declaredProperties = personInfo.DeclaredProperties;
declaredProperties.PrintValues();

IEnumerable<MethodInfo> declaredMethods = personInfo.DeclaredMethods;
declaredMethods.PrintValues();

IEnumerable<EventInfo> declaredEvents = personInfo.DeclaredEvents;
declaredEvents.PrintValues();
```

## AssemblyInfo

```c#

static void Main(string[] args)
{
    // При помощи класса Assembly - можно динамически загружать сборки, 
    // обращаться к членам класса в процессе выполнения (ПОЗДНЕЕ СВЯЗЫВАНИЕ),
    // а так же получать информацию о самой сборке.
    Assembly assembly = null;

    try
    {
        // Assembly.Load() - метод для загрузки сборки.
        assembly = Assembly.Load("TestLibrary");
        Console.WriteLine("Сборка TestLibrary - успешно загружена.");
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine(ex.Message);
    }

    // Выводим информацию о всех типах в сборке.
    ListAllTypes(assembly);
    // Выводим информацию о всех членах в классе.
    ListAllMembers(assembly);
    // Выводим информацию о всех параметрах метода.
    GetParams(assembly);
}

// Метод для получения информации о всех типах в сборке.
private static void ListAllTypes(Assembly assembly)
{
    Console.WriteLine(new string('_', 80));
    Console.WriteLine("\nТипы в: {0} \n", assembly.FullName);

    Type[] types = assembly.GetTypes();

    foreach (Type t in types)
        Console.WriteLine("Тип: {0}", t);
}

// Метод для получения информации о членах класса.
private static void ListAllMembers(Assembly assembly)
{
    Console.WriteLine(new string('_', 80));

    Type type = assembly.GetType("TestLibrary.MiniVan");

    Console.WriteLine("\nЧлены класса: {0} \n", type);

    MemberInfo[] members = type.GetMembers();

    foreach (MemberInfo element in members)
        Console.WriteLine("{0,-15}:  {1}", element.MemberType, element);
}

// Получаем информацию о параметрах для метода TurboBoost() 
private static void GetParams(Assembly assembly)
{
    Console.WriteLine(new string('_', 80));

    Type type = assembly.GetType("TestLibrary.MiniVan");
    MethodInfo method = type.GetMethod("Driver"); // Equals , Acceleration, Driver

    // Вывод информации о количестве параметров.
    Console.WriteLine("\nИнформация о параметрах для метода {0}", method.Name);
    ParameterInfo[] parameters = method.GetParameters();
    Console.WriteLine("Метод имеет " + parameters.Length + " параметров");

    // Выводим некоторые характеристики каждого из параметров. 
    foreach (ParameterInfo parameter in parameters)
    {
        Console.WriteLine("Имя параметра: {0}", parameter.Name);
        Console.WriteLine("Позиция в методе: {0}", parameter.Position);
        Console.WriteLine("Тип параметра: {0}", parameter.ParameterType);
    }
}
```

## Late - binding

```c#
// Создаем объект выбранного нами типа "на лету".
// Позднее связывание - это технология которая позволяет обнаруживать типы,
// определять их имена и члены непосредственно в процессе выполнения.
// Раннее связывание - все указанные выше операции выполняются во время компиляции.   
static void Main()
{
    Assembly assembly = null;

    try
    {
        assembly = Assembly.Load("TestLibrary");
        Type type = assembly.GetType("TestLibrary.MiniVan");
        MiniVan carInstance = Activator.CreateInstance(type) as MiniVan;

        if (carInstance != null)
        {
            carInstance.Acceleration();
            carInstance.Driver("Shumaher", 26);
        }

    }
    catch (FileNotFoundException e)
    {
        Console.WriteLine(e.Message);
    }
}    

internal class MiniVan
{
    public void Acceleration() { }
    public void Driver(string s, int i) { }
}
```

```c#
static void Main()
{
    //Type type = Type.GetType("System.Collections.Generic.List`1"); 
    // для получения информацции о дженериках нужно использовать такой синтаксис `кол-во параметров
    Type type = typeof(Console);
    ListMethods(type);
    ListFields(type);
    ListProperties(type);
    ListInterfaces(type);
    ListVariousStats(type);
}

// Рефлексия методов
static void ListMethods(Type t)
{
    Console.WriteLine("***** Methods *****");
    MethodInfo[] mi = t.GetMethods();
    foreach (MethodInfo m in mi)
    {
        string retVal = m.ReturnType.FullName;
        string paramInfo = "( ";

        foreach (ParameterInfo pi in m.GetParameters())
        {
            paramInfo += string.Format("{0} {1}", pi.ParameterType, pi.Name);
        }
        paramInfo += " )";

        Console.WriteLine($"->{retVal} {m.Name} {paramInfo}");
    }
}

// Рефлексия полей
static void ListFields(Type t)
{
    Console.WriteLine("***** Fields *****");
    FieldInfo[] fi = t.GetFields();
    foreach (FieldInfo f in fi)
    {
        Console.WriteLine($"->{f.Name}");
    }
}

// Рефлексия свойств
static void ListProperties(Type t)
{
    Console.WriteLine("***** Properties *****");
    PropertyInfo[] pi = t.GetProperties();
    foreach (PropertyInfo p in pi)
    {
        Console.WriteLine($"->{p.Name}");
    }
}

// Рефлексия реализованных интерфейсов
static void ListInterfaces(Type t)
{
    Console.WriteLine("***** Interfaces *****");
    Type[] ii = t.GetInterfaces();
    foreach (Type i in ii)
    {
        Console.WriteLine($"->{i.Name}");
    }
}

// Отображение дополнительных деталей
static void ListVariousStats(Type t)
{
    Console.WriteLine("***** Various Statistics *****");
    Console.WriteLine($"Base class is: {t.BaseType}");
    Console.WriteLine($"Is type abstract: {t.IsAbstract}");
    Console.WriteLine($"Is type sealed: {t.IsSealed}");
    Console.WriteLine($"Is type generic: {t.IsGenericTypeDefinition}");
    Console.WriteLine($"Is type a class type: {t.IsClass}");
}
```
