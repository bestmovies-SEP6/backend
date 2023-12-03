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
}