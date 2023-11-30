using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.wishlist;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class WishListController : ControllerBase {
    private IWIshListService _wishListService;

    public WishListController(IWIshListService wishListService) {
        _wishListService = wishListService;
    }

    [HttpPost, Route("{movieId}")]
    public async Task<ActionResult> AddMovieToWishList([FromRoute] int movieId) {
        try {
            string? loggedInUser = User.Identity!.Name;
            if (string.IsNullOrEmpty(loggedInUser))
                return BadRequest(ErrorMessages.LoginRequired);

            await _wishListService.AddMovieToWishList(loggedInUser, movieId);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}