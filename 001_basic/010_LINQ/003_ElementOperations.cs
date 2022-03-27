namespace linq.LinqMethods
{
    internal class ElementOperations
    {
        public void FirstElement()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            string f1 = strings.First();

            // В случае не найденого элемента сгененрирует ошибку
            string f2 = strings.First(f => f.StartsWith("ree"));

            /****************************************************************/

            string fod = strings.FirstOrDefault();

            // В случае не найденого элемента вернет значение по умолчанию для типа
            string fod2 = strings.FirstOrDefault(f => f.StartsWith("lg"));

            /****************************************************************/

            // Возвращение элемента по индексу
            string ela = strings.ElementAt(0);
            string ela2 = strings.ElementAtOrDefault(0);
        }

        public void Quatifiers()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            // Все элементы последовательности удовлетворяют условию
            bool isAll = strings.All(a => a.StartsWith("f"));

            // Хотя бы один элмент удовлетворяет условию
            bool isAny = strings.Any(a => a.StartsWith("f"));

            // Вернут true если последовательность содержит элементы
            bool isAny2 = strings.Any();
        }

        public void Restricted()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            string[] str = strings.Where(w => w.StartsWith("z")).ToArray();

        }

        public void Ordering()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            string[] ordered = strings.OrderBy(w => w).ToArray();
            string[] orderedDesc = strings.OrderByDescending(w => w).ToArray();
        }

        public void SequenceOperations()
        {
            int[] vectorA = { 0, 2, 4, 5, 6 };
            int[] vectorB = { 1, 3, 5, 7, 8 };

            // Сложение
            int dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
            Console.WriteLine($"Dot product: {dotProduct}");

            // Склеивание
            var allNumbers = vectorA.Concat(vectorB);

            // Сравнение
            bool match = vectorA.SequenceEqual(vectorB);

            //
            var intersectNumbers = vectorA.Intersect(vectorB);
            var exceptNumbers = vectorA.Except(vectorB);

            //
            var dist = exceptNumbers.Distinct();

            //
            var union = vectorA.Union(vectorB);

            // Разбивает элементы последовательности на блоки размером не более
            var chunked = vectorA.Concat(vectorB).Chunk(3);
        }

        public void AgregateOperation()
        {
            int[] vectorA = { 0, 2, 4, 5, 6 };

            int sum = vectorA.Sum();
            int min = vectorA.Min();
            int max = vectorA.Max();
            int count = vectorA.Count();
            double average = vectorA.Average(); //Среднее

            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            // ПРименяет к последовательности агрегатную функцию
            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);
        }

        public void Projections()
        {
            IEnumerable<Customer> customers = CustomerData.CustomerList;

            var proectionSiquence = customers.Select(s => new { s.CustomerID, s.Address });

            foreach (var item in proectionSiquence)
            {
                Console.WriteLine(item.CustomerID + " " + item.Address);
            }
        }

        public void Partition()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int[] taked = numbers.Take(3).ToArray();
            int[] skiped = numbers.Skip(5).ToArray();
            int[] takeWhile = numbers.TakeWhile(t => t % 2 == 0).ToArray();
            int[] skipWhile = numbers.SkipWhile(t => t % 2 == 0).ToArray();
        }

        public void Group()
        {
            IEnumerable<Product> products = ProductData.ProductList;

            var query = products
                .GroupBy(g => g.ProductName)
                .Select(s => new { Product = s.Key, SumProd = s.Sum(f => f.UnitPrice) });

            foreach (var q in query)
            {
                Console.WriteLine(q.Product + " " + q.SumProd);
            }
        }
    }
}
