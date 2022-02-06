// Моделирование с помощью DataAnnotation
// System.ComponentModel.DataAnnotations - пространство имен DataAnotation

// Служит для более точной конфигурации таблиц
[Table("table_name")]
public class DataAnotationDemoModel
{
    [Key]
    [Column("id_table")]
    public int Id { get; set; }

    [Required, StringLength(25), Column("test_string")]
    public string Name { get; set; }

    [Column(TypeName = "money")]
    public decimal Value { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime Date { get; set; }
    
    public bool IsValid { get; set; }
}