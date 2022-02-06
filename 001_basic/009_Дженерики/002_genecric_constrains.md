# Ограничения параметров типа

_where T : class  -   Аргумент типа должен иметь ссылочный тип, это также распространяется на тип любого класса, интерфейса, делегата или массива._

```c#
class MyClassA<T> where T : class
{
    public T variable;
}
```

_where T : struct  -  Аргумент типа должен иметь тип значения. Допускается указание любого типа значения, кроме Nullable._

```c#
class MyClassB<T> where T : struct
{
    public T variable;
}
```

_where T : new()  -  Аргумент типа должен иметь открытый конструктор без параметров._
При использовании с другими ограничениями ограничение new() должно устанавливаться последним:

```c#
class MyClass<T> where T : class, new()   { /* ... */ }
class MyClassC<T> where T : new()
{
    public T instance = new T();
}
```

_where T : Base -  Аргумент типа должен являться или быть производным от указанного базового класса._

```c#
class Base { /* ... */ }
class MyClassD<T> where T : Base { /* ... */ }
```

_where T : IInterface - Аргумент типа должен являться или реализовывать указанный интерфейс._

```c#
interface IInterface { /* ... */ }
class Derived : IInterface { /* ... */ }
class MyClassD<T> where T : IInterface { /* ... */ }
```

_Можно установить несколько ограничений интерфейса. Ограничивающий интерфейс также может быть универсальным._

```c#
class MyClassE<T> where T : IInterface, IInterface<object> { /* ... */ }
```

_Аргумент типа, предоставляемый в качестве T, должен совпадать с аргументом, предоставляемым в качестве U, или быть производным от него._
Это называется ограничением типа "Naked".

```c#
class MyClassF<T, R, U> where T : U { /* ... */ }
```
