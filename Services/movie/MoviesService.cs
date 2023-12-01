using ApiClient.api;
using Dto;
using Microsoft.Extensions.Caching.Memory;

namespace Services.movie;

public class MoviesService : IMoviesService {
    private IMemoryCache _cache;
    private const string NowPlayingCacheKey = "NowPlayingData";
    private const string TrendingCacheKey = "TrendingData";
    private const string PopularCacheKey = "PopularData";
    private const string TopRatedCacheKey = "TopRatedData";
    private IMoviesClient _moviesClient;


    public MoviesService(IMoviesClient moviesClient, IMemoryCache cache) {
        _moviesClient = moviesClient;
        _cache = cache;
    }

    public Task<MovieDetailsDto> GetMovieDetailsById(int id) {
        return _moviesClient.GetMovieDetailsById(id);
    }

    public async Task<List<MovieDto>> GetNowPlaying() {
        // Try to get the cached now playing movies
        Tuple<List<MovieDto>, DateOnly>? nowPlayingFromCache =
            GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(NowPlayingCacheKey);

        // If value is null then refresh the cache
        if (nowPlayingFromCache is null) {
            await RefreshNowPlayingCache();
        }
        else {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            List<MovieDto> nowPlayingMovies = nowPlayingFromCache!.Item1;
            DateOnly lastUpdatedDate = nowPlayingFromCache!.Item2;

            // If the last updated date is not today, then refresh the cache
            if (!nowPlayingMovies.Any() || lastUpdatedDate != today) {
                await RefreshNowPlayingCache();
            }
        }

        // Here we know that the cache is up to date
        return GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(NowPlayingCacheKey)!.Item1;
    }

    public async Task<List<MovieDto>> GetTrending() {
        Tuple<List<MovieDto>, DateOnly>? trendingFromCache =
            GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(TrendingCacheKey);
        if (trendingFromCache is null) {
            await RefreshTrendingCache();
        }
        else {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            List<MovieDto> trendingMovies = trendingFromCache!.Item1;
            DateOnly lastUpdatedDate = trendingFromCache!.Item2;

            if (!trendingMovies.Any() || lastUpdatedDate != today) {
                await RefreshTrendingCache();
            }
        }

        return GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(TrendingCacheKey)!.Item1;
    }

    public async Task<List<MovieDto>> GetPopular() {
        Tuple<List<MovieDto>, DateOnly>? popularFromCache =
            GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(PopularCacheKey);
        if (popularFromCache is null) {
            await RefreshPopularCache();
        }
        else {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            List<MovieDto> trendingMovies = popularFromCache!.Item1;
            DateOnly lastUpdatedDate = popularFromCache!.Item2;

            if (!trendingMovies.Any() || lastUpdatedDate != today) {
                await RefreshPopularCache();
            }
        }

        return GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(PopularCacheKey)!.Item1;
    }

    public async Task<List<MovieDto>> GetTopRated() {
        Tuple<List<MovieDto>, DateOnly>? topRatedFromCache =
            GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(TopRatedCacheKey);
        if (topRatedFromCache is null) {
            await RefreshTopRatedCache();
        }
        else {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            List<MovieDto> trendingMovies = topRatedFromCache!.Item1;
            DateOnly lastUpdatedDate = topRatedFromCache!.Item2;

            if (!trendingMovies.Any() || lastUpdatedDate != today) {
                await RefreshTopRatedCache();
            }
        }

        return GetValueFromCache<Tuple<List<MovieDto>, DateOnly>>(TopRatedCacheKey)!.Item1;
    }



    // Get value from the cache
    private T? GetValueFromCache<T>(string cacheKey) {
        _cache.TryGetValue(cacheKey, out T? valueFromCache);
        return valueFromCache;
    }

    private async Task RefreshNowPlayingCache() {
        Console.WriteLine("Fetching now playing movies from api");
        List<MovieDto> nowPlaying = await _moviesClient.GetNowPlaying();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var dataToCache = new Tuple<List<MovieDto>, DateOnly>(nowPlaying, today);
        _cache.Set(NowPlayingCacheKey, dataToCache);
    }

    private async Task RefreshTrendingCache() {
        Console.WriteLine("Fetching trending movies from api");
        List<MovieDto> trending = await _moviesClient.GetTrending();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var dataToCache = new Tuple<List<MovieDto>, DateOnly>(trending, today);
        _cache.Set(TrendingCacheKey, dataToCache);
    }

    private async Task RefreshPopularCache() {
        Console.WriteLine("Fetching popular movies from api");
        List<MovieDto> popular = await _moviesClient.GetPopular();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var dataToCache = new Tuple<List<MovieDto>, DateOnly>(popular, today);
        _cache.Set(PopularCacheKey, dataToCache);
    }

    private async Task RefreshTopRatedCache() {
        Console.WriteLine("Fetching top rated movies from api");
        List<MovieDto> topRated = await _moviesClient.GetTopRated();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var dataToCache = new Tuple<List<MovieDto>, DateOnly>(topRated, today);
        _cache.Set(TopRatedCacheKey, dataToCache);
    }
}