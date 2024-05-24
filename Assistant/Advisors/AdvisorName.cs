using System.Text.Json.Serialization;

namespace PersuadeMate.Assistant.Advisors;

[JsonConverter(typeof(JsonStringEnumConverter<AdvisorName>))]
public enum AdvisorName
{
    AI,
    Stub,
}
