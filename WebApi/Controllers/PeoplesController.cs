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
    
    [HttpGet, Route("person-movie-roles/{personId}")]
    public async Task<ActionResult<PersonMovieRolesDto>> GetPersonMovieRoles([FromRoute] int personId) {
        try {
            PersonMovieRolesDto personMovieRoles = await _peopleService.GetPersonMoviePieChart(personId);
            return Ok(personMovieRoles);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("person-movie-popularity/{personId}")]
    public async Task<ActionResult<List<PersonMoviePopularityDto>>> GetPersonMoviePopularity([FromRoute] int personId) {
        try {  
            List<PersonMoviePopularityDto> personMoviePopularityLineGraphDto = await _peopleService.GetPersonMoviePopularityLineChart(personId);
            return Ok(personMoviePopularityLineGraphDto);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("person-movie-genre-variation/{personId}")]
    public async Task<ActionResult<PersonMovieGenreVariationDto>> GetPersonMovieGenreVariation([FromRoute] int personId) {
        try {  
            Console.WriteLine("personId: " + personId);
            PersonMovieGenreVariationDto personMoviePopularityLineGraphDto = await _peopleService.GetPersonMovieGenreVariation(personId);
            return Ok(personMoviePopularityLineGraphDto);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}