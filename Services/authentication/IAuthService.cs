using Dto;

namespace Services.authentication; 

public interface IAuthService {
    Task<UserDto> ValidateUser(LoginDto loginDto);
    Task<UserDto> RegisterUser(UserDto userDto);
}