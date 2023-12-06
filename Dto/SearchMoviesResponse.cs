using System.Text.Json.Serialization;

namespace Dto; 

public class SearchMoviesResponse {
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("results")]
    public List<MovieDto> Results { get; set; }
}