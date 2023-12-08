using System.Text.Json.Serialization;

namespace Dto;

public class PersonMoviePopularityDto
{
    [JsonPropertyName("movie_title")]
    public string Title { get; set; }
    
    [JsonPropertyName("popularity_scores")]
    public double PopularityScores { get; set; }
    
    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }
}