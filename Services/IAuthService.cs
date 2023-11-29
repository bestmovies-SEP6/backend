using Dto;

namespace Services; 

public interface IAuthService {
    Task<UserDto> ValidateUser(LoginDto loginDto);
    Task<UserDto> RegisterUser(UserDto userDto);
}