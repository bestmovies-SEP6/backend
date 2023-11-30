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
            List<MovieDto> nowPlayings = await _movieService.GetNowPlaying();
            return Ok(nowPlayings);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("trending")]
    public async Task<ActionResult<List<MovieDto>>> GetTrending() {
        try {
            List<MovieDto> trendings = await _movieService.GetTrending();
            return Ok(trendings);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }


    [HttpGet, Route("popular")]
    public async Task<ActionResult<List<MovieDto>>> GetPopular() {
        try {
            List<MovieDto> popular = await _movieService.GetPopular();
            return Ok(popular);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("top-rated")]
    public async Task<ActionResult<List<MovieDto>>> GetTopRated() {
        try {
            List<MovieDto> topRated = await _movieService.GetTopRated();
            return Ok(topRated);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

  

}