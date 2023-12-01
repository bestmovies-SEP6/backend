namespace Data.dao.wishList; 

public interface IWishListsDao {
    Task AddMovieToWishList(string loggedInUser, int movieId);
    Task<List<int>> GetAllWishListedMovies(string username);
}