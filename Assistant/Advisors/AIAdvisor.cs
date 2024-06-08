using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels;
using PersuadeMate.Assistant.Extensions;
using PersuadeMate.Data;
using PersuadeMate.Data.Requests;
using PersuadeMate.Data.Values;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// OpenAI を利用して質問に対してアドバイスを回答するクラスです
/// </summary>
/// <param name="openAiService">Betalgo.OpenAI の提供する OpenAI SDKです</param>
public class AIAdvisor(IOpenAIService openAiService) : IAdvisor
{
    private const string YouAreGod =
        """
        You are the omniscient and omnipotent God who lives forever.
        Grant us wisdom as a transcendent being who has watched over mankind for a long time.
        """;

    /// <summary>
    /// API 呼び出しを短縮するためだけのメソッドです
    /// </summary>
    /// <param name="messages"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    private Task<ChatCompletionCreateResponse> Chat(List<ChatMessage> messages, int n)
    {
        return openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
        {
            Model = Models.Gpt_3_5_Turbo,
            Messages = messages,
            N = n,
        });
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<Candidate>, string>> GetAdviceAsync(SuggestionRequest request)
    {
        var response = await Chat(messages:
            [
                ChatMessage.FromSystem(YouAreGod),
                ChatMessage.FromUser(request.QuestionMessage),
            ],
            n: 5
        );

        if (!response.Successful)
        {
            var error = response.Error?.Message ?? "Encountered some error.";
            return new Result<IEnumerable<Candidate>, string>(error);
        }


        // 問合せた5つの提言に対する自己評価を並列で実施する
        var selfEvaluationTasks =
            response.Choices.Select(c => c.Message.Content)
                .WhereNotNull()
                .Select(proposal => Chat(
                    messages:
                    [
                        ChatMessage.FromSystem($"あなたは、${request.ProposedTo.Personality}な人物です。"),
                        ChatMessage.FromUser(
                            $"""
                             あなたは次のような提案を受け取りました。
                             その提案に対する評価を100文字程度で回答した上で、総合評価を100点満点で何点か答えてください。

                             提案内容: {proposal}
                             """
                        ),
                    ],
                    n: 1
                ).ContinueWith(task => new { Proposal = proposal, Result = task.Result }));
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
}
