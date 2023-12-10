using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.favorites;

namespace WebApi.Controllers; 

[ApiController]
[Authorize]
[Route("[controller]")]
public class FavoritesController : ControllerBase {

    private readonly IFavoritesService _favoritesService;

    public FavoritesController(IFavoritesService favoritesService) {
        _favoritesService = favoritesService;
    }

    [HttpPost, Route("{movieId}")]
    public async Task<ActionResult> AddMovieToFavorites([FromRoute] int movieId) {
        try {
            string loggedInUser = GetLoggedInUser();

            await _favoritesService.AddMovieToFavorites(loggedInUser, movieId);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete, Route("{movieId}")]
    public async Task<ActionResult> RemoveMovieFromWishList([FromRoute] int movieId) {
        try {
            string loggedInUser = GetLoggedInUser();

            await _favoritesService.RemoveMovieFromFavorite(loggedInUser, movieId);
            return Ok();
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDetailsDto>>> GetAllWishListedMovies() {
        try {
            string loggedInUser = GetLoggedInUser();
            List<MovieDetailsDto> wishListedMovies = await _favoritesService.GetAllFavoriteMovies(loggedInUser);
            return Ok(wishListedMovies);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }

    }
    private string GetLoggedInUser() {
        string? loggedInUser = User.Identity!.Name;
        if (string.IsNullOrEmpty(loggedInUser))
            throw new Exception(ErrorMessages.LoginRequired);

        return loggedInUser;
    }
    
}