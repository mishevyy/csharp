# ConcurrentDictionary<TKey,TValue>

Представляет потокобезопасную коллекцию пар "ключ-значение", доступ к которой могут одновременно получать несколько потоков.

https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=net-5.0

```c#
private static void Main()
{
    ConcurrentDictionary<string, double> movieRating = new ConcurrentDictionary<string, double>();
    movieRating.TryAdd("Avengers: Endgame", 8.8);
    movieRating.TryAdd("The Departed", 8.5);
    movieRating.TryAdd("Santa Barbara", 2.5);
    movieRating.TryAdd("The Shawshank Redemption", 9.3);

    movieRating.TryAdd("The Dark Knight", 8.5);
    
    Console.WriteLine($"Количество фильмов в коллекции - {movieRating.Count}");
    EnumerateMovies(movieRating);
    
    movieRating["Fight Club"] = 8.8;
    
    double santaBarbaraValue = movieRating["Santa Barbara"];
    bool isSuccess = movieRating.TryUpdate("Santa Barbara", santaBarbaraValue * 2, santaBarbaraValue);
    
    if (isSuccess == true)
    {
        Console.WriteLine($"У сериала Santa Barbara рейтинг - {movieRating["Santa Barbara"]} \n");
    }
    
    isSuccess = movieRating.TryGetValue("The Dark Knight", out double theDarkKnightValue);
    
    if (isSuccess == true)
    {
        Console.WriteLine($"У фильма The Dark Knight рейтинг - {theDarkKnightValue} \n");
    }
    
    isSuccess = movieRating.TryRemove("Santa Barbara", out double _);
    Console.WriteLine($"Удаление успешно - {isSuccess}");
    Console.WriteLine($"Количество фильмов в коллекции после удаления - {movieRating.Count}");
    
    EnumerateMovies(movieRating);
}

private static void EnumerateMovies(ConcurrentDictionary<string, double> movieRating)
{
    Console.WriteLine("Список фильмов:");
    foreach (var movie in movieRating)
    {
        Console.WriteLine($"Фильм \"{movie.Key}\" имеет рейтинг - [{movie.Value}]");
    }

    Console.WriteLine(new string('-', 80));
}
```

