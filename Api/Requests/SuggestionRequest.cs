using System.ComponentModel.DataAnnotations;

namespace PersuadeMate.Api.Requests;

/// <summary>
/// 一言提案を要求するためのリクエストパラメータです
/// </summary>
public class SuggestionRequest
{
    /// <summary>
    /// 提案をする対象の属性、趣味嗜好を表す内容です
    /// </summary>
    [Required]
    public Persona ProposedTo { get; set; } = new();

    /// <summary>
    /// 提案したい具体的な内容です
    /// </summary>
    [Required]
    public string Suggestion { get; set; } = string.Empty;

    /// <summary>
    /// 提案する対象者の属性から、質問する文言を構築します
    /// </summary>
    /// <returns></returns>
    public string CreateQuestionMessage()
    {
        var gender = ProposedTo.Gender switch
        {
            Gender.Male => "男性",
            Gender.Female => "女性",
            _ => "性別は不明",
        };
        var age = ProposedTo.Age switch
        {
            Age.Teens => "10歳代",
            Age.Twenties => "20歳代",
            Age.Thirties => "30歳代",
            Age.Forties => "40歳代",
            _ => "年齢層は不明",
        };
        var preferences = string.Join("で、", ProposedTo.Preferences);
        var personaStr = $"{preferences}。{age}の{gender}です。";
        return $"""
                次のような人に対して、{Suggestion}に誘いたいです。
                よいプレゼンテーションの文言を100文字程度にまとめて提案してください。

                その人は {personaStr}です。
                """;
    }
}
