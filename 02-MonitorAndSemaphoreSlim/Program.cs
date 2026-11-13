namespace MonitorAndSemaphoreSlim;

internal static class Program
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private static async Task Main()
    {
        Console.WriteLine("lock (Monitor): синхронная критическая секция");
        await new MonitorExample().RunAsync();

        Console.WriteLine();
        Console.WriteLine("SemaphoreSlim: ожидание асинхронное, внутри допустим await");
        await Task.WhenAll(Enumerable.Range(1, 3).Select(number => WorkWithSemaphoreAsync(number)));
    }

    private static async Task WorkWithSemaphoreAsync(int number)
    {
        await Semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"SemaphoreSlim: задача {number} вошла");
            await Task.Delay(150);
            Console.WriteLine($"SemaphoreSlim: задача {number} вышла");
        }
        finally
        {
            Semaphore.Release();
        }
    }
}
