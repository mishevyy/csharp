/*--------------------------------------Управление связями таблиц--------------------------------------------*/
/*-----------------------------------------Связи один к одному-----------------------------------------------*/
public class OneToOneSource
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public int ReceiverId { get; set; }
    public virtual OneToOneReceiver Receiver { get; set; }
}
public class OneToOneReceiver
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public int SourceId { get; set; }
    public virtual OneToOneSource Source { get; set; }
}

/*-----------------------------------------Связь один ко многим-----------------------------------------------*/
// Самый распространенный тип связи
public class OneToManySource
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public int ReciverId { get; set; }
    public virtual OneToManyReciver Reciver { get; set; }
}
public class OneToManyReciver
{
    public OneToManyReciver()
    {
        Source = new HashSet<OneToManySource>();
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<OneToManySource> Source { get; set; }
}

/*-----------------------------------------Связь многие ко многим-----------------------------------------------*/
// EF должен создать промежуточную таблицу для связей, но также ее можно определить и самому
// тогда это будет такая таблица которая с двух сторон имеет связи многие ко многим
public class ManyToManySource
{
    public ManyToManySource()
    {
        Reciver = new HashSet<ManyToManyReciver>();
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<ManyToManyReciver> Reciver { get; set; }
}

public class ManyToManyReciver
{
    public ManyToManyReciver()
    {
        Source = new HashSet<ManyToManySource>();
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<ManyToManySource> Source { get; set; }
}