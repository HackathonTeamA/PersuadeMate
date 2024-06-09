using System.ComponentModel.DataAnnotations;

namespace PersuadeMate.Api.Requests;

/// <summary>
/// 一言提案を取得するために必要な情報を保持するリクエストデータです
/// </summary>
public class SuggestionRequest
{
    /// <summary>
    /// 提案をする対象の属性、趣味嗜好を表す内容です
    /// </summary>
    [Required]
    public ProposedTo ProposedTo { get; set; } = new();

    /// <summary>
    /// 提案したい具体的な内容です
    /// </summary>
    [Required]
    public string Suggestion { get; set; } = string.Empty;
}
