using ApiClient.api;
using Dto;

namespace Services;

public class PeopleService : IPeopleService
{
    private IPeopleClient _peopleClient;

    public PeopleService(IPeopleClient peopleClient)
    {
        _peopleClient = peopleClient;
    }

    public Task<List<PeopleDto>> GetListOfPopularPeople(int pageId)
    {
        return _peopleClient.GetListOfPopularPeople(pageId);
    }

    public Task<PersonDetailsDto> GetPersonDetailsById(int personId)
    {
        return _peopleClient.GetPersonDetailsById(personId);
    }

    public Task<PersonMovieRolesDto> GetPersonMoviePieChart(int personId)
    {
        return _peopleClient.GetPersonMoviePieChart(personId);
    }

    public Task<List<PersonMoviePopularityDto>> GetPersonMoviePopularityLineChart(int personId)
    {
        return _peopleClient.GetPersonMoviePopularityLineChart(personId);
    }

    public Task<PersonMovieGenreVariationDto> GetPersonMovieGenreVariation(int personId)
    {
        return _peopleClient.GetPersonMovieGenreVariation(personId);
    }
}