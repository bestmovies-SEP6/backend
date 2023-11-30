using Dto;

namespace Services.movie; 

public interface IMovieService {
    Task<List<MovieDto>> GetNowPlaying();
    Task<List<MovieDto>> GetTrending();
    Task<List<MovieDto>> GetPopular();
    Task<List<MovieDto>> GetTopRated();
}