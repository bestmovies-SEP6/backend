using Dto;

namespace Services.person; 

public interface IPersonService {
    Task<List<PersonDto>> GetPersonsByMovieId(int movieId);
}