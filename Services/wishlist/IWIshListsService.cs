using Dto;

namespace Services.wishlist; 

public interface IWIshListsService {
    Task AddMovieToWishList(string username, int movieId);
    Task<List<MovieDetailsDto>> GetAllWishListedMovies(string username);
}