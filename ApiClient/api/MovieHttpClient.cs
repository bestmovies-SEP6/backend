using System.Text.Json;
using System.Text.Json.Nodes;
using ApiClient.apiMap;
using ApiClient.util;
using Dto;

namespace ApiClient.api;

public class MovieHttpClient : IMovieClient {
    public async Task<List<MovieDto>> GetNowPlaying() {
        MovieIdResponseRoot rootObject =
            await HttpClientUtil.Get<MovieIdResponseRoot>("https://api.themoviedb.org/3/movie/now_playing");

        return ConvertMovies(rootObject);

    }

    public async Task<List<MovieDto>> GetTrending() {
        MovieIdResponseRoot rootObject =
            await HttpClientUtil.Get<MovieIdResponseRoot>("https://api.themoviedb.org/3/trending/all/day");
        return ConvertMovies(rootObject); 
    }

    private List<MovieDto> ConvertMovies(MovieIdResponseRoot rootObject) {
        if (rootObject.Results is null) {
            throw new Exception("Something went wrong while fetching now playing movies from tmdb api");
        }

        foreach (MovieDto result in rootObject.Results) {
            result.PosterPath = $"https://image.tmdb.org/t/p/original{result.PosterPath}";
            result.VoteAverage = result.VoteAverage / 2;
        }

        return rootObject.Results;
    }
}