using Dto;

namespace Services.movie; 

public interface IMovieService {
    Task<List<MovieDto>> GetNowPlaying();
}