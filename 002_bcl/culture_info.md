# Класс CultureInfo

[CultureInfo](https://docs.microsoft.com/ru-ru/dotnet/api/system.globalization.cultureinfo?view=net-5.0)

*Получение информации о текущей культуре.*

```c#
CultureInfo currentCulture = CultureInfo.CurrentCulture;
```

Информация о всех культурах доступных в системе.

```c#
CultureInfo[] cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures);
```

```c#
double money = 122343.45;
var american = new CultureInfo("en-US");
var germany = new CultureInfo("de-DE");
var russian = new CultureInfo("ru-RU");

money.ToString("C", american);
money.ToString("C", germany);
money.ToString("C", russian);
```

## Использование интерфейса IFormattable

```c#
public class Temperature : IFormattable
{
    private decimal temperature;

    public Temperature(decimal temperature)
    {
        if (temperature < -273.15m) // -273.15m - Абсолютный ноль!
        {
            throw new ArgumentOutOfRangeException(String.Format("{0} is less than absolute zero.", temperature));
        }
        this.temperature = temperature;
    }
    
    public decimal Celsius
    {
        get { return temperature; }
    }
    
    public decimal Fahrenheit
    {
        // Перевод Цельсия в Фарингейт.
        get { return temperature * 9 / 5 + 32; }
    }
    
    public decimal Kelvin
    {
        // Перевод Цельсия в Кельвин.
        get { return temperature + 273.15m; }
    }
    
    public override string ToString()
    {
        return this.ToString("G", CultureInfo.CurrentCulture);
    }
    
    public string ToString(string format)
    {
        return this.ToString(format, CultureInfo.CurrentCulture);
    }
    
    // Реализация IFormattable.
    public string ToString(string format, IFormatProvider provider)
    {
        if (String.IsNullOrEmpty(format))
            format = "G";
    
        if (provider == null)
            provider = CultureInfo.CurrentCulture;
    
        switch (format.ToUpperInvariant())
        {
            case "G":
            case "C":
                return temperature.ToString("F2", provider) + " °C"; // F2 - формат вывода вещественного числа (2 знака после запятой).
            case "F":
                return Fahrenheit.ToString("F2", provider) + " °F";
            case "K":
                return Kelvin.ToString("F2", provider) + " K";
            default:
                throw new FormatException(String.Format("The {0} format string is not supported.", format));
        }
    }

}
```

```c#
var temperature = new Temperature(12); // 12°C
Console.WriteLine("Temperature [default]     = {0}", temperature);
Console.WriteLine("Temperature [Fahrenheit]  = {0:F}", temperature);
Console.WriteLine("Temperature [CultureInfo] = {0}", temperature.ToString("F", CultureInfo.CreateSpecificCulture("en-US")));
Console.WriteLine("Temperature [CultureInfo] = {0}", temperature.ToString("C", CultureInfo.CreateSpecificCulture("ru-RU")));
```
