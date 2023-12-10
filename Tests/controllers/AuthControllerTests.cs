using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Services.authentication;
using WebApi.Controllers;

namespace Tests.controllers;

[TestFixture]
public class AuthControllerTests {
    private Mock<IConfiguration> _configurationMock;
    private Mock<IAuthService> _authServiceMock;
    private AuthController _authController;

    [SetUp]
    public void Setup() {
        _configurationMock = new Mock<IConfiguration>();
        _authServiceMock = new Mock<IAuthService>();
        _authController = new AuthController(_configurationMock.Object, _authServiceMock.Object);

        _configurationMock.Setup(x => x["JWT:SecretKey"]).Returns("your_secret_key_that_is_not_too_short");
        _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("your_issuer");
        _configurationMock.Setup(x => x["JWT:Audience"]).Returns("your_audience");
        _configurationMock.Setup(x => x["JWT:Subject"]).Returns("your_subject");
    }

    [Test]
    public async Task Login_ReturnsOkResult_WithJwtToken() {
        // Arrange
        var loginDto = new LoginDto();
        var userDto = new UserDto {Username = "testuser", Email = "test@example.com"};
        _authServiceMock.Setup(service => service.ValidateUser(loginDto))
            .ReturnsAsync(userDto);

        // Act
        var result = await _authController.Login(loginDto);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var okResult = result.Result as ObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<LoginResponseDto>());

        Console.Write(okResult.Value);
        var responseDto = okResult?.Value as LoginResponseDto;
        Assert.That(responseDto?.JwtToken, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task Login_ReturnsBadRequest_OnException() {
        // Arrange
        var loginDto = new LoginDto();
        _authServiceMock.Setup(service => service.ValidateUser(loginDto))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _authController.Login(loginDto);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task Register_ReturnsOkResult_WithJwtToken() {
        // Arrange
        var userDto = new UserDto {Username = "testuser", Email = "test@example.com"};
        _authServiceMock.Setup(service => service.RegisterUser(userDto))
            .ReturnsAsync(userDto);

        // Act
        var result = await _authController.Register(userDto);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<LoginResponseDto>());
        var responseDto = okResult?.Value as LoginResponseDto;
        Assert.That(responseDto?.JwtToken, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task Register_ReturnsBadRequest_OnException() {
        // Arrange
        var userDto = new UserDto();
        _authServiceMock.Setup(service => service.RegisterUser(userDto))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _authController.Register(userDto);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }
}