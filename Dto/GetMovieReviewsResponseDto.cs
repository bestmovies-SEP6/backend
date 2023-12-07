using System.Text.Json.Serialization;

namespace Dto; 

public class GetMovieReviewsResponseDto {
    [JsonPropertyName("movie_id")]
    public int MovieId { get; set; }

    [JsonPropertyName("reviews")]
    public List<ReviewDto> Reviews { get; set; }

    [JsonPropertyName("total_reviews")]
    public int TotalReviews { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("average_rating")] public double AverageRating { get; set; }


    
}