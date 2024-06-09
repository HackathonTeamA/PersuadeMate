using System.Text;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels;

using PersuadeMate.Assistant.Extensions;
using PersuadeMate.Data;
using PersuadeMate.Data.Interfaces;
using PersuadeMate.Data.Values;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// OpenAI を利用して質問に対してアドバイスを回答するクラスです
/// </summary>
/// <param name="openAiService">Betalgo.OpenAI の提供する OpenAI SDKです</param>
/// <param name="fineTune">true の場合は GPT4o のモデルを使う</param>
public class AIAdvisor(IOpenAIService openAiService, bool fineTune = false) : IAdvisor
{
    /// <summary>
    /// API 呼び出しを短縮するためだけのメソッドです
    /// </summary>
    /// <param name="messages"></param>
    /// <param name="n"></param>
    /// <param name="temperature"></param>
    /// <returns></returns>
    private Task<ChatCompletionCreateResponse> Chat(List<ChatMessage> messages, int n)
    {
        return openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
        {
            Model = fineTune ? Models.Gpt_4o : Models.Gpt_3_5_Turbo,
            Messages = messages,
            N = n,
        });
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<Candidate>, string>> GetAdviceAsync(List<Interview> interviews, string suggestion)
    {
        var messages = GetQuestionMessages(interviews, suggestion);
        var response = await Chat(messages, n: 5);

        if (!response.Successful)
        {
            var error = response.Error?.Message ?? "Encountered some error.";
            return new Result<IEnumerable<Candidate>, string>(error);
        }

        // 問合せた5つの提言に対する自己評価を並列で実施する
        var personality = GerPersonality(interviews);
        var selfEvaluationTasks =
            response.Choices.Select(c => c.Message.Content)
                .WhereNotNull()
                .Select(proposal => Chat(
                    messages:
                    [
                        ChatMessage.FromSystem(personality),
                        ChatMessage.FromUser(
                            $"""
                             あなたは次のような提案を受け取りました。
                             その提案に対する評価を100文字程度で回答した上で、一行開行を空けて、総合評価を100点満点で何点か教えてください。

                             提案内容: {proposal}
                             """
                        ),
                    ],
                    n: 1
                ).ContinueWith(task => new { Proposal = proposal, task.Result }));
        var results = await Task.WhenAll(selfEvaluationTasks);


        var candidates = results.Select(result =>
        {
            var selfEvaluation = result.Result.Successful switch
            {
                true => result.Result.Choices.FirstOrDefault()?.Message.Content,
                false => null,
            };


            return new Candidate() { Proposal = result.Proposal, SelfEvaluation = selfEvaluation };
        });
        return new Result<IEnumerable<Candidate>, string>(candidates);
    }

    private List<ChatMessage> GetQuestionMessages(List<Interview> interviews, string suggestion)
    {
        var messages = new List<ChatMessage>()
        {
            ChatMessage.FromSystem("あなたは全知全能の自信に溢れた神です。人からの質問に自信満々で御告げを述べます。"),
            ChatMessage.FromAssistant("誰かを説得したいのか？そんな時はこの神にまかせたまえ!!"),
            ChatMessage.FromUser($"「{suggestion}」を提案するためのよい誘い文句を100文字程度でいただきたいです。"),
        };
        foreach (var (question, answer) in interviews)
        {
            messages.AddRange([
                ChatMessage.FromAssistant("説得したい相手を思い浮かべるのじゃ・・・"),
                ChatMessage.FromAssistant(question),
                ChatMessage.FromUser(answer is not null ? $"{answer}です。" : "わかりません。"),
            ]);
        }
        messages.AddRange([
            ChatMessage.FromAssistant("提案相手の情報はそれで全てか？"),
            ChatMessage.FromUser("はい。何卒、アドバイズをお願いします。"),
        ]);

        return messages;
    }

    private string GerPersonality(List<Interview> interviews)
    {
        if (interviews.All(interview => string.IsNullOrEmpty(interview.Answer)))
        {
            // 問答の回答が全くない場合は一般的な人となりであるとする
            return "あなたは一般的な常識を有した人物です。";
        }

        var personality = new StringBuilder("あなたの人となりは次の通りです。");
        foreach (var (_, answer) in interviews)
        {
            if (!string.IsNullOrEmpty(answer))
            {
                personality.Append($"{answer}です。");
            }
        }

        return personality.ToString();
    }
}
