using System.Text.Json.Serialization;
using Dto;

namespace ApiClient.apiMap; 

public class MovieIdResponseRoot {
    [JsonPropertyName("results")]
    public List<MovieDto>? Results { get; set; }
}