namespace linq.LinqMethods
{
    public class Generators
    {
        public void RangeOfInteger()
        {
            int[] numbers = Enumerable.Range(0, 10).ToArray();
            Array.ForEach(numbers, item => Console.WriteLine(item));
        }

        public void RepeatNumber()
        {
            int[] numbers = Enumerable.Repeat(7, 10).ToArray();
            Array.ForEach(numbers, item => Console.WriteLine(item));
        }
    }
}
