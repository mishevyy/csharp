// https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/lambda-expressions#natural-type-for-lambda-expressions

// Естественный тип для лямбда выражении

var op = (string s) => Console.WriteLine(s);
op("asdasd");



// https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/record
// Добавлена возможность создавать рекорды на основе структур и структур доступных только для чтения
// Раньше можно было создавать записи только ссылочного типа record MyRecordS(double X, double Y);

record struct RecordStruct(double X, double Y);
readonly record struct ReadonlyRecordStruct(double X, double Y);



namespace str
{
    // https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/struct#parameterless-constructors-and-field-initializers
    readonly struct MyStruct
    {
        // Возможно объявить структуру с конструктором без параметров
        public MyStruct()
        {

        }
    }
    
    class Progrann
    {
        static void Main()
        {
            MyStruct mc = new MyStruct();
        }
    }

}



namespace w
{
    record MyRecord
    {
        public int Id { get; set; }
    }

    struct MyStruct
    {
        public double X { get; set; }
    }
    
    class Programm
    {
        static void Main()
        {
            MyRecord i1 = new MyRecord() { Id = 50 };
    
            // Выражение with создает копию своего операнда
            MyRecord i2 = i1 with { Id = 100 };
    
            // в c# 10 with стал доступен для структур
            MyStruct s1 = new MyStruct() { X = 2.0 };
            MyStruct s2 = s1 with { X = 3.0 };
        }
    }
}
