using PersuadeMate.Assistant.Values;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
///     質問に対して何らかのアドバイスを与える機能のインターフェイスです
/// </summary>
public interface IAdvisor
{
    /// <summary>
    ///     質問に対して、アドバイスを(いくつか)返却します
    /// </summary>
    /// <param name="message">質問の文言です</param>
    /// <returns></returns>
    Task<Result<List<string>, string>> GetAdviceAsync(string message);
}
