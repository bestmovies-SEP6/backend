using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PeoplesController : ControllerBase
{
    private IPeopleService _peopleService;
    
    public PeoplesController(IPeopleService peopleService)
    {
        _peopleService = peopleService;
    }

    [HttpGet, Route("popular-person/{pageId}")]
    public async Task<ActionResult<List<PeopleDto>>> GetNowPlaying([FromRoute] int pageId) {
        try {
            List<PeopleDto> popularPeoples = await _peopleService.GetListOfPopularPeople(pageId);
            return Ok(popularPeoples);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}