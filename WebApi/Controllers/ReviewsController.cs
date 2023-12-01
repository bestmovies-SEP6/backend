using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.reviews;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewsController : ControllerBase {
    private readonly IReviewsService _reviewsService;

    public ReviewsController(IReviewsService reviewsService) {
        _reviewsService = reviewsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> AddReview([FromBody] ReviewDto reviewDto) {
        try {
            string loggedInUser = GetLoggedInUser();
            reviewDto.Author = loggedInUser;
            await _reviewsService.AddReview(reviewDto);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("{movieId}")]
    public async Task<ActionResult<List<ReviewDto>>> GetReviewsByMovieId([FromRoute] int movieId) {
        try {
            List<ReviewDto> reviewDtos = await _reviewsService.GetReviewsByMovieId(movieId);
            return Ok(reviewDtos);
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