
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