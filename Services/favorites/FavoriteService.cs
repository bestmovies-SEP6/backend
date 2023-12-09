using ApiClient.api;
using Data.dao.favorites;
using Data.dao.movies;
using Data.dao.wishList;
using Dto;

namespace Services.favorites; 

public class FavoriteService : IFavoritesService {
    private readonly IFavoritesDao _favoritesDao;
    private readonly IMoviesDao _moviesDao;
    private readonly IMoviesClient _moviesClient;


    public FavoriteService(IFavoritesDao favoritesDao, IMoviesDao moviesDao, IMoviesClient moviesClient) {
        _favoritesDao = favoritesDao;
        _moviesDao = moviesDao;
        _moviesClient = moviesClient;
    }



    public async Task AddMovieToFavorites(string username, int movieId) {
        // Check if movie is in database if not add it to database.
        await _moviesDao.AddIfNotExists(movieId);
        await _favoritesDao.AddMovieToFavorites(username, movieId);    }

    public async Task<List<MovieDetailsDto>> GetAllFavoriteMovies(string username) {
        List<int> movieIds = await _favoritesDao.GetAllFavoriteMovies(username);
        List<Task<MovieDetailsDto>> movieTasks = movieIds.Select(id => _moviesClient.GetMovieDetailsById(id)).ToList();

        // This will fetch all the movies in parallel.
        MovieDetailsDto[] movieDetailsArray = await Task.WhenAll(movieTasks);
        return movieDetailsArray.ToList();    }

    public async Task RemoveMovieFromFavorite(string loggedInUser, int movieId) {
        await _favoritesDao.RemoveMovieFromFavorites(loggedInUser, movieId);
    }
}