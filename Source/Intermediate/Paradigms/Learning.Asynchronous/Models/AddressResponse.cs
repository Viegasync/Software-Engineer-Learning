namespace Learning.Asynchronous.Models;

/// <summary>
/// Representa um endereço 
/// retornado pela API do ViaCEP.
/// </summary>
internal sealed record AddressResponse
{
    /// <summary>
    /// Unidade Federativa.
    /// </summary>
    [JsonPropertyName("uf")]
    public string State { get; init; }

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    [JsonPropertyName("localidade")]
    public string City { get; init; }

    /// <summary>
    /// Nome da rua.
    /// </summary>
    [JsonPropertyName("logradouro")]
    public string Street { get; init; }

    /// <summary>
    /// Código de Endereçamento Postal.
    /// </summary>
    [JsonPropertyName("cep")]
    public string ZipCode { get; init; }

    /// <summary>
    /// Indica se houve erro na busca.
    /// </summary>
    [JsonPropertyName("erro")]
    public string Error { get; init; }
}