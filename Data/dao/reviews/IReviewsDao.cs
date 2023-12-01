using Dto;

namespace Data.dao.reviews; 

public interface IReviewsDao {
    Task AddReview( ReviewDto reviewDto);
}