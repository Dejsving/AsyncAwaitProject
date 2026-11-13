namespace AwaitOrNot;

internal static class Program
{
    private static async Task Main()
    {
        Console.WriteLine("Негативный пример: await пропущен, работа стартует в фоне");
        DoWorkAsync("без await");
        Console.WriteLine("Эта строка выводится раньше завершения работы");
        await Task.Delay(800);

        Console.WriteLine();
        Console.WriteLine("Положительный пример: вызывающий код дожидается результата");
        await DoWorkAsync("с await");
        Console.WriteLine("Продолжаем после завершения работы");

        Console.WriteLine();
        Console.WriteLine("Параллельность намеренная: обе задачи наблюдаются через WhenAll");
        await Task.WhenAll(DoWorkAsync("параллельная задача 1"), DoWorkAsync("параллельная задача 2"));
    }

    private static async Task DoWorkAsync(string name)
    {
        Console.WriteLine($"Старт: {name}");
        await Task.Delay(500);
        Console.WriteLine($"Финиш: {name}");
    }
}
