using ApiClient.api;
using Data.dao.movies;
using Data.dao.wishList;
using Dto;

namespace Services.wishlist;

public class WishListService : IWIshListService {
    private readonly IWishListDao _wishListDao;
    private readonly IMovieDao _movieDao;
    private readonly IMovieClient _movieClient;


    public WishListService(IWishListDao wishListDao, IMovieDao movieDao, IMovieClient movieClient) {
        _wishListDao = wishListDao;
        _movieDao = movieDao;
        _movieClient = movieClient;
    }


    public async Task AddMovieToWishList(string username, int movieId) {
        // Check if movie is in database if not add it to database.
        await _movieDao.AddIfNotExists(movieId);
        await _wishListDao.AddMovieToWishList(username, movieId);
    }

    public async Task<List<MovieDetailsDto>> GetAllWishListedMovies(string username) {
        List<int> movieIds = await _wishListDao.GetAllWishListedMovies(username);
        List<Task<MovieDetailsDto>> movieTasks = movieIds.Select(id => _movieClient.GetMovieDetailsById(id)).ToList();

        // This will fetch all the movies in parallel.
        MovieDetailsDto[] movieDetailsArray = await Task.WhenAll(movieTasks);
        return movieDetailsArray.ToList();
    }
}