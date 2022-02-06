/*------------Выполнени sql через EF------------------------*/
// Выполнение sql в ef core
using (var context = new SampleContext())
{
    using (var command = context.Database.GetDbConnection().CreateCommand())
    {
        command.CommandText = "SELECT * From Table1";
        context.Database.OpenConnection();
        using (var result = command.ExecuteReader())
        {
            // do something with result
        }
    }
}