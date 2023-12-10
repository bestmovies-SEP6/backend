using System.Security.Claims;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.wishlist;
using WebApi.Controllers;

namespace Tests.controllers;

[TestFixture]
public class WishListsControllerTests {
    private Mock<IWIshListsService> _wishListsServiceMock;
    private WishListsController _wishListsController;

    [SetUp]
    public void Setup() {
        _wishListsServiceMock = new Mock<IWIshListsService>();
        _wishListsController = new WishListsController(_wishListsServiceMock.Object);
    }

    [Test]
    public async Task AddMovieToWishList_ReturnsOkResult_OnSuccess() {
        // Arrange
        int movieId = 1;
        _wishListsServiceMock.Setup(service => service.AddMovieToWishList(It.IsAny<string>(), movieId))
            .Returns(Task.CompletedTask);

        var controller = SetupControllerWithLoggedInUser();

        // Act
        var result = await controller.AddMovieToWishList(movieId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task AddMovieToWishList_ReturnsInternalServerError_OnException() {
        // Arrange
        int movieId = 1;
        _wishListsServiceMock.Setup(service => service.AddMovieToWishList(It.IsAny<string>(), movieId))
            .ThrowsAsync(new Exception("Simulated exception"));

        var controller = SetupControllerWithLoggedInUser();

        // Act
        var result = await controller.AddMovieToWishList(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.That(objectResult?.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task RemoveMovieFromWishList_ReturnsOkResult_OnSuccess() {
        // Arrange
        int movieId = 1;
        _wishListsServiceMock.Setup(service => service.RemoveMovieFromWishList(It.IsAny<string>(), movieId))
            .Returns(Task.CompletedTask);

        var controller = SetupControllerWithLoggedInUser();

        // Act
        var result = await controller.RemoveMovieFromWishList(movieId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task RemoveMovieFromWishList_ReturnsBadRequestResult_OnException() {
        // Arrange
        int movieId = 1;
        _wishListsServiceMock.Setup(service => service.RemoveMovieFromWishList(It.IsAny<string>(), movieId))
            .ThrowsAsync(new Exception("Simulated exception"));

        var controller = SetupControllerWithLoggedInUser();

        // Act
        var result = await controller.RemoveMovieFromWishList(movieId);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task GetAllWishListedMovies_ReturnsOkResult_WithMovieDetailsDtoList() {
        // Arrange
        var wishListedMovies = new List<MovieDetailsDto> {new MovieDetailsDto()};
        _wishListsServiceMock.Setup(service => service.GetAllWishListedMovies(It.IsAny<string>()))
            .ReturnsAsync(wishListedMovies);

        var controller = SetupControllerWithLoggedInUser();

        // Act
        var result = await controller.GetAllWishListedMovies();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<List<MovieDetailsDto>>());
    }

    [Test]
    public async Task GetAllWishListedMovies_ReturnsInternalServerError_OnException() {
        // Arrange
        _wishListsServiceMock.Setup(service => service.GetAllWishListedMovies(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        var controller = SetupControllerWithLoggedInUser();

        // Act
        var result = await controller.GetAllWishListedMovies();

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult?.StatusCode, Is.EqualTo(500));
    }

    private WishListsController SetupControllerWithLoggedInUser() {
        var controller = new WishListsController(_wishListsServiceMock.Object);

        // Set up the controller context with a mock user
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Name, "testuser"),
            // Add other claims as needed
        }, "mock"));

        controller.ControllerContext = new ControllerContext {
            HttpContext = new DefaultHttpContext {User = user}
        };

        return controller;
    }
}