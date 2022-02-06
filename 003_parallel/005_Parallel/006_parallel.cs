using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ParallelProgramming
{
    internal class Employee
    {
        public Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }

        public string Name { get; set; }
        public decimal Salary { get; set; }

        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee("Иванов Рома", 1000),
                new Employee("Петров Рома", 1000),
                new Employee("Ворнов Рома", 1000),
                new Employee("Ястреб Рома", 1000),
                new Employee("Коргов Рома", 1000),

                new Employee("Иванов Олег", 2000),
                new Employee("Петров Олег", 2000),
                new Employee("Ворнов Олег", 2000),
                new Employee("Ястреб Олег", 2000),
                new Employee("Коргов Олег", 2000),

                new Employee("Иванов Петр", 2000),
                new Employee("Петров Петр", 2000),
                new Employee("Ворнов Петр", 2000),
                new Employee("Ястреб Петр", 2000),
                new Employee("Коргов Петр", 2000),

                new Employee("Иванов Женя", 5000),
                new Employee("Петров Женя", 5000),
                new Employee("Ворнов Женя", 5000),
                new Employee("Ястреб Женя", 5000),
                new Employee("Коргов Женя", 5000),

                new Employee("Иванов Дима", 10000),
                new Employee("Петров Дима", 10000),
                new Employee("Ворнов Дима", 10000),
                new Employee("Ястреб Дима", 10000),
                new Employee("Коргов Дима", 10000),
            };
        }
    }

    internal class Program
    {
        private static void Main()
        {
            List<Employee> employees = Employee.GetEmployees();

            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Func<int> localInit = () => new int();

            Func<Employee, ParallelLoopState, int, int> loopBody = (employee, state, localValue) =>
            {
                if (state.ShouldExitCurrentIteration == true)
                    return localValue;

                Console.WriteLine($"[Поток #{Thread.CurrentThread.ManagedThreadId}] Сотрудник {employee.Name} получил зп - ${employee.Salary:N}");
                return localValue + (int)employee.Salary;
            };

            int companyPayments = 0;
            Action<int> localFinally = (localValue) =>
            {
                Interlocked.Add(ref companyPayments, localValue);
            };

            Parallel.ForEach(employees, parallelOptions, localInit, loopBody, localFinally);
            Console.WriteLine($"Компания тратит на зарплаты сотрудникам - ${companyPayments:N}");

            Console.ReadKey();
        }


    }
}
