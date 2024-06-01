namespace PersuadeMate.Assistant.Extensions;

public static class FilterNotNullExtensions
{
    /// <summary>
    /// 一覧データから要素が null でないものだけを取り出す拡張メソッドです
    /// </summary>
    /// <param name="source">元となる一覧データです</param>
    /// <typeparam name="T">一覧データの要素の型です</typeparam>
    /// <returns>nullな要素を含まない一覧データです</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?>? source)
        where T : class
    {
        var result = source?.Where(x => x is not null) ?? [];

        return result!;
    }
}
