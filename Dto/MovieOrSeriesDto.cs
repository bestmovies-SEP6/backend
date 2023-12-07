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
    
    [JsonPropertyName("original_name")]
    public string? OriginalName { get; set; }
    
    [JsonPropertyName("popularity")]
    public double? Popularity { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("media_type")]
    public string MediaType { get; set; }
    
    [JsonPropertyName("order")]
    public int? Order { get; set; }
    
    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }
    
    [JsonPropertyName("first_air_date")]
    public string? FirstAirDate { get; set; }
    
    [JsonPropertyName("genre_ids")]
    public List<int> GenreIds { get; set; }
}