using System.Reflection;
using System.Runtime.CompilerServices;

namespace AsyncAwait;

internal static class Program
{
    private static async Task Main()
    {
        Console.WriteLine("До вызова async-метода");
        var result = await GetNumberAsync();
        Console.WriteLine($"После await: {result}");

        ShowGeneratedStateMachine(nameof(GetNumberAsync));
    }

    private static async Task<int> GetNumberAsync()
    {
        await Task.Delay(500);
        return 42;
    }

    private static void ShowGeneratedStateMachine(string methodName)
    {
        var method = typeof(Program).GetMethod(
            methodName,
            BindingFlags.NonPublic | BindingFlags.Static)!;
        var attribute = method.GetCustomAttribute<AsyncStateMachineAttribute>()!;
        var stateMachineType = attribute.StateMachineType;

        Console.WriteLine();
        Console.WriteLine("Тип, сгенерированный компилятором:");
        Console.WriteLine(stateMachineType.FullName);
        Console.WriteLine($"Реализует IAsyncStateMachine: {typeof(IAsyncStateMachine).IsAssignableFrom(stateMachineType)}");
        Console.WriteLine("Поля state machine:");

        foreach (var field in stateMachineType.GetFields(
                     BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            Console.WriteLine($"  {field.FieldType.Name} {field.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("У async-метода нет отдельного потока: компилятор сохраняет состояние");
        Console.WriteLine("и продолжает выполнение после завершения await.");
    }
}
