using Dto;

namespace ApiClient.api; 

public interface IMoviesClient {
    Task<MovieDetailsDto> GetMovieDetailsById(int id);
    Task<List<MovieDto>> GetNowPlaying();
    Task<List<MovieDto>> GetTrending();
    Task<List<MovieDto>> GetPopular();
    Task<List<MovieDto>> GetTopRated();
    Task<List<MovieDto>> GetSimilarMovies(int movieId);
    Task<SearchMoviesResponse> GetMovies(MovieFilterDto filterDto);
}