using Dto;

namespace Data.dao.reviews; 

public interface IReviewsDao {
    Task AddReview( ReviewDto reviewDto);
    Task<GetMovieReviewsResponseDto> GetReviewsByMovieId(int movieId, int page, int pageSize);
    Task<double> GetAverageRatingByMovieId(int movieId);
    Task DeleteReview(int reviewId, string loggedInUser);
}