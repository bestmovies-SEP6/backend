using Dto;

namespace Services.favorites;

public interface IFavoritesService {
    Task AddMovieToFavorites(string username, int movieId);
    Task<List<MovieDetailsDto>> GetAllFavoriteMovies(string username);
    Task RemoveMovieFromFavorite(string loggedInUser, int movieId);
}