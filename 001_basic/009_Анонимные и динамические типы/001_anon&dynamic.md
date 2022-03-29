# Динамические типы

В отличие от других встроенных типов языка C# (например, string, int, object и т.п.), dynamic не имеет прямого сопоставления ни с одним из базовых типов BCL. Вместо этого, dynamic – специальный псевдоним для System.Object с дополнительными метаданными, необходимыми для правильного позднего связывания.

```c#
// Код

dynamic d = 100;
d++;

// Будет преобразован

object d = 100;
    object arg = d;
    if (Program.<dynamicMethod>o__SiteContainerd.<>p__Sitee == null)
    {
        Program.<dynamicMethod>o__SiteContainerd.<>p__Sitee = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Increment, typeof(Program), new CSharpArgumentInfo[]
        {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
        }));
    }
    d = Program.<dynamicMethod>o__SiteContainerd.<>p__Sitee.Target(Program.<dynamicMethod>o__SiteContainerd.<>p__Sitee, arg);
```

Сначала среда выполнения решает с объектом какого типа мы имеем дело (COM, POCO).

Далее в дело вступает компилятор. Так как необходимость в лексере и парсере отсутствует, DLR использует специальную версию компилятора C#, имеющего только анализатор метаданных, семантический анализатор выражений, а также генератор кода, который вместо IL генерирует Expression Trees.

Анализатор метаданных использует рефлексию, чтобы установить тип объекта, который потом передается семантическому анализатору для установления возможности вызова метода или выполнения операции. Далее происходит построение Expression Tree, как если бы Вы использовали лямбда-выражение.

Компилятор C# возвращает обратно дерево выражений в DLR вместе с политикой кэширования. DLR потом сохраняет данный делегат в кэше, ассоциирующимся с узлом вызовов.
