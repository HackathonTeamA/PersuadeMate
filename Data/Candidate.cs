namespace PersuadeMate.Data;

/// <summary>
/// Advisor から返却される提言の候補の内容です
/// </summary>
public class Candidate
{
    /// <summary>
    /// 提言の内容です
    /// </summary>
    public required string Proposal { get; set; }

    /// <summary>
    /// 提言の内容を Advisor が自己評価した結果です
    /// </summary>
    public string? SelfEvaluation { get; set; }
}
