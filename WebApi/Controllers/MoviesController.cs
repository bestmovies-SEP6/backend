using System.ComponentModel.DataAnnotations;
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

    [HttpGet, Route("{movieId}/similar")]
    public async Task<ActionResult<List<MovieDto>>> GetSimilarMovies([FromRoute] int movieId) {
        try {
            List<MovieDto> similarMovies = await _moviesService.GetSimilarMovies(movieId);
            return Ok(similarMovies);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    // TODO: Think about what can be filtered with and update the method signature

    // https://developer.themoviedb.org/reference/search-movie
    public async Task<ActionResult<SearchMoviesResponse>> GetMovies([FromQuery, Required] string query,
        [FromQuery, Required] int pageNo,
        [FromQuery] string? region,
        [FromQuery] int? year,
        [FromQuery(Name = "genres")] List<string>? genres) {

        MovieFilterDto filterDto = new MovieFilterDto {
            Query = query,
            PageNo = pageNo,
            Region = region,
            Year = year,
            Genres = genres
        };
        try {
            if (query.Equals("null")) {
                return BadRequest("Query is required");
            }
            SearchMoviesResponse movies = await _moviesService.GetMovies(filterDto);
            return Ok(movies);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}