namespace Dto;

public static class ErrorMessages {
    // Signup errors
    public const string UsernameCannotBeEmpty = "Username cannot be empty";
    public const string UsernameCharacterCountMismatch = "Username must be between 3 and 20 characters";
    public const string PasswordCannotBeEmpty = "Password cannot be empty";
    public const string PasswordCharacterCountMismatch = "Password must be between 5 and 20 characters";
    public const string InvalidEmail = "Invalid email";

    // Login errors
    public const string UsernameAlreadyExists = "Username already exists, Please try another username";
    public const string UsernameNotFound = "Username not found";
    public const string IncorrectPassword = "Incorrect password";
    public const string LoginRequired = "You need to login to perform this action";

    // Wishlist errors
    public const string MovieAlreadyInWishlist = "You already have this movie in your wishlists";
    public const string MovieNotInWishlist = "You do not have provided movie inside your wishlists";


    // Favorites errors
    public const string MovieAlreadyInFavorites = "You already have this movie in your favorite list";
public const string MovieNotInFavorites = "You do not have provided movie inside your favorite list";


    // Review errors

    public const string ReviewAlreadyExists =
        "You have already reviewed this movie, if you want to change your review please edit or delete the previous review";

    public const string InvalidPageOrPageSize = "Page and page size must be greater than 0";
    public const string ReviewNotFound = "Requested review not found";

    public const string NotAuthorized = "You are not authorized to perform this action";
}