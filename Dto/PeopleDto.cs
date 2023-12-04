using System.Text.Json.Serialization;

namespace Dto;

public class PeopleDto
{
    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    // [JsonPropertyName("known_for")]
    // public string? Title{ get; set; }

    [JsonPropertyName("profile_path")]
    public string ProfilePath { get; set; }
    
    [JsonPropertyName("known_for")]
    public List<KnownForDto>? KnownForMovies { get; set; }
    
}