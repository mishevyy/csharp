# Пример создания простого неирона
public class Neuron
{
    private decimal weight = 0.5m; // вес неирона

    public decimal LastError { get; private set; } // проверочное значение
    public decimal Smooth { get; set; } = 0.00001m; // коэфф сглаживания

    public decimal ProcessInputData(decimal input)
    {
        return input * weight;
    }

    public decimal RestoreInputData(decimal output)
    {
        return output * weight;
    }

    public void Train(decimal input, decimal expectedResult)
    {
        // поиск актуального значения, такого что
        // при expectedResult - actualResult значение будет равно (+-0)
        var actualResult = input * weight;
        LastError = expectedResult - actualResult;

        // корректировка веса
        var correction = (LastError / actualResult) * Smooth;
        weight += correction;
    }
}

class Program
{
    static void Main()
    {
        decimal km = 100;
        decimal miles = 62.1371m;

        Neuron neuron = new Neuron();

        int i = 0;
        do
        {
            i++;
            // Тренировка неирона, вес неирона в данном примере 
            // должен стать близко равен к 62.1371;
            neuron.Train(km, miles);
        }
        while (neuron.LastError > neuron.Smooth || neuron.LastError < -neuron.Smooth);

        Console.WriteLine("Обучение завершено");
        Console.WriteLine($"В {100} км {neuron.ProcessInputData(100)} миль");
    }
}
