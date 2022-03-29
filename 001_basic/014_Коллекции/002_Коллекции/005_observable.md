# `ObservableCollection<T>`

Представляет динамическую коллекцию данных, которая выдает уведомления при добавлении и удалении элементов, а также при обновлении списка.
[ObservableCollection](https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.objectmodel.observablecollection-1)

```c#
class User
{
    public string Name { get; set; }
}

static void ObservableTest()
{
    ObservableCollection<User> users = new ObservableCollection<User>
    {
        new User { Name = "Bill"},
        new User { Name = "Tom"},
        new User { Name = "Alice"},
        new User { Name = "Adam"}
    };

    users.CollectionChanged += Users_CollectionChanged;
    
    users.Add(new User { Name = "Bob" });
    users.RemoveAt(1);
    users[0] = new User { Name = "Anders" };
    
    foreach (User user in users)
    {
        Console.WriteLine(user.Name);
    }
}

private static void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
{
    switch (e.Action)
    {
        case NotifyCollectionChangedAction.Add: // если добавление
            User newUser = e.NewItems[0] as User;
            Console.WriteLine($"Добавлен новый объект: {newUser.Name}");
            break;
        case NotifyCollectionChangedAction.Remove: // если удаление
            User oldUser = e.OldItems[0] as User;
            Console.WriteLine($"Удален объект: {oldUser.Name}");
            break;
        case NotifyCollectionChangedAction.Replace: // если замена
            User replacedUser = e.OldItems[0] as User;
            User replacingUser = e.NewItems[0] as User;
            Console.WriteLine($"Объект {replacedUser.Name} заменен объектом {replacingUser.Name}");
            break;
    }
}
```
