using System.Text.Json.Serialization;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// appsettings.json に設定する IAdvisor の実態として何を使用するかを示す列挙体です
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvisorName>))]
public enum AdvisorName
{
    /// <summary>
    /// OpenAI API を用いた IAdvisor 実装を使用します
    /// </summary>
    AI,

    /// <summary>
    /// テスト用にスタブデータを返却する IAdvisor 実装を使用します
    /// </summary>
    Stub,
}
