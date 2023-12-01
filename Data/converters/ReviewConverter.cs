using Dto;
using Entity;

namespace Data.converters; 

public static class ReviewConverter {


    public static ReviewEntity ToEntity( ReviewDto reviewDto) {
        return new ReviewEntity() {
            Author = reviewDto.Author,
            AuthoredAt = DateTime.Now,
            MovieId = reviewDto.MovieId,
            Rating = reviewDto.Rating,
            ReviewDescription = reviewDto.ReviewDescription
        };
    }

    public static ReviewDto ToDto(ReviewEntity reviewEntity) {
        return new ReviewDto() {
            Author = reviewEntity.Author,
            AuthoredAt = reviewEntity.AuthoredAt.ToString("yyyy-MM-dd HH:mm:ss"),
            MovieId = reviewEntity.MovieId,
            Rating = reviewEntity.Rating,
            ReviewDescription = reviewEntity.ReviewDescription
        };
    }
    
}