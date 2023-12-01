using Data.converters;
using Dto;
using Entity;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Data.dao.reviews; 

public class ReviewsDao : IReviewsDao {

    private DatabaseContext _databaseContext;

    public ReviewsDao(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task AddReview( ReviewDto reviewDto) {
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
}