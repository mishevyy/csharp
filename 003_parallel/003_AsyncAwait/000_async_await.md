# Асинхронные задачи

Необходимо помнить одну важную подробность: в процессе выполнения устройством операции ввода-вывода поток исполнения, передавший запрос, простаивает, поэтому Windows переводит его в спящее состояние, чтобы не расходовать процессорное время впустую (6). Однако при этом поток продолжает занимать место в памяти своим стеком пользовательского режима, стеком режима ядра, блоком переменных окружения потока (Thread Environment Block, TEB) и другими структурами данных, которые в этот момент не используются. Приложения с графическим интерфейсом перестают реагировать на действия пользователя на время блокировки потока. Все это, конечно, нежелательно.

Представим реализацию веб-приложения, в которой для каждого пришедшего на ваш сервер клиентского запроса формируется запрос к базе данных. При поступлении клиентского запроса поток из пула потоков обращается к вашему коду. При выдаче синхронного запроса к базе данных этот поток окажется заблокированным на неопределенное время, необходимое для получения ответа из базы. Если в это время придет еще один клиентский запрос, пул создаст еще один поток, который снова окажется заблокированным. В итоге можно оказаться с целым набором блокированных потоков, ожидающих ответа из базы данных. Получается, что веб-сервер выделяет массу ресурсов (потоков и памяти для них), которые почти не используются!

Может возникнуть вопрос, когда и каким образом обрабатываются считываемые данные? При вызове ReadAsync возвращается объект `Task<Int32>`. Используя этот объект, можно вызвать метод ContinueWith для регистрации метода обратного вызова, который должен выполняться при завершении задачи, а затем обработать данные в методе обратного вызова. Также можно использовать асинхронные функции C#, позволяющие использовать последовательную структуру кода (как при выполнении синхронного ввода-вывода)

Теперь, разобравшись с основами, посмотрим на открывающиеся перед нами перспективы. Предположим, в ответ на клиентский запрос сервер выдает асинхронный запрос к базе данных. При этом наш поток не блокируется, а возвращается в пул, получая возможность заняться обработкой других клиентских запросов. Таким образом, получается, что для обработки всех входящих запросов достаточно всего одного потока. Полученный от базы данных ответ также окажется в очереди пула потоков, то есть наш поток сможет тут же его обработать и отправить данные клиенту. Таким образом, единственный поток обрабатывает не только клиентские запросы, но и все ответы базы данных. В итоге сервер практически не потребляет системных ресурсов, но работает с максимально возможной скоростью, так как переключения контекста не происходит!Если элементы появляются в пуле быстрее, чем поток может их обработать, пул может создать дополнительные потоки. Пул быстро создаст по одному потоку на каждый процессор. Соответственно, на машине с четырьмя процессорами четыре клиентских запроса к базе данных и ответа базы данных (в любой комбинации) будут обрабатываться в четырех потоках без какого-либо переключения контекста

Для реализации описанного поведения CLR-пул потоков использует такой ресурс Windows, как порт завершения ввода-вывода (I/O Completion Port). Он создается при инициализации CLR. Затем с этим портом можно связать подсоединяемые устройства, чтобы в результате их драйверы «знали», куда поставить в очередь IRP-пакет. Подробнее этот механизм описан в моей книге «Windows via C/C++» (Microsoft Press, 2007). Асинхронный ввод-вывод кроме минимального использования ресурсов и уменьшения количества переключений контекста предоставляет и другие преимущества. Скажем, в начале сборки мусора CLR приостанавливает все потоки в процессе. Получается, чем меньше у нас потоков, тем быстрее произойдет уборка мусора. Кроме того, при уборке мусора CLR просматривает в поисках корней все стеки потоков. Соответственно, чем меньше у нас потоков, тем меньше стеков приходится просматривать и тем быстрее работает уборщик мусора. Плюс ко всему, если в процессе обработки потоки не были заблокированы, большую часть времени они будут проводить в пуле в режиме ожидания. А значит, в начале уборки мусора потоки окажутся наверху стека, и поиск корней не займет много времени.

## Асинхронные функции

Асинхронные операции являются ключом к созданию высокопроизводительных масштабируемых приложений, выполняющих множество операций при помощи небольшого количества потоков. Вместе с пулом потоков они дают возможность эффективно задействовать все процессоры в системе.

## Преобразование асинхронной функции

```c#
internal sealed class Type1 { } 
internal sealed class Type2 { } 
private static async Task<Type1> Method1Async() { 
 /* Асинхронная операция, возвращающая объект Type1 */ 
} 
private static async Task<Type2> Method2Async() { 
 /* Асинхронная операция, возвращающая объект Type2 */ 
}
Теперь я приведу асинхронную функцию, которая использует эти простые типы 
и методы.
private static async Task<String> MyMethodAsync(Int32 argument) { 
 Int32 local = argument; 
 try { 
 Type1 result1 = await Method1Async(); 
 for (Int32 x = 0; x < 3; x++) { 
 Type2 result2 = await Method2Async(); 
 } 
 } 
 catch (Exception) { 
 Console.WriteLine("Catch"); 
 } 
 finally { 
 Console.WriteLine("Finally"); 
 } 
 return "Done"; 
}


// Атрибут AsyncStateMachine обозначает асинхронный метод
// (полезно для инструментов, использующих отражение); 
// тип указывает, какая структура реализует конечный автомат.
[DebuggerStepThrough, AsyncStateMachine(typeof(StateMachine))]
private static Task<String> MyMethodAsync(Int32 argument) { 
 // Создание экземпляра конечного автомата и его инициализация
 StateMachine stateMachine = new StateMachine() { 
 // Создание построителя, возвращающего Task<String>.
 // Конечный автомат обращается к построителю для назначения
 // завершения задания или выдачи исключения.
 m_builder = AsyncTaskMethodBuilder<String>.Create(), 
 m_state = ­1, // инициализация местонахождения
 m_argument = argument // Копирование аргументов в поля конечного
 }; // автомата
 // Начало выполнения конечного автомата.
 stateMachine.m_builder.Start(ref stateMachine); 
 return stateMachine.m_builder.Task; // Возвращение задания конечного
} // автомата
// Структура конечного автомата
[CompilerGenerated, StructLayout(LayoutKind.Auto)]
private struct StateMachine : IAsyncStateMachine { 
 // Поля для построителя конечного автомата (Task) и его местонахождения
 public AsyncTaskMethodBuilder<String> m_builder; 
 public Int32 m_state; 
 // Аргумент и локальные переменные становятся полями:
 public Int32 m_argument, m_local, m_x;
 public Type1 m_resultType1; 
 public Type2 m_resultType2; 
 // Одно поле на каждый тип Awaiter.
 // В любой момент времени важно только одно из этих полей. В нем
 // хранится ссылка на последний выполненный экземпляр await,
 // который завершается асинхронно:
 private TaskAwaiter<Type1> m_awaiterType1; 
 private TaskAwaiter<Type2> m_awaiterType2; 
 // Сам конечный автомат
 void IAsyncStateMachine.MoveNext() { 
 String result = null; // Результат Task
 // Вставленный компилятором блок try гарантирует
 // завершение задания конечного автомата
 try { 
 Boolean executeFinally = true; // Логический выход из блока 'try'
 if (m_state == ­1) { // Если метод конечного автомата
 // выполняется впервые
 m_local = m_argument; // Выполнить начало исходного метода
 } 
 // Блок try из исходного кода
 try { 
 TaskAwaiter<Type1> awaiterType1; 
 TaskAwaiter<Type2> awaiterType2; 
 switch (m_state) { 
 case ­1: // Начало исполнения кода в 'try'
 // вызвать Method1Async и получить его объект ожидания
 awaiterType1 = Method1Async().GetAwaiter(); 
 if (!awaiterType1.IsCompleted) { 
 m_state = 0; // 'Method1Async'
 // завершается асинхронно
 m_awaiterType1 = awaiterType1; // Сохранить объект
 // ожидания до возвращения
 // Приказать объекту ожидания вызвать MoveNext
 // после завершения операции
 m_builder.AwaitUnsafeOnCompleted(ref awaiterType1, ref this);
 // Предыдущая строка вызывает метод OnCompleted
 // объекта awaiterType1, что приводит к вызову
 // ContinueWith(t => MoveNext()) для Task.
 // При завершении Task ContinueWith вызывает MoveNext
 executeFinally = false; // Без логического выхода
 // из блока 'try' 
 return; // Поток возвращает
 } // управление вызывающей стороне
 // 'Method1Async' завершается синхронно.
 break; 
 case 0: // 'Method1Async' завершается асинхронно
 awaiterType1 = m_awaiterType1; // Восстановление последнего
 break; // объекта ожидания
 case 1: // 'Method2Async' завершается асинхронно
 awaiterType2 = m_awaiterType2; // Восстановление последнего
 goto ForLoopEpilog; // объекта ожидания
 } 
 // После первого await сохраняем результат и запускаем цикл 'for'
 m_resultType1 = awaiterType1.GetResult(); // Получение результата
 ForLoopPrologue: 
 m_x = 0; // Инициализация цикла 'for'
 goto ForLoopBody; // Переход к телу цикла 'for'
 ForLoopEpilog: 
 m_resultType2 = awaiterType2.GetResult(); 
 m_x++; // Увеличение x после каждой итерации
 // Переход к телу цикла 'for'
 ForLoopBody: 
 if (m_x < 3) { // Условие цикла 'for'
 // Вызов Method2Async и получение объекта ожидания
 awaiterType2 = Method2Async().GetAwaiter(); 
 if (!awaiterType2.IsCompleted) { 
 m_state = 1; // 'Method2Async' завершается
 // асинхронно
 m_awaiterType2 = awaiterType2; // Сохранение объекта
 // ожидания до возвращения
 // Приказываем вызвать MoveNext при завершении операции
 m_builder.AwaitUnsafeOnCompleted(ref awaiterType2, ref this);
 executeFinally = false; // Без логического выхода
 // из блока 'try'
 return; // Поток возвращает управление
 } // вызывающей стороне
 // 'Method2Async' завершается синхронно
 goto ForLoopEpilog; // Синхронное завершение, возврат
 } 
 } 
 catch (Exception) { 
 Console.WriteLine("Catch"); 
 } 
 finally { 
 // Каждый раз, когда блок физически выходит из 'try',
 // выполняется 'finally'.
 // Этот код должен выполняться только при логическом
 // выходе из 'try'.
 if (executeFinally) { 
 Console.WriteLine("Finally"); 
 } 
 } 
 result = "Done"; // То, что в конечном итоге должна вернуть
 } // асинхронная функция.
 catch (Exception exception) { 
 // Необработанное исключение: задание конечного автомата
 // завершается с исключением.
 m_builder.SetException(exception); 
 return; 
 } 
 // Исключения нет: задание конечного автомата завершается с результатом
  m_builder.SetResult(result); 
 } 
}
```

Пожалуй, стоит особо упомянуть об одном важном моменте. Каждый раз, когда в вашем коде используется оператор await, компилятор берет указанный операнд и пытается вызвать для него метод GetAwaiter. Этот метод может быть как экземплярным методом, так и методом расширения. Объект, возвращаемый при вызове GetAwaiter, называется объектом ожидания (awaiter).

Что касается расширяемости, если в объект Task можно упаковать операцию, которая завершится в будущем, то вы сможете использовать оператор await для ожидания завершения этой операции.

### Класс TaskLogger, который может использоваться для вывода информации о незавершенных асинхронных операциях

```c#
public static class TaskLogger 
{ 
    public enum TaskLogLevel { None, Pending }
    public static TaskLogLevel LogLevel { get; set; } 
    public sealed class TaskLogEntry { 
    public Task Task { get; internal set; }
    public String Tag { get; internal set; } 
    public DateTime LogTime { get; internal set; } 
    public String CallerMemberName { get; internal set; } 
    public String CallerFilePath { get; internal set; }
    public Int32 CallerLineNumber { get; internal set; }

    public override string ToString() 
    {
        return String.Format("LogTime={0}, Tag={1}, Member={2}, File={3}({4})",
            LogTime, Tag ?? "(none)", CallerMemberName, CallerFilePath,
            CallerLineNumber);
    }
}
 
 private static readonly ConcurrentDictionary<Task, TaskLogEntry> s_log = 
    new ConcurrentDictionary<Task, TaskLogEntry>();

 public static IEnumerable<TaskLogEntry> GetLogEntries() { return s_log.Values; }
 
 public static Task<TResult> Log<TResult>(this Task<TResult> task,
    String tag = null,
    [CallerMemberName] String callerMemberName = null,
    [CallerFilePath] String callerFilePath = null,
    [CallerLineNumber] Int32 callerLineNumber = 1) { 
    return (Task<TResult>) 
    Log((Task)task, tag, callerMemberName, callerFilePath, callerLineNumber);
    }

    public static Task Log(this Task task, String tag = null,
    [CallerMemberName] String callerMemberName = null,
    [CallerFilePath] String callerFilePath = null,
    [CallerLineNumber] Int32 callerLineNumber = 1)
    { 

    if (LogLevel == TaskLogLevel.None) return task; 

        var logEntry = new TaskLogEntry 
        { 
            Task = task,
            LogTime = DateTime.Now,
            Tag = tag,
            CallerMemberName = callerMemberName,
            CallerFilePath = callerFilePath,
            CallerLineNumber = callerLineNumber 
        }; 

    s_log[task] = logEntry; 

    task.ContinueWith(t => { TaskLogEntry entry; 
        s_log.TryRemove(t, out entry); },
        TaskContinuationOptions.ExecuteSynchronously); 

        return task; 
    } 
}

// Использование

public static async Task Go() { 
    #if DEBUG 
    // Использование TaskLogger приводит к лишним затратам памяти
    // и снижению производительности; включить для отладочной версии
    TaskLogger.LogLevel = TaskLogger.TaskLogLevel.Pending; 
    #endif 
    // Запускаем 3 задачи; для тестирования TaskLogger их продолжительность
    // задается явно.
    var tasks = new List<Task> { 
    Task.Delay(2000).Log("2s op"),
    Task.Delay(5000).Log("5s op"),
    Task.Delay(6000).Log("6s op") 
    }; 
    try { 
    // Ожидание всех задач с отменой через 3 секунды; только одна задача
    // должна завершиться в указанное время.
    // Примечание: WithCancellation - мой метод расширения,
    // описанный позднее в этой главе.
    await Task.WhenAll(tasks). 
    WithCancellation(new CancellationTokenSource(3000).Token); 
    } 
    catch (OperationCanceledException) { } 
    // Запрос информации о незавершенных задачах и их сортировка
    // по убыванию продолжительности ожидания
    foreach (var op in TaskLogger.GetLogEntries().OrderBy(tle => tle.LogTime)) 
    Console.WriteLine(op); 
}

```

## Обработка ошибок

объекты Task обычно инициируют исключение AggregateException, а для получения информации о реальных исключениях следует обратиться к свойству InnerExceptions этого исключения. Однако при использовании await с Task вместо AggregateException выдается первое внутреннее исключение. Это было сделано для того, чтобы поведение кода соответствовало ожиданиям разработчика. Кроме того, без этого вам пришлось бы перехватывать AggregateException в вашем коде, проверять внутреннее исключение и либо перехватывать его, либо выдавать заново. От этого код становится слишком громоздким