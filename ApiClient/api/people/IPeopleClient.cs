using Dto;

namespace ApiClient.api;

public interface IPeopleClient
{
    Task<List<PeopleDto>> GetListOfPopularPeople(int pageId);
    
}