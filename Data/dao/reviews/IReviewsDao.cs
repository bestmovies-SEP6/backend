using Dto;

namespace Data.dao.reviews; 

public interface IReviewsDao {
    Task AddReview( ReviewDto reviewDto);
    Task<List<ReviewDto>> GetReviewsByMovieId(int movieId);
    Task<double> GetAverageRatingByMovieId(int movieId);
}