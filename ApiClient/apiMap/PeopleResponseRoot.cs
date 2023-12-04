using System.Text.Json.Serialization;
using Dto;

namespace ApiClient.apiMap;

public class PeopleResponseRoot
{
    [JsonPropertyName("results")]
    public List<PeopleDto>? Results { get; set; }
}