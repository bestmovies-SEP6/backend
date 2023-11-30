using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.wishlist;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class WishListsController : ControllerBase {
    private IWIshListService _wishListService;

    public WishListsController(IWIshListService wishListService) {
        _wishListService = wishListService;
    }

    [HttpPost, Route("{movieId}")]
    public async Task<ActionResult> AddMovieToWishList([FromRoute] int movieId) {
        try {
            string loggedInUser = GetLoggedInUser();

            await _wishListService.AddMovieToWishList(loggedInUser, movieId);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDetailsDto>>> GetAllWishListedMovies() {
        try {
            string loggedInUser = GetLoggedInUser();
            List<MovieDetailsDto> wishListedMovies = await _wishListService.GetAllWishListedMovies(loggedInUser);
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