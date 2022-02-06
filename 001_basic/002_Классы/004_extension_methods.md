# Методы расширения (Extension Methods)

Расширяющие методы могут расширить функциональность типа, не изменяя сам тип. Расширяющие методы могут быть только статическими и хранится в статических классах.

```c#
public static class MyExtensions
{    
    // Аргумент расширения может быть только один 
    // и стоит первым в списке аргументов (this string value)
    public static void FirstExtensionMethod(this string value)
    {
        Console.WriteLine(value);
    }    
}

// Применение:
string s = "Hello World!";
s.FirstExtensionMethod();
```

Extension методы могут содержать ref и out параметры.

```c#
public static class MyExtensions
{ 
    public static void AddOut(this int summand, out int summand2)
    {
        summand2 = 50;           
    }

    public static void AddRef(this int summand, ref int summand2)
    {
        summand2 += summand;
    }
}

// Применение:
int b;
int a = 10;
a.AddOut(out b);
Console.WriteLine(b);
```

Рекурсия в Extension методах.

```c#
public static class MyExtensions
{
    public static void Recoursion(this string value, string r, int counter)
    {
        counter--;
        Console.WriteLine(value + " " + r + " " + counter);
        if (counter > 0)            
            value.Recoursion(r, counter); 
    }
}

// Применение:
string val1 = "Hello";
string val2 = "Recoursion";
val1.Recoursion(val2, 5);
```
