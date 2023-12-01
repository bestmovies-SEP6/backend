using Data.dao.movies;
using Data.dao.reviews;
using Dto;

namespace Services.reviews; 

public class ReviewsService : IReviewsService {


    private readonly IMoviesDao _moviesDao;
    private readonly IReviewsDao _reviewsDao;

    public ReviewsService(IReviewsDao reviewsDao, IMoviesDao moviesDao) {
        _reviewsDao = reviewsDao;
        _moviesDao = moviesDao;
    }
    public async Task AddReview( ReviewDto reviewDto) {
        await _moviesDao.AddIfNotExists(reviewDto.MovieId);
        await _reviewsDao.AddReview(reviewDto);
    }
}