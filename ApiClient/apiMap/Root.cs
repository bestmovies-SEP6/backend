using System.Text.Json.Serialization;

namespace ApiClient.apiMap; 

public class MovieIdResponse {
    [JsonPropertyName("results")]
    public List<MovieId>? Results { get; set; }
}