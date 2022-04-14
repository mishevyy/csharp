# Коллекции

Не рекомендуется использовать коллекции из пространства имен System.Collections.
Вместо этого рекомендуется использовать дженерик коллекции из пространства имен System.Collections.Generic.

Список соответствия старых и обновленных коллекций

[Справка по дженерик коллекциям](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic?view=net-5.0)

[Справка по коллекциям](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic?view=net-5.0)

***

## Наиболее популярные коллекции

`List<T>` [List](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.list-1?view=net-5.0) Наиболее часто используемая коллекция
Представляет строго типизированный список объектов, доступных по индексу.
Поддерживает методы для поиска по списку, выполнения сортировки и других операций со списками.

`Dictionary<TKey,TValue>` [Dictionary](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.dictionary-2?view=net-5.0)
Представляет коллекцию ключей и значений.

***

## Другие часто используемы коллекции

`SortedList<TKey,TValue>`
Представляет коллекцию пар "ключ-значение", упорядоченных по ключу на основе реализации `IComparer<T>`.
[SortedList](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.sortedlist-2)

`LinkedList<T>`
Представляет двунаправленный список.
[LinkedList](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.linkedlist-1)

`Queue<T>`
Представляет коллекцию объектов, основанную на принципе «первым поступил — первым обслужен».
[Queue](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.queue-1)

`Stack<T>`
Представляет коллекцию переменного размера экземпляров одинакового заданного типа, обслуживаемую по принципу "последним пришел - первым вышел" (LIFO).
[Stack](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.generic.stack-1)

`BitArray`
Управляет компактным массивом двоичных значений, представленных логическими значениями,  где значение true соответствует включенному биту (1), а значение false соответствует отключенному биту (0).
[BitArray](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.bitarray)

***

## Наблюдаемые коллекции

`ObservableCollection<T>`
Представляет динамическую коллекцию данных, которая выдает уведомления при добавлении и удалении элементов, а также при обновлении списка.
https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.objectmodel.observablecollection-1

## Потокобезопасные коллекции

ConcurrentDictionary<TKey,TValue>

Представляет потокобезопасную коллекцию пар "ключ-значение", доступ к которой могут одновременно получать несколько потоков.
[ConcurrentDictionary](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=net-5.0)
[Concurrent](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.concurrent?view=net-5.0)
