namespace PersuadeMate.Data;

/// <summary>
/// アドバイザーに一言提案を求めるための質問です
/// </summary>
public class Question
{
    /// <summary>
    /// 提案をする対象の属性、趣味嗜好を表す内容です
    /// </summary>
    public Persona Persona { get; set; } = new();

    /// <summary>
    /// 提案したい具体的な内容です
    /// </summary>
    public string Suggestion { get; set; } = string.Empty;

    /// <summary>
    /// 提案する対象者の属性から、質問する文言を構築します
    /// </summary>
    /// <returns></returns>
    public string Message =>
        $"""
         次のような人に対して、{Suggestion}に誘いたいです。
         よいプレゼンテーションの文言を100文字程度にまとめて提案してください。

         その人は {Persona.Personality}です。
         """;
}
