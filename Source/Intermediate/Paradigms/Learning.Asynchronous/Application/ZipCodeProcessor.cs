namespace Learning.Asynchronous.Application;

/// <summary>
/// Classe estática que representa
/// o ponto de entrada da aplicação.
/// </summary>
internal static class ZipCodeProcessor
{
    /// <summary>
    /// Processa os CEPs fornecidos.
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    internal static async Task RunAsync(string[] zipCodes, CancellationToken token = default)
    {
        Benchmark.Start();

        ConcurrentBag<string> errors = [];
        using ViaCEPService service = new(new HttpClient());

        AddressResponse[] responses = await Task
            .WhenAll(zipCodes
                .Where(zipCode => !string.IsNullOrWhiteSpace(zipCode))
                .Select(async zipCode =>
                {
                    try { return await service.GetAddressAsync(zipCode, token); }
                    catch (Exception ex)
                    {
                        errors.Add($"Errors: {ex.Message}");
                        return null;
                    }
                }))
            .ConfigureAwait(false);

        responses
            .Where(address => address is not null)
            .ForEach(address => Console.WriteLine(GetPropertyValue(address)));

        errors.ForEach(Console.WriteLine);

        Benchmark.Stop();
        Benchmark.DisplayElapsedTime();
    }

    /// <summary>
    /// Retorna o valor de todas
    /// as propriedades não nulas.
    /// </summary>
    private static StringBuilder GetPropertyValue(AddressResponse address)
    {
        StringBuilder builder = new();
        PropertyInfo[] properties = address
            .GetType()
            .GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object currentValue = property.GetValue(address);

            if (currentValue is string value && !string.IsNullOrWhiteSpace(value))
                builder.AppendLine($"{property.Name}: {value}");
        }

        return builder;
    }
}