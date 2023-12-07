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
    public async Task<ActionResult<List<PeopleDto>>> GetPopularPeopleList([FromRoute] int pageId) {
        try {
            List<PeopleDto> popularPeoples = await _peopleService.GetListOfPopularPeople(pageId);
            return Ok(popularPeoples);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("person-details/{personId}")]
    public async Task<ActionResult<PersonDetailsDto>> GetPersonDetails([FromRoute] int personId) {
        try {
            PersonDetailsDto personDetails = await _peopleService.GetPersonDetailsById(personId);
            return Ok(personDetails);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("person-movie-pie-chart/{personId}")]
    public async Task<ActionResult<PersonMoviePieChartDto>> GetPersonMoviePieChart([FromRoute] int personId) {
        try {
            PersonMoviePieChartDto personMoviePieChart = await _peopleService.GetPersonMoviePieChart(personId);
            return Ok(personMoviePieChart);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}