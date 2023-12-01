using Dto;
using Microsoft.AspNetCore.Mvc;
using Services.movie;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase {
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService) {
        _moviesService = moviesService;
    }

    [HttpGet, Route("details/{id}")]
    public async Task<ActionResult<MovieDetailsDto>> GetMovieDetailsById([FromRoute] int id) {
        try {
            MovieDetailsDto movieDetailsDto = await _moviesService.GetMovieDetailsById(id);
            return Ok(movieDetailsDto);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }

    }

    [HttpGet, Route("now-playing")]
    public async Task<ActionResult<List<MovieDto>>> GetNowPlaying() {
        try {
            List<MovieDto> nowPlayings = await _moviesService.GetNowPlaying();
            return Ok(nowPlayings);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("trending")]
    public async Task<ActionResult<List<MovieDto>>> GetTrending() {
        try {
            List<MovieDto> trendings = await _moviesService.GetTrending();
            return Ok(trendings);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }


    [HttpGet, Route("popular")]
    public async Task<ActionResult<List<MovieDto>>> GetPopular() {
        try {
            List<MovieDto> popular = await _moviesService.GetPopular();
            return Ok(popular);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("top-rated")]
    public async Task<ActionResult<List<MovieDto>>> GetTopRated() {
        try {
            List<MovieDto> topRated = await _moviesService.GetTopRated();
            return Ok(topRated);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

  

}