using ApiClient.api;
using Data.dao.favorites;
using Data.dao.movies;
using Dto;
using Moq;
using Services.favorites;

namespace Tests.services;

[TestFixture]
public class FavoritesServiceTests {
    private Mock<IFavoritesDao> _favoritesDaoMock;
    private Mock<IMoviesDao> _moviesDaoMock;
    private Mock<IMoviesClient> _moviesClientMock;
    private FavoriteService _favoriteService;

    [SetUp]
    public void Setup() {
        _favoritesDaoMock = new Mock<IFavoritesDao>();
        _moviesDaoMock = new Mock<IMoviesDao>();
        _moviesClientMock = new Mock<IMoviesClient>();
        _favoriteService =
            new FavoriteService(_favoritesDaoMock.Object, _moviesDaoMock.Object, _moviesClientMock.Object);
    }

    [Test]
    public async Task AddMovieToFavorites_CallsAddIfNotExists_And_AddMovieToFavorites() {
        // Arrange
        string username = "testuser";
        int movieId = 1;

        // Act
        await _favoriteService.AddMovieToFavorites(username, movieId);

        // Assert
        _moviesDaoMock.Verify(dao => dao.AddIfNotExists(movieId), Times.Once);
        _favoritesDaoMock.Verify(dao => dao.AddMovieToFavorites(username, movieId), Times.Once);
    }

    [Test]
    public async Task GetAllFavoriteMovies_ReturnsListOfMovieDetailsDto() {
        // Arrange
        string username = "testuser";
        List<int> movieIds = new List<int> {1, 2, 3};
        List<MovieDetailsDto> movieDetailsList = new List<MovieDetailsDto> {
            new MovieDetailsDto {Id = 1, Title = "Movie 1"},
            new MovieDetailsDto {Id = 2, Title = "Movie 2"},
            new MovieDetailsDto {Id = 3, Title = "Movie 3"}
        };

        _favoritesDaoMock.Setup(dao => dao.GetAllFavoriteMovies(username))
            .ReturnsAsync(movieIds);

        _moviesClientMock.Setup(client => client.GetMovieDetailsById(It.IsAny<int>()))
            .ReturnsAsync((int id) => movieDetailsList.First(m => m.Id == id));

        // Act
        var result = await _favoriteService.GetAllFavoriteMovies(username);

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<List<MovieDetailsDto>>(result);
        Assert.That(movieIds.Count, Is.EqualTo(result.Count));
    }

    [Test]
    public async Task RemoveMovieFromFavorites_CallsRemoveMovieFromFavorites() {
        // Arrange
        string loggedInUser = "testuser";
        int movieId = 1;

        // Act
        await _favoriteService.RemoveMovieFromFavorite(loggedInUser, movieId);

        // Assert
        _favoritesDaoMock.Verify(dao => dao.RemoveMovieFromFavorites(loggedInUser, movieId), Times.Once);
    }


    // Rainy scenarios

    [Test]
    public void AddMovieToFavorites_ForwardsException_WhenMoviesDaoThrowsException()
    {
        // Arrange
        string username = "testuser";
        int movieId = 1;
        var expectedException = new Exception("Simulated exception from MoviesDao");

        _moviesDaoMock.Setup(dao => dao.AddIfNotExists(movieId))
            .ThrowsAsync(expectedException);

        // Act and Assert
        var exception = Assert.ThrowsAsync<Exception>(async () => await _favoriteService.AddMovieToFavorites(username, movieId));
        Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
    }

    [Test]
    public void GetAllFavoriteMovies_ForwardsException_WhenFavoritesDaoThrowsException()
    {
        // Arrange
        string username = "testuser";
        var expectedException = new Exception("Simulated exception from FavoritesDao");

        _favoritesDaoMock.Setup(dao => dao.GetAllFavoriteMovies(username))
            .ThrowsAsync(expectedException);

        // Act and Assert
        var exception = Assert.ThrowsAsync<Exception>(async () => await _favoriteService.GetAllFavoriteMovies(username));
        Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
    }

    [Test]
    public void RemoveMovieFromFavorite_ForwardsException_WhenFavoritesDaoThrowsException()
    {
        // Arrange
        string loggedInUser = "testuser";
        int movieId = 1;
        var expectedException = new Exception("Simulated exception from FavoritesDao");

        _favoritesDaoMock.Setup(dao => dao.RemoveMovieFromFavorites(loggedInUser, movieId))
            .ThrowsAsync(expectedException);

        // Act and Assert
        var exception = Assert.ThrowsAsync<Exception>(async () => await _favoriteService.RemoveMovieFromFavorite(loggedInUser, movieId));
        Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
    }

}