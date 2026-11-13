namespace DelegatesAndEvents;

internal delegate void TemperatureHandler(double temperature);
internal delegate void TemperatureAuditHandler(string sensor, double temperature);

internal sealed class Thermometer
{
    public event TemperatureHandler? TemperatureChanged;
    public event TemperatureAuditHandler? TemperatureAudited;

    public void Report(string sensor, double temperature)
    {
        TemperatureChanged?.Invoke(temperature);
        TemperatureAudited?.Invoke(sensor, temperature);
    }
}

internal static class Program
{
    private static void Main()
    {
        var thermometer = new Thermometer();
        TemperatureHandler display = temperature => Console.WriteLine($"Дисплей: {temperature:F1} °C");
        TemperatureHandler warning = temperature => Console.WriteLine($"Предупреждение: {temperature:F1} °C");
        TemperatureAuditHandler audit = (sensor, temperature) =>
            Console.WriteLine($"Журнал [{sensor}]: {temperature:F1} °C");

        thermometer.TemperatureChanged += display;
        thermometer.TemperatureChanged += warning;
        thermometer.TemperatureAudited += audit;

        Console.WriteLine("Оба обработчика подписаны через +=");
        thermometer.Report("кухня", 24.5);

        thermometer.TemperatureChanged -= warning;
        thermometer.TemperatureAudited -= audit;

        Console.WriteLine();
        Console.WriteLine("warning и audit отписаны через -=; display остался");
        thermometer.Report("кухня", 25.0);
    }
}
