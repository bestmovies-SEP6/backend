namespace Data.dao.wishList; 

public interface IWishListDao {
    Task AddMovieToWishList(string loggedInUser, int movieId);
    Task<List<int>> GetAllWishListedMovies(string username);
}