using System.Text.Json.Serialization;

namespace Dto;

public class PersonMovieRolesDto
{
    [JsonPropertyName("lead_role")]
    public int? LeadRoles { get; set; }
    
    [JsonPropertyName("supporting_role")]
    public int? SupportingRoles { get; set; }
    
    [JsonPropertyName("guest_role")]
    public int? GuestRoles { get; set; }
    
    [JsonPropertyName("crew_role")]
    public int? CrewRoles { get; set; } 

    [JsonPropertyName("not_specified")]
    public int? NotSpecified { get; set; }
}