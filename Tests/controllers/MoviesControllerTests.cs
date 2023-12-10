using Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.movie;
using WebApi.Controllers;

namespace Tests.controllers;

[TestFixture]
public class MoviesControllerTests {
    private Mock<IMoviesService> _moviesServiceMock;
    private MoviesController _moviesController;

    [SetUp]
    public void SetUp() {
        _moviesServiceMock = new Mock<IMoviesService>();
        _moviesController = new MoviesController(_moviesServiceMock.Object);
    }

    [Test]
    public async Task GetMovieDetailsById_ReturnsOkResult_WhenMovieExists() {
        // Arrange
        int movieId = 1;
        var expectedMovieDetailsDto = new MovieDetailsDto();
        _moviesServiceMock.Setup(service => service.GetMovieDetailsById(movieId))
            .ReturnsAsync(expectedMovieDetailsDto);

        // Act
        var result = await _moviesController.GetMovieDetailsById(movieId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(expectedMovieDetailsDto, Is.EqualTo(okResult!.Value));
    }

    [Test]
    public async Task GetNowPlaying_ReturnsOkResult_WhenNowPlayingMoviesExist() {
        // Arrange
        var expectedNowPlaying = new List<MovieDto>();
        _moviesServiceMock.Setup(service => service.GetNowPlaying())
            .ReturnsAsync(expectedNowPlaying);

        // Act
        var result = await _moviesController.GetNowPlaying();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(expectedNowPlaying, Is.EqualTo(okResult!.Value));
    }


    [Test]
    public async Task GetTrending_ReturnsOkResult_WhenTrendingMoviesExist() {
        // Arrange
        var expectedTrendings = new List<MovieDto>();
        _moviesServiceMock.Setup(service => service.GetTrending())
            .ReturnsAsync(expectedTrendings);

        // Act
        var result = await _moviesController.GetTrending();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(expectedTrendings, Is.EqualTo(okResult!.Value));
    }
    [Test]
    public async Task GetPopular_ReturnsOkResult_WhenPopularMoviesExist() {
        // Arrange
        var expectedPopular = new List<MovieDto>();
        _moviesServiceMock.Setup(service => service.GetPopular())
            .ReturnsAsync(expectedPopular);

        // Act
        var result = await _moviesController.GetPopular();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(expectedPopular, Is.EqualTo(okResult!.Value));
    }
    [Test]
    public async Task GetTopRated_ReturnsOkResult_WhenTopRatedMoviesExist() {
        // Arrange
        var expectedTopRated = new List<MovieDto>();
        _moviesServiceMock.Setup(service => service.GetTopRated())
            .ReturnsAsync(expectedTopRated);

        // Act
        var result = await _moviesController.GetTopRated();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(expectedTopRated, Is.EqualTo(okResult!.Value));
    }

    [Test]
    public async Task GetSimilarMovies_ReturnsOkResult_WhenSimilarMoviesExist() {
        // Arrange
        int movieIdForSimilarMovies = 1;
        var expectedSimilarMovies = new List<MovieDto>();
        _moviesServiceMock.Setup(service => service.GetSimilarMovies(movieIdForSimilarMovies))
            .ReturnsAsync(expectedSimilarMovies);

        // Act
        var result = await _moviesController.GetSimilarMovies(movieIdForSimilarMovies);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(expectedSimilarMovies, Is.EqualTo(okResult!.Value));
    }

    [Test]
    public async Task GetMovies_ReturnsOkResult_WhenMoviesExist() {
        // Arrange
        string query = "searchQuery";
        int pageNo = 1;
        string region = "us";
        int? year = 2023;
        List<string> genres = new List<string> { "Action", "Drama" };
        var expectedSearchMoviesResponse = new SearchMoviesResponse();
        _moviesServiceMock.Setup(service => service.GetMovies(It.IsAny<MovieFilterDto>()))
            .ReturnsAsync(expectedSearchMoviesResponse);

        // Act
        var result = await _moviesController.GetMovies(query, pageNo, region, year, genres);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(expectedSearchMoviesResponse, Is.EqualTo(okResult!.Value));
    }



    // Edge cases
 
    [Test]
    public async Task GetMovieDetailsById_ReturnsInternalServerError_OnException()
    {
        // Arrange
        int movieId = 1;
        _moviesServiceMock.Setup(service => service.GetMovieDetailsById(movieId))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _moviesController.GetMovieDetailsById(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var statusCodeResult = result.Result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }


    [Test]
    public async Task GetNowPlaying_ReturnsInternalServerError_OnException()
    {
        // Arrange
        _moviesServiceMock.Setup(service => service.GetNowPlaying())
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _moviesController.GetNowPlaying();

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

     [Test]
    public async Task GetTrending_ReturnsInternalServerError_OnException()
    {
        // Arrange
        _moviesServiceMock.Setup(service => service.GetTrending())
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _moviesController.GetTrending();

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task GetPopular_ReturnsInternalServerError_OnException()
    {
        // Arrange
        _moviesServiceMock.Setup(service => service.GetPopular())
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _moviesController.GetPopular();

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task GetTopRated_ReturnsInternalServerError_OnException()
    {
        // Arrange
        _moviesServiceMock.Setup(service => service.GetTopRated())
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _moviesController.GetTopRated();

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task GetSimilarMovies_ReturnsInternalServerError_OnException()
    {
        // Arrange
        int movieIdForSimilarMovies = 1;
        _moviesServiceMock.Setup(service => service.GetSimilarMovies(movieIdForSimilarMovies))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _moviesController.GetSimilarMovies(movieIdForSimilarMovies);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task GetMovies_ReturnsBadRequest_OnInvalidModelState()
    {
        // Arrange
        _moviesController.ModelState.AddModelError("query", "Query is required");

        // Act
        var result = await _moviesController.GetMovies("null", 1, null, null, null);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task GetMovies_ReturnsInternalServerError_OnException()
    {
        // Arrange
        _moviesServiceMock.Setup(service => service.GetMovies(It.IsAny<MovieFilterDto>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _moviesController.GetMovies("query", 1, "region", 2023, new List<string>());

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }
}