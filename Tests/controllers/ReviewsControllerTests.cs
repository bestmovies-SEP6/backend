using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.reviews;
using WebApi.Controllers;

namespace Tests.controllers;

[TestFixture]
public class ReviewsControllerTests {
    private Mock<IReviewsService> _reviewsServiceMock;
    private ReviewsController _reviewsController;

    [SetUp]
    public void Setup() {
        _reviewsServiceMock = new Mock<IReviewsService>();
        // Mock HttpContext for user identity
        var httpContext = new Mock<HttpContext>();
        httpContext.Setup(c => c.User.Identity.Name).Returns("testuser");

        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext.Object
        };

        _reviewsController = new ReviewsController(_reviewsServiceMock.Object)
        {
            ControllerContext = controllerContext
        }; 
    }

    [Test]
    public async Task AddReview_ReturnsOkResult_OnSuccess() {
        // Arrange
        var reviewDto = new ReviewDto();
        _reviewsServiceMock.Setup(service => service.AddReview(reviewDto))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _reviewsController.AddReview(reviewDto);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task AddReview_ReturnsInternalServerError_OnException() {
        // Arrange
        var reviewDto = new ReviewDto();
        _reviewsServiceMock.Setup(service => service.AddReview(reviewDto))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _reviewsController.AddReview(reviewDto);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        var statusCodeResult = result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task DeleteReview_ReturnsOkResult_OnSuccess() {
        // Arrange
        int reviewId = 1;
        _reviewsServiceMock.Setup(service => service.DeleteReview(reviewId, It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _reviewsController.DeleteReview(reviewId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task DeleteReview_ReturnsInternalServerError_OnException() {
        // Arrange
        int reviewId = 1;
        _reviewsServiceMock.Setup(service => service.DeleteReview(reviewId, It.IsAny<string>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _reviewsController.DeleteReview(reviewId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        var statusCodeResult = result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task GetReviewsByMovieId_ReturnsOkResult_WithReviewsResponseDto() {
        // Arrange
        int movieId = 1;
        var reviewsResponseDto = new GetMovieReviewsResponseDto();
        _reviewsServiceMock.Setup(service => service.GetReviewsByMovieId(movieId, It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(reviewsResponseDto);

        // Act
        var result = await _reviewsController.GetReviewsByMovieId(movieId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as ObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<GetMovieReviewsResponseDto>());
    }

    [Test]
    public async Task GetReviewsByMovieId_ReturnsInternalServerError_OnException() {
        // Arrange
        int movieId = 1;
        _reviewsServiceMock.Setup(service => service.GetReviewsByMovieId(movieId, It.IsAny<int>(), It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _reviewsController.GetReviewsByMovieId(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var statusCodeResult = result.Result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task GetAverageRatingByMovieId_ReturnsOkResult_WithDoubleValue() {
        // Arrange
        int movieId = 1;
        double averageRating = 4.5;
        _reviewsServiceMock.Setup(service => service.GetAverageRatingByMovieId(movieId))
            .ReturnsAsync(averageRating);

        // Act
        var result = await _reviewsController.GetAverageRatingByMovieId(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var okResult = result.Result as ObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<double>());
    }

    [Test]
    public async Task GetAverageRatingByMovieId_ReturnsInternalServerError_OnException() {
        // Arrange
        int movieId = 1;
        _reviewsServiceMock.Setup(service => service.GetAverageRatingByMovieId(movieId))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _reviewsController.GetAverageRatingByMovieId(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var statusCodeResult = result.Result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }

    // Add more tests for other scenarios as needed
}