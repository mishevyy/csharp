// запросы ef core https://docs.microsoft.com/ru-ru/ef/core/querying/

/*---------------------------------------------Операции CRUD-------------------------------------------------*/
// CRUD - Create Read Update Delete (Создание Чтение Изменение Удаление)
class Person
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Email {get; set;}
    public int Age {get; set;}
}

class PersonRepository // Класс для управления опреациями CRUD
{
    private DemoContext _context;
    public PersonRepository(DemoContext context){
        _context = context;
    }

    public IEnumerable<Person> Persons { get => _context.Persons; }

    public Person GetPerson(int id)
    {
        return _context.Persons.Find(id);
    }

    public Person Create(Person person)
    {   
        // команда Add сохранит объект Person
        // и вернет в объект присвоенный Id
        _context.Add(person);
        _context.SaveChanges();
    }

    public Person Update(Person person)
    {
        // Способ 1 (при таком варианте создается команда Insert которая обновляет все поля
        // даже если они не изменялись)
        _context.Update(person);
        _context.SaveChanges();

        // Способ 2 (при таком варианте EF задействует механизм change trcker, который
        // отследит изменение в полученом обновление и создаст команду Insert для 
        // обновления только измененных полей, команду SaveChanges() в таком случае
        // выполнять ненужно)
        Person newPerson = GetPerson(person.Id);
        newPerson.Name = "Новое имя";

    }
    
    public void Delete(Person person)
    {
        // Команда Remove получит из person его Id и произведет удаление
        // поэтому в person достаточно передать только значение ID
        // Такой способ полностью удаляет объект из БД, что не всегда нужно
        _context.Remove(person);
        _context.SaveChanges();

        // Обычно у объекта определяют поле delete логического типа
        // и при удалении просто передают ему delete = true
    }
}