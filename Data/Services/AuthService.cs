using Data.converters;
using Data.Entities;
using Dto;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Services;

public class AuthService : IAuthService {
    private readonly DatabaseContext _databaseContext;

    public AuthService(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task<UserDto> ValidateUser(LoginDto loginDto) {
        UserEntity? userEntity = await _databaseContext.Users.FindAsync(loginDto.Username);
        if (userEntity is null) {
            throw new Exception(ErrorMessages.UsernameNotFound);
        }

        if (!VerifyPassword(loginDto.Password, userEntity.Password)) {
            throw new Exception(ErrorMessages.IncorrectPassword);
        }

        return UserConverter.ToDto(userEntity);
    }

    public async Task<UserDto> RegisterUser(UserDto userDto) {
        try {
            UserEntity userEntityToBeAdded = UserConverter.ToEntity(userDto);
            userEntityToBeAdded.Password = HashPassword(userDto.Password);


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

    private string HashPassword(string password) {          
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string plainPassword, string hashedPassword) {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}