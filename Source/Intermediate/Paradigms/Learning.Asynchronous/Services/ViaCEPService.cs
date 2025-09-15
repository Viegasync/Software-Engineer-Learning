namespace Learning.Asynchronous.Services;

/// <summary>
/// Serviço que fornece acesso à API.
/// </summary>
internal sealed class ViaCEPService(HttpClient http) : IDisposable
{
    /// <summary>
    /// Realiza uma chamada assíncrona à API e, 
    /// conforme o CEP informado, obtém os dados.
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    internal async Task<AddressResponse> GetAddressAsync(string zipCode, CancellationToken token = default)
    {
        ValidateZipCode(zipCode);

        HttpResponseMessage response = await http
            .GetAsync($"https://viacep.com.br/ws/{zipCode}/json/", token)
            .ConfigureAwait(false);

        AddressResponse address = await response.Content
            .ReadFromJsonAsync<AddressResponse>(token)
            .ConfigureAwait(false);

        return !response.IsSuccessStatusCode || address?.Error == "true"
            ? throw new HttpRequestException($"Address \"{zipCode}\" not found.")
            : address;
    }

    /// <summary>
    /// Valida e formata um CEP, 
    /// removendo caracteres não numéricos.
    /// </summary>
    /// <exception cref="FormatException"></exception>
    private static string ValidateZipCode(string zipCode)
    {
        string formatted = new([.. zipCode
            .Where(char.IsDigit)]);

        return formatted.Length != 8
            ? throw new FormatException($"ZipCode \"{zipCode}\" is invalid.")
            : formatted;
    }

    /// <summary>
    /// Libera os recursos utilizados
    /// pelo <see cref="HttpClient"/> interno.
    /// </summary>
    public void Dispose() => http.Dispose();
}