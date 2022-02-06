// Моделирование с помощью Fluent Api
// Fluent Api служит для более тонкой конфигурации таблиц, чем DataAnotation
public class FluentApiDemo
{       
    public int Id { get; set; }       
    public string Name { get; set; }       
    public decimal Value { get; set; }        
    public DateTime Date { get; set; }
    public bool IsValid { get; set; }
}

// Определение свойство применяется в методете OnModelCreating класса контекста Entity Framework
protected override void OnModelCreating(DbModelBuilder modelBuilder) // переопределенный метод из класса контекста
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<FluentApiDemo>()
        .ToTable("fluent_table");

    modelBuilder.Entity<FluentApiDemo>()
        .HasKey(k => k.Id);

    modelBuilder.Entity<FluentApiDemo>()
        .Property(p => p.Name)
        .HasMaxLength(30)
        .IsRequired()
        .HasColumnName("name_of_fluent");

    modelBuilder.Entity<FluentApiDemo>()
        .Property(p => p.Value)
        .HasPrecision(14, 4);

    modelBuilder.Entity<FluentApiDemo>()
        .Property(p => p.Date)
        .HasColumnType("datetime2");      
}