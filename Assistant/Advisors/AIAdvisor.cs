using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using PersuadeMate.Assistant.Extensions;
using PersuadeMate.Data.Values;

namespace PersuadeMate.Assistant.Advisors;

/// <summary>
/// OpenAI を利用して質問に対してアドバイスを回答するクラスです
/// </summary>
/// <param name="openAiService">Betalgo.OpenAI の提供する OpenAI SDKです</param>
public class AIAdvisor(IOpenAIService openAiService) : IAdvisor
{
    /// <inheritdoc />
    public async Task<Result<List<string>, string>> GetAdviceAsync(string message)
    {
        var response = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Model = Models.Gpt_3_5_Turbo,
            Messages = [ChatMessage.FromUser(message)],
            N = 5,
        });

        if (response.Successful)
        {
            var advices = response.Choices.Select(c => c.Message.Content).WhereNotNull().ToList();
            return new Result<List<string>, string>(advices);
        }

        var error = response.Error?.Message ?? "Encountered some error.";
        return new Result<List<string>, string>(error);
    }
}
