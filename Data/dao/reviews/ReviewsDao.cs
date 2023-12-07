using Data.converters;
using Dto;
using Entity;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Data.dao.reviews;

public class ReviewsDao : IReviewsDao {
    private readonly DatabaseContext _databaseContext;

    public ReviewsDao(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task AddReview(ReviewDto reviewDto) {
        try {
            ReviewEntity reviewToAdd = ReviewConverter.ToEntity(reviewDto);
            await _databaseContext.AddAsync(reviewToAdd);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception e) {
            if (e is UniqueConstraintException) {
                throw new Exception(ErrorMessages.ReviewAlreadyExists);
            }

            throw;
        }
    }

    public async Task<GetMovieReviewsResponseDto> GetReviewsByMovieId(int movieId, int page, int pageSize) {
        try {
            if (page < 1 || pageSize < 1) {
                throw new Exception(ErrorMessages.InvalidPageOrPageSize);
            }

            int skipAmount = (page - 1) * pageSize;

            List<ReviewEntity> reviewEntities = await _databaseContext.Reviews
                .Where(entity => entity.MovieId == movieId)
                .OrderByDescending(review => review.AuthoredAt)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();

            if (!reviewEntities.Any()) {
                Console.WriteLine("No reviews found for movieId: " + movieId);
            }

            int totalReviews = await _databaseContext.Reviews
                .Where(entity => entity.MovieId == movieId)
                .CountAsync();

            int totalPages = (int) Math.Ceiling((double) totalReviews / pageSize);

            double averageRating = await _databaseContext.Reviews
                .Where(reviewEntity => reviewEntity.MovieId == movieId)
                .Select(entity => entity.Rating)
                .AverageAsync();

            return ReviewConverter.ToResponseDto(reviewEntities, totalPages, averageRating, totalReviews);
        }
        catch (Exception e) {
            if (e.Message.Contains("Sequence contains no elements")) {
                return new GetMovieReviewsResponseDto() {
                    Reviews = new List<ReviewDto>(),
                    AverageRating = 0,
                    MovieId = movieId,
                    TotalPages = 1,
                    TotalReviews = 0
                };
            }
            throw;
        }
    }

    public async Task<List<ReviewDto>> GetReviewsByMovieId(int movieId) {
        List<ReviewEntity> reviewEntities = await _databaseContext.Reviews
            .Where(reviewEntity => reviewEntity.MovieId == movieId)
            .ToListAsync();
        return ReviewConverter.ToDtoList(reviewEntities);
    }

    public async Task<double> GetAverageRatingByMovieId(int movieId) {
        return await _databaseContext.Reviews
            .Where(reviewEntity => reviewEntity.MovieId == movieId)
            .Select(entity => entity.Rating)
            .AverageAsync();
    }

    public async Task DeleteReview(int reviewId, string loggedInUser) {
        ReviewEntity? reviewEntity = await _databaseContext.Reviews.FindAsync(reviewId);
        if (reviewEntity is null) {
            throw new Exception(ErrorMessages.ReviewNotFound);
        }

        if (!reviewEntity.Author.Equals(loggedInUser)) {
            throw new Exception(ErrorMessages.NotAuthorized);
        }

        _databaseContext.Reviews.Remove(reviewEntity);
        await _databaseContext.SaveChangesAsync();
    }
}