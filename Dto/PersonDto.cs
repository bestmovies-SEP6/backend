using System.Text.Json.Serialization;

namespace Dto; 

public class PersonDto {
    

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("adult")]
    public bool IsAdult { get; set; }

    [JsonPropertyName("gender")]
    public int Gender { get; set; }

    [JsonPropertyName("profile_path")]
    public string? ProfileImage { get; set; }

    [JsonPropertyName("character")]
    public string? CharacterName { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("known_for_department")]
    public string? Department { get; set; }
}