using Dto;

namespace ApiClient.api;

public interface IPeopleClient
{
    Task<List<PeopleDto>> GetListOfPopularPeople(int pageId);
    Task<PersonDetailsDto> GetPersonDetailsById(int personId);
    
    Task<PersonMovieRolesDto> GetPersonMoviePieChart(int personId);
    Task<List<PersonMoviePopularityDto>> GetPersonMoviePopularityLineChart(int personId);
    Task<PersonMovieGenreVariationDto> GetPersonMovieGenreVariation(int personId);
}