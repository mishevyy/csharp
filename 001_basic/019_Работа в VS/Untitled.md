## Элементы воплощающие выражения

В C# версии 6 появились элементы, воплощающие выражение, для функций-членов и свойств, доступных только для чтения. В C# 7.0 расширен список допустимых членов, которые могут быть реализованы как выражения. В C# 7.0 можно реализовать _конструкторы_, _методы завершения_, а также методы доступа `get` и `set` для _свойств_ и _индексаторов_.

```c#
// Выражение в конструкторе
public ExpressionMembersExample(string label) => this.Label = label;

private string label;

// Выражения в свойствах
public string Label
{
    get => label;
    set => this.label = value ?? "Default label";
}

// throw
public string Name
{
    get => name;
    set => name = value ??
        throw new ArgumentNullException(paramName: nameof(value), message: "Name cannot be null");
}

```