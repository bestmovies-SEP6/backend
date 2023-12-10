using Data.dao.authentication;
using Dto;
using Moq;
using Services.authentication;

namespace Tests.services;

[TestFixture]
public class AuthServiceTests {
    private Mock<IAuthDao> _authDaoMock;
    private IAuthService _authService;

    [SetUp]
    public void SetUp() {
        _authDaoMock = new Mock<IAuthDao>();
        _authService = new AuthService(_authDaoMock.Object);
    }

    [Test]
    public async Task ValidateUser_CorrectCredentials_ReturnsUserDto() {
        // Arrange
        var loginDto = new LoginDto {Username = "validUser", Password = "validPassword"};
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(loginDto.Password);
        var userFromDatabase = new UserDto {Username = loginDto.Username, Password = hashedPassword};

        _authDaoMock.Setup(dao => dao.GetUserByUsername(loginDto.Username)).ReturnsAsync(userFromDatabase);

        // Act
        var result = await _authService.ValidateUser(loginDto);

        // Assert
        Assert.That(userFromDatabase, Is.EqualTo(result));
    }

    [Test]
    public void ValidateUser_IncorrectPassword_ThrowsException() {
        // Arrange
        var loginDto = new LoginDto {Username = "validUser", Password = "invalidPassword"};
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("validPassword");
        var userFromDatabase = new UserDto {Username = loginDto.Username, Password = hashedPassword};

        _authDaoMock.Setup(dao => dao.GetUserByUsername(loginDto.Username)).ReturnsAsync(userFromDatabase);

        // Act and Assert
        Assert.ThrowsAsync<Exception>(async () => await _authService.ValidateUser(loginDto));
    }

    [Test]
    public async Task RegisterUser_ValidUserDto_CallsAddUserAndReturnsUserDto() {
        // Arrange
        var userDto = new UserDto {Username = "newUser", Password = "validPassword", Email = "valid@email.com"};

        _authDaoMock.Setup(dao => dao.AddUser(userDto)).ReturnsAsync(userDto);

        // Act
        var result = await _authService.RegisterUser(userDto);

        // Assert
        _authDaoMock.Verify(dao => dao.AddUser(userDto), Times.Once);
        Assert.That(userDto, Is.EqualTo(result));
    }

    [Test]
    public void RegisterUser_Invalid_Username_ThrowsException() {
        // Arrange
        var userDto = new UserDto {
            Username = "", Password = "validPassword", Email = "valid@valid.com"
        };

        //Act and Assert
        Assert.ThrowsAsync<Exception>(async () => await _authService.RegisterUser(userDto));
    }

    [Test]
    public void RegisterUser_Invalid_Password_ThrowsException() {
        // Arrange
        var userDto = new UserDto {
            Username = "validEmail", Password = "g", Email = "valid@valid.com"
        };

        //Act and Assert
        Assert.ThrowsAsync<Exception>(async () => await _authService.RegisterUser(userDto));
    }

    [Test]
    public void RegisterUser_Invalid_Email_ThrowsException() {
        // Arrange
        var userDto = new UserDto {
            Username = "validEmail", Password = "validPassword", Email = "invalid.invalid.com"
        };

        //Act and Assert
        Assert.ThrowsAsync<Exception>(async () => await _authService.RegisterUser(userDto));
    }
}