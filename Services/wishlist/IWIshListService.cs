using Dto;

namespace Services.wishlist; 

public interface IWIshListService {
    Task AddMovieToWishList(string username, int movieId);
    Task<List<MovieDetailsDto>> GetAllWishListedMovies(string username);
}