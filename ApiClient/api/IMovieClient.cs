using Dto;

namespace ApiClient.api; 

public interface IMovieClient {
    
    Task<List<MovieDto>> GetNowPlaying();
    Task<List<MovieDto>> GetTrending();
}