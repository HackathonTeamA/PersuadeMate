using System.Text.Json.Serialization;
using OpenAI.Extensions;
using OpenAI.Interfaces;
using PersuadeMate.Assistant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Default Cors Profile
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policyBuilder => { policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});

// OpenAI
var apiKey = builder.Configuration.GetValue<string>("OpenAIServiceOptions:ApiKey");
builder.Services.AddOpenAIService(settings =>
{
    settings.ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
});

// Store Advisor service in DI container
var advisorName = builder.Configuration.GetValue<AdvisorName>("AdvisorName");
builder.Services.AddScoped(typeof(IAdvisor), sp =>
{
    if (advisorName == AdvisorName.AI)
    {
        var openAIService = sp.GetRequiredService<IOpenAIService>();
        return new AIAdvisor(openAIService);
    }

    return new StubAdvisor();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Serve static files under wwwroot/
app.UseStaticFiles();

// Apply Default Cors
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
