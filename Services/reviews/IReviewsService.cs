using Dto;

namespace Services.reviews; 

public interface IReviewsService {
    Task AddReview( ReviewDto reviewDto);
    Task<GetMovieReviewsResponseDto> GetReviewsByMovieId(int movieId, int page, int pageSize);
    Task<double> GetAverageRatingByMovieId(int movieId);
}                                                         