namespace Dto; 

public class ErrorMessages {
    // Signup errors
    public static string UsernameCannotBeEmpty = "Username cannot be empty";
    public static string UsernameCharacterCountMismatch = "Username must be between 3 and 20 characters";
    public static string PasswordCannotBeEmpty = "Password cannot be empty";
    public static string PasswordCharacterCountMismatch = "Password must be between 5 and 20 characters";
    public static string InvalidEmail = "Invalid email";

    // Login errors
    public static string UsernameAlreadyExists = "Username already exists";
    public static readonly string UsernameNotFound = "Username not found";
    public static string IncorrectPassword = "Incorrect password";
  


}