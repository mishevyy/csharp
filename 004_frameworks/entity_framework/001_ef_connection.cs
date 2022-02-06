/*-------------------------------------------Entity Framework------------------------------------------------*/
// ef-core https://docs.microsoft.com/ru-ru/ef/core/
// Entity Framework - представляет собой пакет объектно-реляционного отображения (ORM) данных
// Он позволяет хранить/создавать и обращаться к сущностям БД как с объектами

/*---------------------------------------------Nuget Пакеты-----------------------------------------------*/
// https://docs.microsoft.com/ru-ru/ef/core/what-is-new/nuget-packages
// Microsoft.EntityFrameworkCore - основной пакет EF
// Microsoft.EntityFrameworkCore.SqlServer - пакет поставщика БД
// Microsoft.EntityFrameworkCore.Tools - иструменты миграции которые работает в диспетчере пакетов
// Microsoft.EntityFrameworkCore.Design - инструменнты для миграции из коммандной строки (dotnet-ef)

/*--------------------------------------Настройка подключений------------------------------------------*/
// https://docs.microsoft.com/ru-ru/ef/core/dbcontext-configuration/
// Поставщики бд https://docs.microsoft.com/ru-ru/ef/core/providers/?tabs=dotnet-core-cli
// Строки подключения https://docs.microsoft.com/ru-ru/ef/core/miscellaneous/connection-strings

// Для настройки подключения нужно создать класс контекста унаследованный от DbContext
// и передать в конструкторе опции через объект DbContextOptions<>
public class DemoContext : DbContext
{
    public DemoContext(DbContextOptions<DemoContext> options)
        : base(options) { }

    // для определение таблиц используется объект DbSet
    public DbSet<Person> Persons { get; set; }
}

// сам класс DbContext хранит методы для работы с БД такие как (получение. удаление изменеие и тд)
// и методы OnConfiguring(DbContextOptionsBuilder optionsBuilder) - нужен для конфигурирования подулючения
// и OnModelCreating(ModelBuilder modelBuilder) в котором можно можно применять различные настройки для
// конфигурирования таблиц БД с помощью FluentAPI

// Настройка контекста подключений во внедрение зависимостей
public void ConfigureServices(IServiceCollection services)
{
    // добавление контекста
    services.AddDbContext<Essen_chicagoContext>(options =>
    {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    });
}

// добавление строки подключения в фаиле конфигурации
"ConnectionStrings": {
    "DefaultConnection": "Server=servdb1;Database=Essen_chicago;Trusted_Connection=True;"
}