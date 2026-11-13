namespace Delegates;

// Тип делегата описывает «форму» метода: принимает double, возвращает double
internal delegate double MathFunction(double x);

internal static class Program
{
    private static void Main()
    {
        // Делегат — «ярлык» (алиас) на метод: переменная хранит ссылку на Sine, а не результат его вызова
        MathFunction sineDelegate = Sine;
        MathFunction squareDelegate = Square;

        var sineIntegral = Integrate(sineDelegate, 0, Math.PI, 10_000);
        var squareIntegral = Integrate(squareDelegate, 0, 1, 10_000);

        Console.WriteLine($"Интеграл sin(x) на [0, pi] ≈ {sineIntegral:F4}");
        Console.WriteLine($"Интеграл x^2 на [0, 1] ≈ {squareIntegral:F4}");

        // Прямой вызов делегата и вызов через Invoke — одно и то же
        Console.WriteLine($"Sine(0.5) через Invoke: {sineDelegate.Invoke(0.5):F4}");

        // Делегат может быть null (на него ничего не указывает); ?.Invoke вызывает метод только если делегат не null
        MathFunction? emptyDelegate = null;
        double? result = emptyDelegate?.Invoke(0.5);
        Console.WriteLine($"Пустой делегат через ?.Invoke: {(result.HasValue ? result.Value.ToString("F4") : "null, вызов пропущен")}");
    }

    private static double Sine(double x)
    {
        return Math.Sin(x);
    }

    private static double Square(double x)
    {
        return x * x;
    }

    private static double Integrate(MathFunction function, double start, double end, int steps)
    {
        var stepWidth = (end - start) / steps;
        var sum = (function(start) + function(end)) / 2;

        for (var index = 1; index < steps; index++)
        {
            sum += function(start + index * stepWidth);
        }

        return sum * stepWidth;
    }
}
