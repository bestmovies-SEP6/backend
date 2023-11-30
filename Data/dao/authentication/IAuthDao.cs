using Dto;

namespace Data.dao.authentication; 

public interface IAuthDao {
    Task<UserDto> GetUserByUsername(string username);
    Task<UserDto> AddUser(UserDto userDto);
    
}