using PersuadeMate.Data.Values;

namespace PersuadeMate.Data.Interfaces;

/// <summary>
/// 質問に対して何らかのアドバイスを与える機能のインターフェイスです
/// </summary>
public interface IAdvisor
{
    /// <summary>
    /// 質問に対して、アドバイスを(いくつか)返却します。同時にアドバイスの自己評価も行います
    /// </summary>
    /// <param name="interviews">提案対象者の特徴に関する問答内容です</param>
    /// <param name="suggestion">提案したい内容です</param>
    /// <returns></returns>
    Task<Result<IEnumerable<Candidate>, string>> GetAdviceAsync(List<Interview> interviews, string suggestion);
}
