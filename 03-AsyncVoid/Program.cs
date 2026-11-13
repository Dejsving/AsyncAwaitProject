namespace AsyncVoid;

internal static class Program
{
    private static async Task Main()
    {
        // это демонстрация того, что async void выбрасывает исключение, которое нельзя поймать try/catch в вызывающем коде
        // закоментируйте код и получите падение приложения при возникновении исключения в async void
        AppDomain.CurrentDomain.UnhandledException += (_, eventArgs) =>
        {
            Console.WriteLine($"Исключение не обработано приложением: {eventArgs.ExceptionObject}");
        };

        Console.WriteLine("Вызов async void внутри try/catch");
        try
        {
            FailLater();
            Console.WriteLine("Вызов вернулся; try/catch уже завершился");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Этот catch не поймает исключение после await: {exception.Message}");
        }

        await Task.Delay(Timeout.Infinite);
    }

    private static async void FailLater()
    {
        await Task.Delay(300);
        throw new InvalidOperationException("Искусственная ошибка из async void");
    }
}
