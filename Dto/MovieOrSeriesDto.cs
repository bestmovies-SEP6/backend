using System.Text.Json.Serialization;

namespace Dto;

public class MovieOrSeriesDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title{ get; set; }
    
    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }
    
    [JsonPropertyName("original_title")]
    public string? OriginalName { get; set; }
    
    
}