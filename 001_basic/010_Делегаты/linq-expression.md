# linq expression

_Лямбда-выражение преобразуется в структуру данных представляющую операцию._

```c#
Expression<Func<int, double>> expression = x => x + 1;
```

_Компилируем выражение в делегат._

```c#
Func<int, double> func = expression.Compile();

for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Результат {0}", func(i));
}
```

_Формирование лямбдавыражения._

```c#
ParameterExpression n = Expression.Parameter(typeof(int), "n");
ParameterExpression t = Expression.Parameter(typeof(int), "t");

var const1 = Expression.Constant(1);
var const2 = Expression.Constant(2);

var addBody = Expression.Add(n, const1);
var multBody = Expression.Multiply(addBody, t);
var divBody = Expression.Divide(multBody, const2);

var expression = Expression.Lambda<Func<int, int, int>>(divBody, t, n);

Console.WriteLine(expression);

Func<int, int, int> func = expression.Compile();

for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Результат {0}", func(i, i));
}
```

```c#
class Program
{
    static void Main()
    {
        // MethodInfo
        MethodInfo consoleWriteLine = typeof(Console).GetMethod("WriteLine", new[] { typeof(String) });
        MethodInfo consoleReadline = typeof(Console).GetMethod("ReadLine");
        MethodInfo stringConcat = typeof(String).GetMethod("Concat", new[] { typeof(String), typeof(String) });
        MethodInfo converToInt = typeof(Convert).GetMethod("ToInt32", new[] { typeof(String) });
        MethodInfo converToString = typeof(Convert).GetMethod("ToString", new[] { typeof(Int32) });        

        // Constant and Parameters
        var enterA = Expression.Constant("Enter a:");
        var enterB = Expression.Constant("Enter b:");
        var theSumIs = Expression.Constant("The sum of a and b is: ");
        var exceptionMessage = Expression.Constant("Exception: ");
        var parametrA = Expression.Parameter(typeof(Int32), "a");
        var parametrB = Expression.Parameter(typeof(Int32), "b");
        var parametrResult = Expression.Parameter(typeof(Int32), "sum");
        var message = Expression.Parameter(typeof(String), "message");
        var exception = Expression.Parameter(typeof(Exception), "ex");
        var callReadLine = Expression.Call(consoleReadline);

        var bloc = Expression.Block(new[]
        {
                parametrA, parametrB, parametrResult, message
        },
        Expression.Call(consoleWriteLine, enterA),
        Expression.Assign(parametrA, Expression.Call(converToInt, callReadLine)),
        Expression.Call(consoleWriteLine, enterB),
        Expression.Assign(parametrB, Expression.Call(converToInt, callReadLine)),
        Expression.Assign(parametrResult, Expression.Add(parametrA, parametrB)),
        Expression.Assign(message, Expression.Call(stringConcat, theSumIs, Expression.Call(converToString, parametrResult))),
        Expression.Call(consoleWriteLine, message)
        );

        var catchBlock = Expression.Catch(exception,
            Expression.Call(consoleWriteLine,
                Expression.Call(stringConcat, exceptionMessage, Expression.Property(exception, "Message"))));

        var safeBlock = Expression.TryCatch(bloc, catchBlock);

        // Print out the Code!
        Console.WriteLine(safeBlock);
        foreach (var expression in bloc.Expressions)
        {
            Console.WriteLine("\t" + expression);
        }

        Console.WriteLine(safeBlock.Handlers[0]);
        Console.WriteLine("\t" + safeBlock.Handlers[0].Body);
        Console.WriteLine(new string('-', 40));


        Expression<Action> expressionCw = Expression.Lambda<Action>(safeBlock);
        var lambda = expressionCw.Compile();
        lambda.Invoke();
    }
}
```
