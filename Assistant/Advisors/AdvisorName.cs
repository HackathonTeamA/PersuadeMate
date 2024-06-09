using System.Text.Json.Serialization;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// appsettings.json に設定する IAdvisor の実態として何を使用するかを示す列挙体です
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvisorName>))]
public enum AdvisorName
{
    /// <summary>
    /// OpenAI API Model GPT3.5Turbo を用いた Advisor 実装を使用します
    /// </summary>
    AI,

    /// <summary>
    /// OpenAI API Model GPT4o を用いたアドバイザー実装を使用します
    /// </summary>
    God,

    /// <summary>
    /// テスト用にスタブデータを返却する IAdvisor 実装を使用します
    /// </summary>
    Stub,
}
