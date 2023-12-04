using ApiClient.api;
using Dto;

namespace Services.person; 

public class PersonService : IPersonService {

    private readonly IPersonClient _personClient;

    public PersonService(IPersonClient personClient) {
        _personClient = personClient;
    }

    public async Task<List<PersonDto>> GetPersonsByMovieId(int movieId) {
        return await _personClient.GetPersonsByMovieId(movieId);
    }
}