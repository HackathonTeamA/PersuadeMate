using PersuadeMate.Data;

namespace PersuadeMate.Api.Requests;

/// <summary>
/// 提案をおこなう対象の人物にたいする情報を保持します
/// </summary>
public class ProposedTo
{
    /// <summary>
    /// 性別です
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 年代です
    /// </summary>
    public string? Age { get; set; }

    /// <summary>
    /// 趣味・嗜好です
    /// </summary>
    public List<string> Preferences { get; set; } = [];
}
