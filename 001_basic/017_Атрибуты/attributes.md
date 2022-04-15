# Атрибуты

Атрибуты служат для определения дополнительных метаданных или дополнительной информации об объекте к которым применяется аттрибут.

Что бы создать собственный атрибут необходимо унаследоваться от класса Attribute, и определить открытый конструктор. Также по спецификации именования необходимо добавить к имени аттрибута суффикс Attribut.

Что бы использовать атрибут, необходимо написать код с использованием рефлексии в котором будет определена логика взаимодействия с аттрибутами

Что бы указать для чего используется аттрибут нужно к настраиваему атрибуту применить атрибут AttributeUsage

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
