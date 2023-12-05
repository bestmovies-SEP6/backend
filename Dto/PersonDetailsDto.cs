using System.Text.Json.Serialization;

namespace Dto;

public class PersonDetailsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("adult")]
    public bool IsAdult { get; set; }

    [JsonPropertyName("gender")]
    public int Gender { get; set; }
    [JsonPropertyName("gender_string")]

    public string? GenderString { get; set; }

    [JsonPropertyName("profile_path")]
    public string? ProfileImage { get; set; }
    
    [JsonPropertyName("known_for_department")]
    public string? Department { get; set; }
    
    [JsonPropertyName("also_known_as")]
    public List<string>? AlsoKnownAs { get; set; }
    
    [JsonPropertyName("biography")]
    public String Biography { get; set; }
    
    [JsonPropertyName("birthday")]
    public String Birthday { get; set; }
    
    [JsonPropertyName("deathday")]
    public String? Deathday { get; set; }
    
    [JsonPropertyName("homepage")]
    public String? HomePage { get; set; }
    
    [JsonPropertyName("place_of_birth")]
    public String? PlaceOfBirth { get; set; }
    
    [JsonPropertyName("combined_credits")]
    public CombinedCreditsDto? CombinedCredits { get; set; }
}

public class CombinedCreditsDto
{
    [JsonPropertyName("cast")]
    public List<MovieOrSeriesDto>? Cast { get; set; }

    [JsonPropertyName("crew")]
    public List<MovieOrSeriesDto>? Crew { get; set; }
}