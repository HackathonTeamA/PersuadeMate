using PersuadeMate.Data;
using PersuadeMate.Data.Requests;
using PersuadeMate.Data.Values;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// 質問に対して何らかのアドバイスを与える機能のインターフェイスです
/// </summary>
public interface IAdvisor
{
    /// <summary>
    /// 質問に対して、アドバイスを(いくつか)返却します。同時にアドバイスの自己評価も行います
    /// </summary>
    /// <param name="request">提案対象者の特徴や提案したい事柄です</param>
    /// <returns></returns>
    Task<Result<IEnumerable<Candidate>, string>> GetAdviceAsync(SuggestionRequest request);
}
