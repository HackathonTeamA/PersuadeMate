using System.Text.Json.Serialization;
using PersuadeMate.Assistant.Extensions;

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

// Add Advisor Application Layer
builder.Services.AddAdvisor(builder.Configuration);

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
