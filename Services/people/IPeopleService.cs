using ApiClient.api;
using Dto;

namespace Services;

public interface IPeopleService
{
    public Task<List<PeopleDto>> GetListOfPopularPeople(int pageId);
}