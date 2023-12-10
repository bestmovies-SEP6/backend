using Data.dao.movies;
using Data.dao.reviews;
using Dto;
using Moq;
using Services.reviews;

namespace Tests.services;

    [TestFixture]
    public class ReviewsServiceTests
    {
        private Mock<IReviewsDao> _reviewsDaoMock;
        private Mock<IMoviesDao> _moviesDaoMock;
        private ReviewsService _reviewsService;

        [SetUp]
        public void SetUp()
        {
            _reviewsDaoMock = new Mock<IReviewsDao>();
            _moviesDaoMock = new Mock<IMoviesDao>();
            _reviewsService = new ReviewsService(_reviewsDaoMock.Object, _moviesDaoMock.Object);
        }

        [Test]
        public async Task AddReview_CallsMoviesDaoAndReviewsDao()
        {
            // Arrange
            var reviewDto = new ReviewDto
            {
                MovieId = 123, // Replace with a valid movie ID
                // Populate other properties as needed
            };

            // Act
            await _reviewsService.AddReview(reviewDto);

            // Assert
            _moviesDaoMock.Verify(dao => dao.AddIfNotExists(reviewDto.MovieId), Times.Once);
            _reviewsDaoMock.Verify(dao => dao.AddReview(reviewDto), Times.Once);
        }

        [Test]
        public async Task GetReviewsByMovieId_CallsMoviesDaoAndReviewsDao_ReturnsResponseDto()
        {
            // Arrange
            var movieId = 456; // Replace with a valid movie ID
            var page = 1;
            var pageSize = 10;

            // Mock MoviesDao and ReviewsDao methods if needed
            _moviesDaoMock.Setup(dao => dao.AddIfNotExists(456)).Returns(Task.CompletedTask);
            _reviewsDaoMock.Setup(mock => mock.GetReviewsByMovieId(movieId, page, pageSize))
                .ReturnsAsync(new GetMovieReviewsResponseDto() {Reviews = new List<ReviewDto>()});

            // Act
            var result = await _reviewsService.GetReviewsByMovieId(movieId, page, pageSize);

            // Assert
            _moviesDaoMock.Verify(dao => dao.AddIfNotExists(movieId), Times.Once);
            _reviewsDaoMock.Verify(dao => dao.GetReviewsByMovieId(movieId, page, pageSize), Times.Once);
            Assert.That(result, Is.TypeOf<GetMovieReviewsResponseDto>());
        }

        // Add more test cases for other methods in ReviewsService...

        // No [TearDown] method since no specific cleanup is needed
    }
