using Dto;

namespace Services.reviews; 

public interface IReviewsService {
    Task AddReview( ReviewDto reviewDto);
    Task<List<ReviewDto>> GetReviewsByMovieId(int movieId);
}