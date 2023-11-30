using Data.dao.movies;
using Data.dao.wishList;

namespace Services.wishlist;

public class WishListService : IWIshListService {
    private readonly IWishListDao _wishListDao;
    private readonly IMovieDao _movieDao;


    public WishListService(IWishListDao wishListDao, IMovieDao movieDao) {
        _wishListDao = wishListDao;
        _movieDao = movieDao;
    }


    public async Task AddMovieToWishList(string loggedInUser, int movieId) {
        // Check if movie is in database if not add it to database.
        await _movieDao.AddIfNotExists(movieId);
        await _wishListDao.AddMovieToWishList(loggedInUser, movieId);
    }
}