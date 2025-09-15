namespace Learning.Asynchronous.Extensions;

/// <summary>
/// Classe estática que contém métodos de 
/// extensão para <see cref="IEnumerable{T}"/>.
/// </summary>
internal static class IEnumerableExtension
{
    /// <summary>
    /// Executa uma ação para 
    /// cada elemento da sequência.
    /// </summary>
    internal static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
    {
        if (source is null || action is null) return;
        foreach (TSource item in source) action(item);
    }
}