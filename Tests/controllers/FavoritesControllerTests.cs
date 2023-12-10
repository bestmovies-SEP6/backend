using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.favorites;
using WebApi.Controllers;

namespace Tests.controllers;

[TestFixture]
public class FavoritesControllerTests {
    private Mock<IFavoritesService> _favoritesServiceMock;
    private FavoritesController _favoritesController;

    [SetUp]
    public void Setup() {
        _favoritesServiceMock = new Mock<IFavoritesService>();
        // Mock HttpContext for user identity
        var httpContext = new Mock<HttpContext>();
        httpContext.Setup(c => c.User.Identity.Name).Returns("testuser");

        var controllerContext = new ControllerContext {
            HttpContext = httpContext.Object
        };

        _favoritesController = new FavoritesController(_favoritesServiceMock.Object) {
            ControllerContext = controllerContext
        };
    }

    [Test]
    public async Task AddMovieToFavorites_ReturnsOkResult_OnSuccess() {
        // Arrange
        int movieId = 1;
        _favoritesServiceMock.Setup(service => service.AddMovieToFavorites(It.IsAny<string>(), movieId))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _favoritesController.AddMovieToFavorites(movieId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
        var statusCodeResult = result as OkResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public async Task AddMovieToFavorites_ReturnsInternalServerError_OnException() {
        // Arrange
        int movieId = 1;
        _favoritesServiceMock.Setup(service => service.AddMovieToFavorites(It.IsAny<string>(), movieId))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _favoritesController.AddMovieToFavorites(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        var statusCodeResult = result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task RemoveMovieFromWishList_ReturnsOkResult_OnSuccess() {
        // Arrange
        int movieId = 1;
        _favoritesServiceMock.Setup(service => service.RemoveMovieFromFavorite(It.IsAny<string>(), movieId))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _favoritesController.RemoveMovieFromWishList(movieId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task RemoveMovieFromWishList_ReturnsBadRequest_OnException() {
        // Arrange
        int movieId = 1;
        _favoritesServiceMock.Setup(service => service.RemoveMovieFromFavorite(It.IsAny<string>(), movieId))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _favoritesController.RemoveMovieFromWishList(movieId);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task GetAllWishListedMovies_ReturnsOkResult_WithMovieDetailsList() {
        // Arrange
        var loggedInUser = "testuser";
        var movieDetailsList = new List<MovieDetailsDto> {new MovieDetailsDto()};
        _favoritesServiceMock.Setup(service => service.GetAllFavoriteMovies(loggedInUser))
            .ReturnsAsync(movieDetailsList);

        // Act
        var result = await _favoritesController.GetAllWishListedMovies();

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var okResult = result.Result as ObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<List<MovieDetailsDto>>());
    }

    [Test]
    public async Task GetAllWishListedMovies_ReturnsInternalServerError_OnException() {
        // Arrange
        var loggedInUser = "testuser";
        _favoritesServiceMock.Setup(service => service.GetAllFavoriteMovies(loggedInUser))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _favoritesController.GetAllWishListedMovies();

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var statusCodeResult = result.Result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }
}