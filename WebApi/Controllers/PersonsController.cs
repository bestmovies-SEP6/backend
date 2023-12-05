using Dto;
using Microsoft.AspNetCore.Mvc;
using Services.person;

namespace WebApi.Controllers; 

[ApiController]
[Route("[controller]")]
public class PersonsController :  ControllerBase{
    private readonly IPersonService _personService;


    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet, Route("movie/{movieId}")]
    public async Task<ActionResult<List<PersonDto>>> GetPersonsByMovieId([FromRoute] int movieId)
    {
        try {
            var persons = await _personService.GetPersonsByMovieId(movieId);
            return Ok(persons);
        }
        catch (Exception e) {
            throw;
            return StatusCode(500, e.Message);
        }                       
    }



    
}