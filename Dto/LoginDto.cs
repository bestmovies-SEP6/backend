namespace Dto; 

public class LoginDto {
    public string Username { get; set; }
    public string Password { get; set; }

    public override string ToString() {
        return $"Username: {Username}, Password: {Password}";
    }
}