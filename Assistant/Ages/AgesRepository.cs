using System.Collections.Immutable;
using PersuadeMate.Data;
using PersuadeMate.Data.Interfaces;

namespace PersuadeMate.Assistant.Ages;

/// <summary>
/// 年代を提供するリポジトリ実装です。固定で 10代から 90代の年齢層が利用可能です
/// </summary>
public class AgesRepository : IAgesRepository
{
    private List<Age> Ages =>
    [
        new("Teens", "10代", 10),
        new("Twenties", "20代", 20),
        new("Thirties", "30代", 30),
        new("Forties", "40代", 40),
        new("Fifties", "50代", 50),
        new("Sixties", "60代", 60),
        new("SevenTies", "70代", 70),
        new("Eighties", "80代", 80),
        new("Nineties", "90代", 90),
    ];

    /// <inheritdoc />
    public IEnumerable<Age> GetAllAges() => Ages.OrderBy(age => age.Order).ToImmutableList();

    /// <inheritdoc />
    public Age? GetAgeByKey(string key) => Ages.SingleOrDefault(age => age.Key == key);
}
