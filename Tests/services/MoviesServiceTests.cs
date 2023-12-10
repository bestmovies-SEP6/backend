using ApiClient.api;
using Dto;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Services.movie;

namespace Tests.services;

[TestFixture]
public class MoviesServiceTests {
    private Mock<IMoviesClient> _moviesClientMock;
    private MoviesService _moviesService;
    private IMemoryCache _memoryCache;

    [SetUp]
    public void SetUp() {
        _moviesClientMock = new Mock<IMoviesClient>();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _moviesService = new MoviesService(_moviesClientMock.Object, _memoryCache);
    }

    [Test]
    public async Task GetNowPlaying_CacheHasData_ReturnsCachedData() {
        // Arrange
        var expectedNowPlaying = GetExpectedData();
        _moviesClientMock.Setup(client => client.GetNowPlaying()).ReturnsAsync(expectedNowPlaying);

        // Make sure that the cache has data
        _memoryCache.Set("NowPlayingData", new Tuple<List<MovieDto>, DateOnly>(expectedNowPlaying, DateOnly.FromDateTime(DateTime.Now)));

        // Act            
        var result = await _moviesService.GetNowPlaying();
        // Assert
        _moviesClientMock.Verify(client => client.GetNowPlaying(), Times.Never);
    }

    [Test]
    public async Task GetNowPlaying_CacheIsStale_InvokesClient()
    {
        // Arrange
        var nowPlaying = GetExpectedData();

        // Set up the MoviesClientMock to return fresh data if GetNowPlaying is called
        _moviesClientMock.Setup(client => client.GetNowPlaying()).ReturnsAsync(nowPlaying);

        // Set up the memory cache with stale cached data (before today)
        _memoryCache.Set("NowPlayingData", new Tuple<List<MovieDto>, DateOnly>(nowPlaying, DateOnly.FromDateTime(DateTime.Now.AddDays(-1))));

        // Act
        var result = await _moviesService.GetNowPlaying();

        // Assert
        // Verify that the client was invoked because cached data is stale
        _moviesClientMock.Verify(client => client.GetNowPlaying(), Times.Once);
    }

    [Test]
    public async Task GetNowPlaying_NoCache_InvokesClient()
    {
        // Arrange
        var expectedNowPlaying = GetExpectedData();

        // Set up the MoviesClientMock to return fresh data if GetNowPlaying is called
        _moviesClientMock.Setup(client => client.GetNowPlaying()).ReturnsAsync(expectedNowPlaying);

        // Ensure that there is no cached data
        _memoryCache.Remove("NowPlayingData");

        // Act
        var result = await _moviesService.GetNowPlaying();

        // Assert
        // Verify that the client was invoked because there is no cached data
        _moviesClientMock.Verify(client => client.GetNowPlaying(), Times.Once);
    }

    [Test]
    public async Task GetMovieDetailsById_Calls_Client() {
        //Act
        var result = await _moviesService.GetMovieDetailsById(1);

        //Assert
        _moviesClientMock.Verify(client => client.GetMovieDetailsById(1), Times.Once);
    }


    private List<MovieDto> GetExpectedData() {
        return new List<MovieDto> {
            new MovieDto() {
                Id = 1,
                Title = "title",
                PosterPath = "posterPath",
                ReleaseDate = "releaseDate",
                VoteAverage = 2,
                BackDropPath = "backDropPath",
            }
        };
    }
}