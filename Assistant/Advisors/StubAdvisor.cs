using PersuadeMate.Data;
using PersuadeMate.Data.Requests;
using PersuadeMate.Data.Values;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// OpenAI に接続せずにダミーの文言を返却するアドバイザーです
/// </summary>
public class StubAdvisor : IAdvisor
{
    /// <inheritdoc />
    public async Task<Result<IEnumerable<Candidate>, string>> GetAdviceAsync(SuggestionRequest request)
    {
        return await Task.Run(() =>
        {
            List<Candidate> advices =
            [
                new Candidate()
                {
                    Proposal = "週末は温泉に浸かりながら、心身をリフレッシュするのがお勧めです。",
                    SelfEvaluation = "中々興味をそそられる提案ですね。評価は 80点です。"
                }
            ];
            return new Result<IEnumerable<Candidate>, string>(advices);
        });
    }
}
