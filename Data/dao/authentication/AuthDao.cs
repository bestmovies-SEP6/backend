using Data.converters;
using Dto;
using Entity;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.dao.authentication;

public class AuthDao : IAuthDao {
    private readonly DatabaseContext _databaseContext;

    public AuthDao(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task<UserDto> GetUserByUsername(string username) {
        UserEntity? userEntity = await _databaseContext.Users.FindAsync(username);
        if (userEntity is null) {
            throw new Exception(ErrorMessages.UsernameNotFound);
        }

        return UserConverter.ToDto(userEntity);
    }

    public async Task<UserDto> AddUser(UserDto userDto) {
        try {

            Console.WriteLine(userDto);
            UserEntity userEntityToBeAdded = UserConverter.ToEntity(userDto);

            EntityEntry<UserEntity> addedUser = await _databaseContext.Users.AddAsync(userEntityToBeAdded);
            await _databaseContext.SaveChangesAsync();

            return UserConverter.ToDto(addedUser.Entity);
        }
        catch (Exception e) {
            if (e is UniqueConstraintException) {
                throw new Exception(ErrorMessages.UsernameAlreadyExists);
            }
            throw;
        }    
    }
}