using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Dto;

public class ReviewDto {

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("movie_id")] public int MovieId { get; set; }

    [JsonPropertyName("description")] public string? ReviewDescription { get; set; }

    [JsonPropertyName("rating")] public double Rating { get; set; }

    // Format 2023-11-17 12:00:00
    [JsonPropertyName("authored_at")] public string? AuthoredAt { get; set; }

    [JsonPropertyName("author")] public string? Author { get; set; }
}