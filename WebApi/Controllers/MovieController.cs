using Dto;
using Microsoft.AspNetCore.Mvc;
using Services.movie;

namespace WebApi.Controllers; 

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase {

    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService) {
        _movieService = movieService;
    }

    [HttpGet, Route("now-playing")]
    public async Task<ActionResult<List<MovieDto>>> GetNowPlaying() {
        try {
            List<MovieDto> nowPlayings= await _movieService.GetNowPlaying();
            return Ok(nowPlayings);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }   

    }
}