using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Extensions;
using OpenAI.Interfaces;
using PersuadeMate.Assistant.Advisors;
using PersuadeMate.Assistant.Ages;
using PersuadeMate.Data.Interfaces;

namespace PersuadeMate.Assistant.Extensions;

/// <summary>
/// REST API のエンドポイントで使用するための、Advisor 他もろもろを DI 登録するための
/// 拡張メソッドを定義するクラスです
/// </summary>
public static class ServiceCollectionsExtensions
{
    /// <summary>
    /// アドバイザーや関連するサービスを DIコンテナに登録します
    /// </summary>
    /// <param name="services">DIコンテナです</param>
    /// <param name="configuration">OpenAI の API キーなどを取得するための設定情報です</param>
    /// <returns>DIコンテナを返却します</returns>
    /// <exception cref="ArgumentNullException">OpenAI に対する API キーが不正です</exception>
    public static IServiceCollection AddAdvisor(this IServiceCollection services, IConfiguration configuration)
    {
        // OpenAI
        var apiKey = configuration.GetValue<string>("OpenAIServiceOptions:ApiKey");
        services.AddOpenAIService(settings =>
        {
            settings.ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        });

        // Store Advisor service in DI container
        var advisorName = configuration.GetValue<AdvisorName>("AdvisorName");
        services.AddScoped(typeof(IAdvisor), sp =>
        {
            if (advisorName == AdvisorName.Stub)
            {
                return new StubAdvisor();
            }

            var openAIService = sp.GetRequiredService<IOpenAIService>();
            var fineTune = advisorName == AdvisorName.God;
            return new AIAdvisor(openAIService, fineTune);
        });

        // Store Ages static Repository in DI container
        services.AddScoped<IAgesRepository, AgesRepository>();

        return services;
    }
}
