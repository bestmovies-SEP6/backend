using Dto;

namespace ApiClient.api;

public interface IPeopleClient
{
    Task<List<PeopleDto>> GetListOfPopularPeople(int pageId);
    Task<PersonDetailsDto> GetPersonDetailsById(int personId);
    
    Task<PersonMoviePieChartDto> GetPersonMoviePieChart(int personId);
}