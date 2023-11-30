using ApiClient.api;
using Dto;
using Microsoft.Extensions.Caching.Memory;

namespace Services.movie;

public class MovieService : IMovieService {
    private IMemoryCache _cache;
    private const string NowPlayingCacheKey = "NowPlayingData";
    private IMovieClient _movieClient;


    public MovieService(IMovieClient movieClient, IMemoryCache cache) {
        _movieClient = movieClient;
        _cache = cache;
    }

    public async Task<List<MovieDto>> GetNowPlaying() {
        // Try to get the cached now playing movies
        Tuple<List<MovieDto>, DateOnly>? valuesFromCache = GetValuesFromCache();

        // If value is null then refresh the cache
        if (valuesFromCache is null) {
            await RefreshNowPlayingCache();
        }
        else {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            List<MovieDto> nowPlayingMovies = valuesFromCache!.Item1;
            DateOnly lastUpdatedDate = valuesFromCache!.Item2;

            // If the last updated date is not today, then refresh the cache
            if (!nowPlayingMovies.Any() || lastUpdatedDate != today) {
                await RefreshNowPlayingCache();
            }
        }


        // Here we know that the cache is up to date
        return GetValuesFromCache()!.Item1;
    }

    // Get value from the cache
    private Tuple<List<MovieDto>, DateOnly>? GetValuesFromCache() {
        Tuple<List<MovieDto>, DateOnly>? moviesAndLastUpdated;
        bool gotValue = _cache.TryGetValue(NowPlayingCacheKey, out moviesAndLastUpdated);

        if (!gotValue || moviesAndLastUpdated is null) {
            return null;
        }

        return moviesAndLastUpdated;
    }

    private async Task RefreshNowPlayingCache() {

        Console.WriteLine("Fetching now playing movies from api");
        List<MovieDto> nowPlaying = await _movieClient.GetNowPlaying();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var dataToCache = new Tuple<List<MovieDto>, DateOnly>(nowPlaying, today);
        _cache.Set(NowPlayingCacheKey, dataToCache);
    }
}