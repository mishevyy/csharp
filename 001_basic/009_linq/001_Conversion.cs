using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq.LinqMethods
{
    internal class Conversion
    {
        public void ConverToArray()
        {
            byte[] array = new byte[10];
            new Random(42).NextBytes(array);

            var sortedArray = (from a in array
                               select a).ToArray();

            for (int i = 0; i < sortedArray.Length; i++)
                Console.Write(sortedArray[i] + " ");
        }

        public void ConverToList()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            var listWords = (from w in words
                             select w).ToList();

            foreach (var w in listWords)
            {
                Console.WriteLine(w);
            }
        }

        public void ConvertToDictionary()
        {
            var scoreRecords = new[]
            { 
                new {Name = "Alice", Score = 50},
                new {Name = "Bob"  , Score = 40},
                new {Name = "Cathy", Score = 45}
            };

            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);

            Console.WriteLine("Bob's score: {0}", scoreRecordsDict["Bob"]);
        }

        public void ConvertSelectedItems()
        {
            object[] numbers = { null, 1.0, "two", 3, "four", 5, "six", 7.0 };

            var doubles = numbers.OfType<double>();

            Console.WriteLine("Numbers stored as doubles:");
            foreach (var d in doubles)
            {
                Console.WriteLine(d);
            }
        }

    }
}
