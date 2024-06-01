namespace PersuadeMate.Data;

/// <summary>
/// 提案する対象の人を表現するクラスです
/// </summary>
public record class Persona
{
    /// <summary>
    /// 性別です
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 年齢層です
    /// </summary>
    public Age? Age { get; set; }

    /// <summary>
    /// 趣味・嗜好です
    /// </summary>
    public List<string> Preferences { get; set; } = [];
}
