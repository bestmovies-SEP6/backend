using ApiClient.api;
using Dto;
using Microsoft.Extensions.Caching.Memory;

namespace Services.movie;

public class MovieService : IMovieService {
    private IMemoryCache _cache;
    private const string NowPlayingCacheKey = "NowPlayingData";
    private const string TrendingCacheKey = "TrendingData";
    private IMovieClient _movieClient;


    public MovieService(IMovieClient movieClient, IMemoryCache cache) {
        _movieClient = movieClient;
        _cache = cache;
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

    // Get value from the cache
    private T? GetValueFromCache<T>(string cacheKey) {
        _cache.TryGetValue(cacheKey, out T? valueFromCache);
        return valueFromCache;
    }

    private async Task RefreshNowPlayingCache() {
        Console.WriteLine("Fetching now playing movies from api");
        List<MovieDto> nowPlaying = await _movieClient.GetNowPlaying();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var dataToCache = new Tuple<List<MovieDto>, DateOnly>(nowPlaying, today);
        _cache.Set(NowPlayingCacheKey, dataToCache);
    }

    private async Task RefreshTrendingCache() {
        Console.WriteLine("Fetching trending playing movies from api");
        List<MovieDto> trending = await _movieClient.GetTrending();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var dataToCache = new Tuple<List<MovieDto>, DateOnly>(trending, today);
        _cache.Set(TrendingCacheKey, dataToCache);
    }
}