using Dto;
using Entity;

namespace Data.converters; 

public static class ReviewConverter {


    public static ReviewEntity ToEntity( ReviewDto reviewDto) {
        return new ReviewEntity() {
            Id = reviewDto.Id,
            Author = reviewDto.Author,
            AuthoredAt = DateTime.Now,
            MovieId = reviewDto.MovieId,
            Rating = reviewDto.Rating,
            ReviewDescription = reviewDto.ReviewDescription
        };
    }

    public static List<ReviewDto> ToDtoList(List<ReviewEntity> reviewEntities) {
        return reviewEntities.Select(ToDto).ToList();
    }
    public static ReviewDto ToDto(ReviewEntity reviewEntity) {
        return new ReviewDto() {
            Id = reviewEntity.Id,
            Author = reviewEntity.Author,
            AuthoredAt = reviewEntity.AuthoredAt.ToString("yyyy-MM-dd HH:mm:ss"),
            MovieId = reviewEntity.MovieId,
            Rating = reviewEntity.Rating,
            ReviewDescription = reviewEntity.ReviewDescription
        };
    }

   
    
}