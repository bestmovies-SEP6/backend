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

    [HttpDelete, Route("{reviewId}")]
    [Authorize]
    public async Task<ActionResult> DeleteReview([FromRoute] int reviewId) {
        try {
            string loggedInUser = GetLoggedInUser();
            await _reviewsService.DeleteReview(reviewId, loggedInUser);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("{movieId}")]
    public async Task<ActionResult<GetMovieReviewsResponseDto>> GetReviewsByMovieId([FromRoute] int movieId,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10) {
        try {
            GetMovieReviewsResponseDto responseDto = await _reviewsService.GetReviewsByMovieId(movieId, page, pageSize);
            return Ok(responseDto);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("average-rating/{movieId}")]
    public async Task<ActionResult<double>> GetAverageRatingByMovieId([FromRoute] int movieId) {
        try {
            double averageRating = await _reviewsService.GetAverageRatingByMovieId(movieId);
            return Ok(averageRating);
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