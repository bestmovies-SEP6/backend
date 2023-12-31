using Dto;
using Entity;

namespace Data.converters;

public static class UserConverter {
    public static UserDto ToDto(UserEntity userEntity) {
        return new UserDto() {
            Username = userEntity.Username,
            Email = userEntity.Email,
            Password = userEntity.Password
        };
    }

    public static UserEntity ToEntity(UserDto userDto) {
        return new UserEntity() {
            Username = userDto.Username,
            Email = userDto.Email,
            Password = userDto.Password
        };
    }
}