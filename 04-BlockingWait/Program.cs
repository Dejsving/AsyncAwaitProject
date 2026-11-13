using System.Diagnostics;

namespace BlockingWait;

internal static class Program
{
    private static async Task Main()
    {
        // таймер в отдельном потоке, что бы видеть, что основной поток в начале блокировался, потом освобождался после await
        using var timer = new Timer(
            _ => Console.WriteLine($"  heartbeat ({DateTime.Now:HH:mm:ss.fff})"),
            null,
            TimeSpan.Zero,
            TimeSpan.FromMilliseconds(250));

        Console.WriteLine("Task.Delay(1500).Result: текущий поток ждет синхронно");
        var stopwatch = Stopwatch.StartNew();
        var result = GetTextAsync().Result;
        Console.WriteLine($"Результат: {result}; поток был заблокирован {stopwatch.ElapsedMilliseconds} мс");

        Console.WriteLine();
        Console.WriteLine("await Task.Delay(1500): метод приостанавливается, поток не занят ожиданием");
        stopwatch.Restart();
        result = await GetTextAsync();
        Console.WriteLine($"Результат: {result}; прошло {stopwatch.ElapsedMilliseconds} мс");
    }

    private static async Task<string> GetTextAsync()
    {
        await Task.Delay(1500);
        return "готово";
    }
}
