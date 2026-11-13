namespace MonitorAndSemaphoreSlim;

internal sealed class MonitorExample
{
    private static readonly object LockObject = new();

    public async Task RunAsync()
    {
        await Task.WhenAll(Enumerable.Range(1, 3).Select(_ => Task.Run(WorkWithLockAsync)));
    }

    private static async Task WorkWithLockAsync()
    {
        lock (LockObject)
        {
            Console.WriteLine($"lock: вошел поток {Environment.CurrentManagedThreadId}");
            Thread.Sleep(150);
            Console.WriteLine($"lock: вышел поток {Environment.CurrentManagedThreadId}");
        }

        await Task.Delay(50);
    }

    private static async Task InvalidAwaitInsideLockAsync()
    {
        lock (LockObject)
        {
            //await Task.Delay(100);
        }
    }
}
