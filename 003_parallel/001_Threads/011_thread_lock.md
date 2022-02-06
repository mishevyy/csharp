# Критическая секция (critical section)

lock - это сокращенное использование System.Threading.Monitor.
lock - блокирует блок кода так, что в каждый отдельный момент времени, этот блок кода сможет использовать только один поток. Все остальные потоки ждут пока текущий поток, закончит работу.

```c#
class MyClass
{
    private object block = new object();

    public void Method()
    {
        int hash = Thread.CurrentThread.GetHashCode();
    
        lock (block)
        {
            for (int counter = 0; counter < 10; counter++)
            {
                Console.WriteLine("Поток # {0}: шаг {1}", hash, counter);
                Thread.Sleep(200);
            }
        }    
        
        //Код который сгенерирует компилятор
        //Monitor.Enter(block);
        //for (int counter = 0; counter < 10; counter++)
        //{
        //    Console.WriteLine("Поток # {0}: шаг {1}", hash, counter);
        //    Thread.Sleep(100);
        //}
        //Console.WriteLine(new string('-', 20));    
        //Monitor.Exit(block);
    }

}
```



