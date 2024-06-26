using PersuadeMate.Data;
using PersuadeMate.Data.Interfaces;
using PersuadeMate.Data.Values;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// OpenAI に接続せずにダミーの文言を返却するアドバイザーです
/// </summary>
public class StubAdvisor : IAdvisor
{
    /// <inheritdoc />
    public async Task<Result<IEnumerable<Candidate>, string>> GetAdviceAsync(List<Interview> interviews, string suggestion)
    {
        return await Task.Run(() =>
        {
            List<Candidate> advices =
            [
                new()
                {
                    Proposal = """
                               週末、新たな冒険に出かけませんか？
                               温泉旅行に行ってリフレッシュしましょう！自然と温泉を満喫し、アウトドアや新しい友達との交流も楽しめます。
                               普段の忙しさを忘れてリラックスできる素敵な時間を過ごしましょう！
                               """,
                    SelfEvaluation = """
                                     提案内容はとても魅力的であり、週末に新しい冒険を求める私にとってぴったりです。
                                     温泉旅行でリフレッシュし、自然を満喫するのは最高です！アウトドアや新しい友達との交流も楽しそうです。
                                     普段の忙しさを忘れてリラックスできる素敵な時間を共有できることを楽しみにしています。
                                     
                                     総合評価：95点。
                                     """
                },
                new()
                {
                    Proposal = """
                               「週末、新しい冒険に出かけませんか？温泉旅行を提案したい」と誘い文句を考えるぞ。
                               """,
                    SelfEvaluation = """
                                     提案は興味深いですが、今週末は予定が詰まっているため無理そうです。
                                     次の週末にも似たような提案があれば参加したいと思います。
                                     
                                     総合評価：85点。
                                     """
                },
                new()
                {
                    Proposal = """
                               思案中じゃ……
                               こういう感じでいいか
                               
                               "自然とリフレッシュできる温泉旅行、アクティビティもいっぱい、一緒に新たな冒険に出かけよう！"
                               """,
                    SelfEvaluation = """
                                     提案ありがとうございます！温泉旅行とアクティビティ満載の冒険は魅力的ですね。
                                     自然とリフレッシュできる時間を過ごしながら新たな体験や挑戦にも興味があります。
                                     この提案にはとても惹かれます！
                                     
                                     総合評価：95点。
                                     """
                },
                new()
                {
                    Proposal = """
                               「新しい冒険に出かけよう！週末は温泉旅行に行こう。自然を満喫し、リフレッシュしよう！
                               君の広い交友関係とアウトドア好きな性格にピッタリだろう。一緒に素晴らしい思い出を作ろう！」
                               """,
                    SelfEvaluation = """
                                     提案はとても魅力的であり、自然を満喫しながらリフレッシュできる温泉旅行は素晴らしいアイデアだと思います。
                                     新しい冒険を楽しみながら広い交友関係とアウトドア好きな性格を活かせることは確かです。
                                     
                                     提案全体を考えると、100点中95点です。
                                     
                                     素晴らしい提案ですが、予定や予算などの面も考慮する必要があります。
                                     """
                },
                new()
                {
                    Proposal = """
                               「新たな冒険に出発しよう！週末、一緒に温泉旅行に行こう。
                               自然を満喫しながらリラックスできる絶好の機会だ。
                               新しい体験が待っているぞ！一緒に楽しい思い出を作ろう！」
                               """,
                    SelfEvaluation = """
                                     提案はとても魅力的で、自然とリラックスを楽しむ温泉旅行は心地よさそうだ。
                                     新たな体験や思い出を作りたい気持ちは共感できる。
                                     
                                     しかし、予定調整や予算面での課題も考えられる。
                                     全体として、楽しさやリフレッシュ度は高そうだが、準備や調整が必要だ。
                                     
                                     総合評価は90点。
                                     """
                }
            ];
            return new Result<IEnumerable<Candidate>, string>(advices);
        });
    }
}
