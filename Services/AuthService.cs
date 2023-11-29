using System.Text.RegularExpressions;
using Data.dao;
using Dto;

namespace Services;

public class AuthService : IAuthService {
    private readonly IAuthDao _authDao;

    public AuthService(IAuthDao authDao) {
        _authDao = authDao;
    }

    public async Task<UserDto> ValidateUser(LoginDto loginDto) {
        CheckForValidUsernameAndPassword(loginDto.Username, loginDto.Password);
        UserDto userFromDatabase = await _authDao.GetUserByUsername(loginDto.Username);

        bool doesPasswordMatch = VerifyPassword(loginDto.Password, userFromDatabase.Password);

        if (!doesPasswordMatch) {
            throw new Exception(ErrorMessages.IncorrectPassword);
        }

        return userFromDatabase;
    }

    public Task<UserDto> RegisterUser(UserDto userDto) {
        CheckForValidUsernameAndPassword(userDto.Username, userDto.Password);
        CheckForValidEmail(userDto.Email);

        string hashedPassword = HashPassword(userDto.Password);
        userDto.Password = hashedPassword;
        return _authDao.AddUser(userDto);
    }

    private void CheckForValidUsernameAndPassword(string username, string password) {
        if (string.IsNullOrWhiteSpace(username)) {
            throw new Exception(ErrorMessages.UsernameCannotBeEmpty);
        }

        if (username.Length < 3 || username.Length > 20) {
            throw new Exception(ErrorMessages.UsernameCharacterCountMismatch);
        }

        if (string.IsNullOrWhiteSpace(password)) {
            throw new Exception(ErrorMessages.PasswordCannotBeEmpty);
        }

        if (password.Length < 5 || password.Length > 20) {
            throw new Exception(ErrorMessages.PasswordCharacterCountMismatch);
        }
    }

    private void CheckForValidEmail(string email) {
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        Regex regex = new Regex(pattern);

        Match match = regex.Match(email);

        if (!match.Success) {
            throw new Exception(ErrorMessages.InvalidEmail);
        }
    }

    private string HashPassword(string password) {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string plainPassword, string hashedPassword) {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}