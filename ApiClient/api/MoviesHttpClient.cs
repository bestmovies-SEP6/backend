using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ApiClient.util;
using Dto;

namespace ApiClient.api;

public class MoviesHttpClient : IMoviesClient {

    private class MovieIdResponseRoot {
        [JsonPropertyName("results")]
        public List<MovieDto>? Results { get; set; }
    }



    private const string URL = "https://api.themoviedb.org/3";

    public async Task<MovieDetailsDto> GetMovieDetailsById(int id) {
        MovieDetailsDto movieDetails = await HttpClientUtil.Get<MovieDetailsDto>($"{URL}/movie/{id}");
        return ConvertMovieDetails(movieDetails);
    }

    public async Task<List<MovieDto>> GetNowPlaying() {
        MovieIdResponseRoot rootObject =
            await HttpClientUtil.Get<MovieIdResponseRoot>($"{URL}/movie/now_playing");

        return ConvertMovies(rootObject);
    }

    public async Task<List<MovieDto>> GetTrending() {
        MovieIdResponseRoot rootObject =
            await HttpClientUtil.Get<MovieIdResponseRoot>($"{URL}/trending/all/day");
        return ConvertMovies(rootObject);
    }

    public async Task<List<MovieDto>> GetPopular() {
        MovieIdResponseRoot rootObject =
            await HttpClientUtil.Get<MovieIdResponseRoot>($"{URL}/movie/popular");
        return ConvertMovies(rootObject);
    }

    public async Task<List<MovieDto>> GetTopRated() {
        MovieIdResponseRoot rootObject =
            await HttpClientUtil.Get<MovieIdResponseRoot>($"{URL}/movie/top_rated");
        return ConvertMovies(rootObject);
    }

    public async Task<List<MovieDto>> GetSimilarMovies(int movieId) {
        MovieIdResponseRoot rootObject = await HttpClientUtil.Get<MovieIdResponseRoot>($"{URL}/movie/{movieId}/similar");
        return ConvertMovies(rootObject);
    }

    private List<MovieDto> ConvertMovies(MovieIdResponseRoot rootObject) {
        if (rootObject.Results is null) {
            throw new Exception("Something went wrong while fetching now playing movies from tmdb api");
        }

        var filteredList = rootObject.Results.Where(movie => movie.Title is not null).ToList();

        foreach (MovieDto result in filteredList) {
            result.PosterPath = $"https://image.tmdb.org/t/p/original{result.PosterPath}";
            result.VoteAverage = result.VoteAverage / 2;
            result.BackDropPath = $"https://image.tmdb.org/t/p/original{result.BackDropPath}";
        }

        return filteredList;
    }

    private MovieDetailsDto ConvertMovieDetails(MovieDetailsDto movieDetails) {
        movieDetails.PosterPath = $"https://image.tmdb.org/t/p/original{movieDetails.PosterPath}";
        movieDetails.VoteAverage = movieDetails.VoteAverage / 2;
        movieDetails.BackdropPath = $"https://image.tmdb.org/t/p/original{movieDetails.BackdropPath}";

        foreach (var company in movieDetails.ProductionCompanies!) {
            company.LogoPath = $"https://image.tmdb.org/t/p/original{company.LogoPath}";
        }

        return movieDetails;
    }
}