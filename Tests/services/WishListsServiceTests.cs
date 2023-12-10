using ApiClient.api;
using Data.dao.movies;
using Data.dao.wishList;
using Dto;
using Moq;
using Services.wishlist;

namespace Tests.services;

[TestFixture]
public class WishListsServiceTests {
    private Mock<IWishListsDao> _wishListsDaoMock;
    private Mock<IMoviesDao> _moviesDaoMock;
    private Mock<IMoviesClient> _moviesClientMock;
    private WishListsService _wishListsService;

    [SetUp]
    public void Setup() {
        _wishListsDaoMock = new Mock<IWishListsDao>();
        _moviesDaoMock = new Mock<IMoviesDao>();
        _moviesClientMock = new Mock<IMoviesClient>();
        _wishListsService =
            new WishListsService(_wishListsDaoMock.Object, _moviesDaoMock.Object, _moviesClientMock.Object);
    }

    [Test]
    public async Task AddMovieToWishList_CallsAddIfNotExists_And_AddMovieToWishList() {
        // Arrange
        string username = "testuser";
        int movieId = 1;

        // Act
        await _wishListsService.AddMovieToWishList(username, movieId);

        // Assert
        _moviesDaoMock.Verify(dao => dao.AddIfNotExists(movieId), Times.Once);
        _wishListsDaoMock.Verify(dao => dao.AddMovieToWishList(username, movieId), Times.Once);
    }

    [Test]
    public async Task GetAllWishListedMovies_ReturnsListOfMovieDetailsDto() {
        // Arrange
        string username = "testuser";
        List<int> movieIds = new List<int> {1, 2, 3};
        List<MovieDetailsDto> movieDetailsList = new List<MovieDetailsDto> {
            new MovieDetailsDto {Id = 1, Title = "Movie 1"},
            new MovieDetailsDto {Id = 2, Title = "Movie 2"},
            new MovieDetailsDto {Id = 3, Title = "Movie 3"}
        };

        _wishListsDaoMock.Setup(dao => dao.GetAllWishListedMovies(username))
            .ReturnsAsync(movieIds);

        _moviesClientMock.Setup(client => client.GetMovieDetailsById(It.IsAny<int>()))
            .ReturnsAsync((int id) => movieDetailsList.First(m => m.Id == id));

        // Act
        var result = await _wishListsService.GetAllWishListedMovies(username);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<MovieDetailsDto>>(result);
        Assert.That(movieIds.Count, Is.EqualTo(result.Count));
    }

    [Test]
    public async Task RemoveMovieFromWishList_CallsRemoveMovieFromWishList() {
        // Arrange
        string loggedInUser = "testuser";
        int movieId = 1;

        // Act
        await _wishListsService.RemoveMovieFromWishList(loggedInUser, movieId);

        // Assert
        _wishListsDaoMock.Verify(dao => dao.RemoveMovieFromWishList(loggedInUser, movieId), Times.Once);
    }

    // Rainy scenarios

[Test]
public async Task AddMovieToWishList_ForwardsException_WhenMoviesDaoThrowsException()
{
    // Arrange
    string username = "testuser";
    int movieId = 1;
    var expectedException = new Exception("Simulated exception from MoviesDao");

    _moviesDaoMock.Setup(dao => dao.AddIfNotExists(movieId))
        .ThrowsAsync(expectedException);

    // Act and Assert
    var exception = Assert.ThrowsAsync<Exception>(async () => await _wishListsService.AddMovieToWishList(username, movieId));
    Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
}

[Test]
public async Task GetAllWishListedMovies_ForwardsException_WhenWishListsDaoThrowsException()
{
    // Arrange
    string username = "testuser";
    var expectedException = new Exception("Simulated exception from WishListsDao");

    _wishListsDaoMock.Setup(dao => dao.GetAllWishListedMovies(username))
        .ThrowsAsync(expectedException);

    // Act and Assert
    var exception = Assert.ThrowsAsync<Exception>(async () => await _wishListsService.GetAllWishListedMovies(username));
    Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
}

[Test]
public async Task GetAllWishListedMovies_ForwardsException_WhenMoviesClientThrowsException()
{
    // Arrange
    string username = "testuser";
    List<int> movieIds = new List<int> { 1, 2, 3 };
    var expectedException = new Exception("Simulated exception from MoviesClient");

    _wishListsDaoMock.Setup(dao => dao.GetAllWishListedMovies(username))
        .ReturnsAsync(movieIds);

    _moviesClientMock.Setup(client => client.GetMovieDetailsById(It.IsAny<int>()))
        .ThrowsAsync(expectedException);

    // Act and Assert
    var exception = Assert.ThrowsAsync<Exception>(async () => await _wishListsService.GetAllWishListedMovies(username));
    Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
}

[Test]
public async Task RemoveMovieFromWishList_ForwardsException_WhenWishListsDaoThrowsException()
{
    // Arrange
    string loggedInUser = "testuser";
    int movieId = 1;
    var expectedException = new Exception("Simulated exception from WishListsDao");

    _wishListsDaoMock.Setup(dao => dao.RemoveMovieFromWishList(loggedInUser, movieId))
        .ThrowsAsync(expectedException);

    // Act and Assert
    var exception = Assert.ThrowsAsync<Exception>(async () => await _wishListsService.RemoveMovieFromWishList(loggedInUser, movieId));
    Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
}


}