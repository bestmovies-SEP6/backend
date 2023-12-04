using System.Text.Json.Serialization;

namespace Dto;

public class KnownForDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")] // For movies
    public string? Title { get; set; }

    [JsonPropertyName("name")] // For TV shows
    public string? Name { get; set; }

}