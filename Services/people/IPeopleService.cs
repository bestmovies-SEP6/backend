using ApiClient.api;
using Dto;

namespace Services;

public interface IPeopleService
{
    public Task<List<PeopleDto>> GetListOfPopularPeople(int pageId);
    public Task<PersonDetailsDto> GetPersonDetailsById(int personId);
    Task<PersonMovieRolesDto> GetPersonMoviePieChart(int personId);
    Task<List<PersonMoviePopularityDto>> GetPersonMoviePopularityLineChart(int personId);
    Task<PersonMovieGenreVariationDto> GetPersonMovieGenreVariation(int personId);
}