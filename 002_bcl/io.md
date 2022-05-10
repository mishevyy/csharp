# Операции ввода вывода

## Основные классы для работы с фаилами и директориями

* File предоставляет статические методы для создания, копирования, удаления, перемещения и открытия файлов, а также помогает создать объект FileStream.

* FileInfo предоставляет методы экземпляра для создания, копирования, удаления, перемещения и открытия файлов, а также помогает создать объект FileStream.

* Directory предоставляет статические методы для создания, перемещения и перечисления в каталогах и подкаталогах.

* DirectoryInfo предоставляет методы экземпляра для создания, перемещения и перечисления в каталогах и подкаталогах.

* Path предоставляет методы и свойства для обработки строк каталогов межплатформенным способом.

### Потоки

Абстрактный базовый класс Stream поддерживает чтение и запись байтов. Все классы, представляющие потоки, являются производными от класса Stream.

FileStream — для чтения и записи в файл.

IsolatedStorageFileStream — для чтения и записи в файл в изолированном хранилище.

MemoryStream — для чтения и записи в память в качестве резервного хранилища.

BufferedStream — для повышения быстродействия операций чтения и записи.

NetworkStream — для чтения и записи на сетевые сокеты.

PipeStream — для чтения и записи в анонимные и именованные каналы.

CryptoStream — для связи потоков данных с криптографическими преобразованиями.

### Средства чтения и записи

Пространство имен System.IO также предоставляет типы для чтения закодированных символов из потоков и их записи в потоки. 
Как правило, потоки предназначены для ввода и вывода байтов. Типы чтения и записи обрабатывают преобразование закодированных символов 
в байты или из байтов, чтобы поток мог завершить операцию. 
Каждый класс чтения и записи связан с потоком, который можно получить с помощью свойства класса BaseStream. 

BinaryReader и BinaryWriter — для чтения и записи простых типов данных, таких как двоичные значения.

StreamReader и StreamWriter — для чтения и записи символов с использованием закодированного значения для преобразования символов в байты или из байтов.

StringReader и StringWriter — для чтения и записи символов в строки или из строк.

TextReader и TextWriter используются в качестве абстрактных базовых классов для других средств чтения и записи, которые считывают и записывают символы и строки, а не двоичные данные.

### Сжатие данных

Сжатием называется процесс сокращения размера сохраняемого файла. Распаковка — это процесс извлечения содержимого сжатого файла, что приводит его в формат, пригодный для использования. 
Пространство имен System.IO.Compression содержит типы для сжатия и распаковки файлов и потоков.

ZipArchive — для создания и восстановления содержимого ZIP-архива.

ZipArchiveEntry — для представления сжатого файла.

ZipFile — для создания, извлечения и открытия сжатого пакета.

ZipFileExtensions — для создания и извлечения содержимого из сжатого пакета.

DeflateStream — для сжатия и распаковки потоков с помощью алгоритма Deflate.

GZipStream — для сжатия и распаковки потоков в формате gzip.

###  Изолированное хранилище

Изолированное хранилище — это механизм хранения данных, обеспечивающий изоляцию и безопасность путем определения стандартизованных способов 
сопоставления кода с хранимыми данными. Хранилище предоставляет виртуальную файловую систему, изолированную по пользователю, 
сборке и (необязательно) домену. Изолированное хранилище особенно полезно в том случае, когда приложение не имеет разрешения на доступ к файлам пользователя. 
Можно сохранить параметры или файлы для приложения таким способом, который контролируется политикой безопасности компьютера.

IsolatedStorage предоставляет базовый класс для реализации изолированного хранилища.

IsolatedStorageFile предоставляет область изолированного хранилища, в которой содержатся файлы и каталоги.

IsolatedStorageFileStream представляет файл в изолированном хранилище.

### Представление путей

Путь	Описание:
C:\Documents\Newsletters\Summer2018.pdf	Абсолютный путь к файлу из корня диска C:.
\Program Files\Custom Utilities\StringFinder.exe	Абсолютный путь из корня текущего диска.
2018\January.xlsx	Относительный путь к файлу в подкаталоге текущего каталога.
..\Publications\TravelBrochure.pdf	Относительный путь к файлу в каталоге, начиная с текущего каталога.
C:\Projects\apilibrary\apilibrary.sln	Абсолютный путь к файлу из корня диска C:.
C:Projects\apilibrary\apilibrary.sln	Относительный путь из текущего каталога диска C:.

Сетевые UNC - пути

`\\system07\C$\`	Корневой каталог диска C: на компьютере system07

`\\Server2\Share\Test\Foo.txt`	Файл Foo.txt в тестовом каталоге тома


```c#
// Копирование содержимого коталога
static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
{
    var dir = new DirectoryInfo(sourceDir);

    if (!dir.Exists)
        throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

    DirectoryInfo[] dirs = dir.GetDirectories();

    Directory.CreateDirectory(destinationDir);

    foreach (FileInfo file in dir.GetFiles())
    {
        string targetFilePath = Path.Combine(destinationDir, file.Name);
        file.CopyTo(targetFilePath);
    }

    if (recursive)
    {
        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, true);
        }
    }
}
```

```c#
// Перечисление каталогов
static void EnumerateDir()
{
    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    DirectoryInfo dir = new DirectoryInfo(path);

    foreach (var d in dir.EnumerateDirectories())
    {

    }

    foreach (var d in dir.EnumerateFiles())
    {

    }

    // или с использованием статических методов            

    foreach (var d in Directory.EnumerateDirectories(path))
    {

    }

    foreach (var d in Directory.EnumerateFiles(path))
    {

    }
}
```

```c#
// считывание и запись файла
static void BinaryWriteRead(string path)
{
    using (FileStream fs = new FileStream(path, FileMode.CreateNew))
    {
        using (BinaryWriter wr = new BinaryWriter(fs))
        {
            wr.Write("Hello, World");
        }
    }

    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
    {
        using (BinaryReader br = new BinaryReader(fs))
        {
            br.Read();
        }
    }
}
```

```c#
 // считывание и запись файла
static void StreamWriterT(string path)
{
    using (StreamWriter sw = new StreamWriter(path))
    {
        sw.WriteLine("Hello, World!");
    }

    try
    {
        // Open the text file using a stream reader.
        using (var sr = new StreamReader(path))
        {
            // Read the stream as a string, and write the string to the console.
            Console.WriteLine(sr.ReadToEnd());
        }
    }
    catch (IOException e)
    {
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
    }
}
```

```c#
// Запись текста при помощи класса File
static void FileT()
{
    // Create a string with a line of text.
    string text = "First line" + Environment.NewLine;

    // Set a variable to the Documents path.
    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    // Write the text to a new file named "WriteFile.txt".
    File.WriteAllText(Path.Combine(docPath, "WriteFile.txt"), text);

    // Create a string array with the additional lines of text
    string[] lines = { "New line 1", "New line 2" };

    // Append new lines of text to the file
    File.AppendAllLines(Path.Combine(docPath, "WriteFile.txt"), lines);
}
```

```c#
static void StringReaderT()
{
    string str = "Some number of characters";
    char[] b = new char[str.Length];

    using (StringReader sr = new StringReader(str))
    {
        // Read 13 characters from the string into the array.
        sr.Read(b, 0, 13);
        Console.WriteLine(b);

        // Read the rest of the string starting at the current string position.
        // Put in the array starting at the 6th array member.
        sr.Read(b, 5, str.Length - 13);
        Console.WriteLine(b);
    }


    StringBuilder sb = new StringBuilder("Start with a string and add from ");
    char[] bz = { 'c', 'h', 'a', 'r', '.', ' ', 'B', 'u', 't', ' ', 'n', 'o', 't', ' ', 'a', 'l', 'l' };

    using (StringWriter sw = new StringWriter(sb))
    {
        // Write five characters from the array into the StringBuilder.
        sw.Write(bz, 0, 5);
        Console.WriteLine(sb);
    }
}
```

```c#
// Следим за дерикторией
class Program
{
    static void Main()
    {
        DirectoryLookup();
    }

    static void DirectoryLookup()
    {
        string path = @"E:\temp";

        using var watcher = new FileSystemWatcher(path);

        watcher.NotifyFilter = NotifyFilters.Attributes
            | NotifyFilters.CreationTime
            | NotifyFilters.DirectoryName
            | NotifyFilters.FileName
            | NotifyFilters.LastAccess
            | NotifyFilters.LastWrite
            | NotifyFilters.Security
            | NotifyFilters.Size;

        watcher.Changed += OnChanged;
        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.Renamed += OnRenamed;
        watcher.Error += OnError;

        watcher.Filter = "*.txt";
        watcher.IncludeSubdirectories = true;
        watcher.EnableRaisingEvents = true;

        Console.WriteLine("!!!!!!!!!!!!!!!");
        Console.ReadLine();
    }

    private static void OnChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Changed)
            return;

        Console.WriteLine($"Changed: {e.FullPath}");
    }

    private static void OnCreated(object sender, FileSystemEventArgs e)
    {
        string value = $"Created: {e.FullPath}";
        Console.WriteLine(value);
    }

    private static void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Deleted: {e.FullPath}");
    }

    private static void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"Renamed:");
        Console.WriteLine($"    Old: {e.OldFullPath}");
        Console.WriteLine($"    New: {e.FullPath}");
    }

    private static void OnError(object sender, ErrorEventArgs e)
        => PrintException(e.GetException());

    private static void PrintException(Exception? ex)
    {
        if (ex != null)
        {
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine("Stacktrace:");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine();
            PrintException(ex.InnerException);
        }
    }
}
```