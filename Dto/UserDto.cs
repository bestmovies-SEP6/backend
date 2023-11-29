namespace Dto; 

public class UserDto {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }


    public override string ToString() {
        return $"Username: {Username}, Email: {Email}, Password: {Password}";
    }
}