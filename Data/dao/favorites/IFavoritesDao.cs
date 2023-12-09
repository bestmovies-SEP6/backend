namespace Data.dao.favorites; 

public interface IFavoritesDao {
    
    Task AddMovieToFavorites(string loggedInUser, int movieId);
    Task<List<int>> GetAllFavoriteMovies(string username);
    Task RemoveMovieFromFavorites(string loggedInUser, int movieId);
}