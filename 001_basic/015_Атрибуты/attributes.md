# Атрибуты

Атрибуты нужны для определения дополнительных свойств объектов

Что бы создать собственный атрибут необходимо унаследоваться от класса Attribute

Что бы использовать атрибут, нужно написать код с использованием рефлексии

Что бы указать для чего используется аттрибут нужно к атрибуту применить атрибут AttributeUsage

```c#
[AttributeUsage(AttributeTargets.Class)]
public class MyAttribute : Attribute
{
    public string Name { get; set; }
}

[type: My(Name = "Hello")]
class MyClass{}

static void Main()
{
    // Выявление настраиваемых атрибутов
    Type type = typeof(MyClass);
    var attr = type.GetCustomAttribute(typeof(MyAttribute));
                
    Console.WriteLine(attr);
}
```
