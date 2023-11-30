using Dto;

namespace ApiClient.api; 

public interface IMovieClient {
    Task<MovieDetailsDto> GetMovieDetailsById(int id);
    Task<List<MovieDto>> GetNowPlaying();
    Task<List<MovieDto>> GetTrending();
    Task<List<MovieDto>> GetPopular();
    Task<List<MovieDto>> GetTopRated();
}