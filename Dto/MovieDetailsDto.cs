using System.Text.Json.Serialization;

namespace Dto;

public class MovieDetailsDto {
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("title")] public string? Title { get; set; }

    [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }

    [JsonPropertyName("release_date")] public string ReleaseDate { get; set; }

    [JsonPropertyName("vote_average")] public double? VoteAverage { get; set; }

    [JsonPropertyName("adult")] public bool IsAdult { get; set; }

    [JsonPropertyName("budget")] public int? Budget { get; set; }

    [JsonPropertyName("genres")] public ICollection<GenreDto>? Genres { get; set; }

    [JsonPropertyName("homepage")] public string? HomePage { get; set; }

    [JsonPropertyName("original_title")] public string? OriginalTitle { get; set; }

    [JsonPropertyName("popularity")] public double Popularity { get; set; }

    [JsonPropertyName("production_companies")]
    public ICollection<ProductionCompanyDto>? ProductionCompanies { get; set; }

    [JsonPropertyName("revenue")] public int? Revenue { get; set; }

    [JsonPropertyName("status")] public string? Status { get; set; }

    [JsonPropertyName("tagline")] public string? TagLine { get; set; }

    [JsonPropertyName("vote_count")] public int VoteCount { get; set; }

    [JsonPropertyName(("runtime"))] public int Runtime { get; set; }

    [JsonPropertyName("overview")] public string? Overview { get; set; }

    [JsonPropertyName("backdrop_path")] public string? BackdropPath { get; set; }
}