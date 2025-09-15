namespace Learning.Asynchronous.Application;

/// <summary>
/// Classe estática para medir o tempo de execução 
/// de operações, baseada em <see cref="Stopwatch"/>.
/// </summary>
internal static class Benchmark
{
    /// <summary>
    /// Tempo decorrido.
    /// </summary>
    private static Stopwatch s_stopwatch;

    /// <summary>
    /// Inicia o cronômetro interno.
    /// </summary>
    internal static void Start() =>
        s_stopwatch = Stopwatch.StartNew();

    /// <summary>
    /// Para o cronômetro interno.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    internal static void Stop()
    {
        ThrowIfNull();
        s_stopwatch.Stop();
    }

    /// <summary>
    /// Exibe o tempo decorrido em segundos 
    /// desde que o cronômetro foi iniciado.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    internal static void DisplayElapsedTime()
    {
        ThrowIfNull();
        Console.Write($"\nTime: {s_stopwatch.Elapsed.TotalSeconds:F2}");
        Console.ReadKey();
    }

    /// <summary>
    /// Ocorre uma exceção 
    /// se <see cref="Stopwatch"/>
    /// não tiver sido inicializado.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    private static void ThrowIfNull()
    {
        if (s_stopwatch is null)
            throw new ArgumentException("The benchmark has not been started.");
    }
}