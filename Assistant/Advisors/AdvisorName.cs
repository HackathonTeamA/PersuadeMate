using System.Text.Json.Serialization;

namespace PersuadeMate.Assistant;

[JsonConverter(typeof(JsonStringEnumConverter<AdvisorName>))]
public enum AdvisorName
{
    AI,
    Stub,
}
