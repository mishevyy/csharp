```c#
using System.Text.Json;

User user = new()
{
    Id = 1,
    Name = "Mike",
    Birth = new DateTime(1991, 07, 16),
    IsMarried = false
};

JsonSerializerOptions options = new()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string json = JsonSerializer.Serialize<User>(user, options);

Console.WriteLine(json);

User u2 = JsonSerializer.Deserialize<User>(json);
Console.WriteLine(u2.Name);


// Сериализация в массив байтов

byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(user);

User u3 = JsonSerializer.Deserialize<User>(jsonUtf8Bytes);
Console.WriteLine(u3.Name);




class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public bool IsMarried { get; set; }
}
```