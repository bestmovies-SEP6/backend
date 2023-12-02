using System.Text.Json.Serialization;

namespace Dto; 

public class LoginResponseDto {
    [JsonPropertyName("jwt_token")]
    public string? JwtToken { get; set; }

}