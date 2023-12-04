using Dto;

namespace ApiClient.api; 

public interface IPersonClient {
    Task<List<PersonDto>> GetPersonsByMovieId(int movieId);
}