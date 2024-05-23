namespace PersuadeMate.Assistant;

/// <summary>
///     OpenAI に接続せずにダミーの文言を返却するアドバイザーです
/// </summary>
public class StubAdvisor : IAdvisor
{
    public async Task<Result<List<string>, string>> GetAdviceAsync(string message)
    {
        return await Task.Run(() =>
        {
            List<string> advices = ["週末は温泉に浸かりながら、心身をリフレッシュするのがお勧めです。"];
            return new Result<List<string>, string>(advices);
        });
    }
}