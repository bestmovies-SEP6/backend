namespace Services.wishlist; 

public interface IWIshListService {
    Task AddMovieToWishList(string loggedInUser, int movieId);
}