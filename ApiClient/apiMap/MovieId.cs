using System.Text.Json.Serialization;

namespace ApiClient.apiMap; 

public class MovieId {

    [JsonPropertyName("id")]
    public int Id { get; set; }

    
}