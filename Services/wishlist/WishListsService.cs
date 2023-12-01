using ApiClient.api;
using Data.dao.movies;
using Data.dao.wishList;
using Dto;

namespace Services.wishlist;

public class WishListsService : IWIshListsService {
    private readonly IWishListsDao _wishListsDao;
    private readonly IMoviesDao _moviesDao;
    private readonly IMoviesClient _moviesClient;


    public WishListsService(IWishListsDao wishListsDao, IMoviesDao moviesDao, IMoviesClient moviesClient) {
        _wishListsDao = wishListsDao;
        _moviesDao = moviesDao;
        _moviesClient = moviesClient;
    }


    public async Task AddMovieToWishList(string username, int movieId) {
        // Check if movie is in database if not add it to database.
        await _moviesDao.AddIfNotExists(movieId);
        await _wishListsDao.AddMovieToWishList(username, movieId);
    }

    public async Task<List<MovieDetailsDto>> GetAllWishListedMovies(string username) {
        List<int> movieIds = await _wishListsDao.GetAllWishListedMovies(username);
        List<Task<MovieDetailsDto>> movieTasks = movieIds.Select(id => _moviesClient.GetMovieDetailsById(id)).ToList();

        // This will fetch all the movies in parallel.
        MovieDetailsDto[] movieDetailsArray = await Task.WhenAll(movieTasks);
        return movieDetailsArray.ToList();
    }
}