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
    public  const string LoginRequired = "You need to login to perform this action";

    // Wishlist errors
    public const string MovieAlreadyInWishlist = "You already have this movie in your wishlist";
}