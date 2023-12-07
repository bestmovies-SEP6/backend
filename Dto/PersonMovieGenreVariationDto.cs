using System.Text.Json.Serialization;

namespace Dto;

public class PersonMovieGenreVariationDto
{
    [JsonPropertyName("action")]
    public int? Action { get; set; }
    
    [JsonPropertyName("adventure")]
    public int? Adventure { get; set; }
    
    [JsonPropertyName("animation")]
    public int? Animation { get; set; }
    
    [JsonPropertyName("comedy")]
    public int? Comedy { get; set; }
    
    [JsonPropertyName("crime")]
    
    public int? Crime { get; set; }
    
    [JsonPropertyName("documentary")]
    public int? Documentary { get; set; }
    
    [JsonPropertyName("drama")]
    public int? Drama { get; set; }
    
    [JsonPropertyName("family")]
    public int? Family { get; set; }
    
    [JsonPropertyName("fantasy")]
    public int? Fantasy { get; set; }
    
    [JsonPropertyName("history")]
    public int? History { get; set; }
    
    [JsonPropertyName("horror")]
    public int? Horror { get; set; }
    
    [JsonPropertyName("music")]
    public int? Music { get; set; }
    
    [JsonPropertyName("mystery")]
    public int? Mystery { get; set; }
    
    [JsonPropertyName("romance")]
    public int? Romance { get; set; }
    
    [JsonPropertyName("science_fiction")]
    public int? ScienceFiction { get; set; }
    
    [JsonPropertyName("tv_movie")]
    public int? TVMovie { get; set; }
    
    [JsonPropertyName("thriller")]
    public int? Thriller { get; set; }
    
    [JsonPropertyName("war")]
    public int? War { get; set; }
    
    [JsonPropertyName("western")]
    public int? Western { get; set; }
    
    [JsonPropertyName("kids")]
    public int? Kids { get; set; }
    
    [JsonPropertyName("news")]
    public int? News { get; set; }
    
    [JsonPropertyName("reality")]
    public int? Reality { get; set; }
    
    [JsonPropertyName("sci_fi_fantasy")]
    public int? SciFiFantasy { get; set; }
    
    [JsonPropertyName("soap")]
    public int? Soap { get; set; }
    
    [JsonPropertyName("talk")]
    public int? Talk { get; set; }
    
    [JsonPropertyName("war_politics")]
    public int? WarPolitics { get; set; }
}