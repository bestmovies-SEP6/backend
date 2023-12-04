using Dto;
using Microsoft.AspNetCore.Mvc;
using Services.person;

namespace WebApi.Controllers; 

[ApiController]
[Route("[controller]")]
public class PersonController :  ControllerBase{
    private readonly IPersonService _personService;


    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet, Route("{movieId}")]
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