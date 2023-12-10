
using ApiClient.api;
using Dto;
using Moq;
using Services;

namespace Tests.services;
            
    [TestFixture]
    public class PeopleServiceTests
    {
        private Mock<IPeopleClient> _peopleClientMock;
        private PeopleService _peopleService;

        [SetUp]
        public void SetUp()
        {
            _peopleClientMock = new Mock<IPeopleClient>();
            _peopleService = new PeopleService(_peopleClientMock.Object);
        }

        [Test]
        public async Task GetListOfPopularPeople_CallsPeopleClient_ReturnsListOfPeople()
        {
            // Arrange
            var expectedPeopleList = new List<PeopleDto> { /* Populate with test data */ };
            _peopleClientMock.Setup(client => client.GetListOfPopularPeople(It.IsAny<int>())).ReturnsAsync(expectedPeopleList);

            // Act
            var result = await _peopleService.GetListOfPopularPeople(It.IsAny<int>());

            // Assert
            Assert.That(result, Is.EqualTo(expectedPeopleList));
            _peopleClientMock.Verify(client => client.GetListOfPopularPeople(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetPersonDetailsById_CallsPeopleClient_ReturnsPersonDetails()
        {
            // Arrange
            var expectedPersonDetails = new PersonDetailsDto { /* Populate with test data */ };
            _peopleClientMock.Setup(client => client.GetPersonDetailsById(It.IsAny<int>())).ReturnsAsync(expectedPersonDetails);

            // Act
            var result = await _peopleService.GetPersonDetailsById(It.IsAny<int>());

            // Assert
            Assert.That(result, Is.EqualTo(expectedPersonDetails));
            _peopleClientMock.Verify(client => client.GetPersonDetailsById(It.IsAny<int>()), Times.Once);
        }

// Existing setup...

        [Test]
        public async Task GetPersonMoviePieChart_CallsPeopleClient_ReturnsPersonMoviePieChart()
        {
            // Arrange
            var expectedPieChart = new PersonMovieRolesDto { /* Populate with test data */ };
            _peopleClientMock.Setup(client => client.GetPersonMoviePieChart(It.IsAny<int>())).ReturnsAsync(expectedPieChart);

            // Act
            var result = await _peopleService.GetPersonMoviePieChart(It.IsAny<int>());

            // Assert
            Assert.That(result, Is.EqualTo(expectedPieChart));
            _peopleClientMock.Verify(client => client.GetPersonMoviePieChart(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetPersonMoviePopularityLineChart_CallsPeopleClient_ReturnsPersonMoviePopularityLineChart()
        {
            // Arrange
            var expectedLineChart = new List<PersonMoviePopularityDto> { /* Populate with test data */ };
            _peopleClientMock.Setup(client => client.GetPersonMoviePopularityLineChart(It.IsAny<int>())).ReturnsAsync(expectedLineChart);

            // Act
            var result = await _peopleService.GetPersonMoviePopularityLineChart(It.IsAny<int>());

            // Assert
            Assert.That(result, Is.EqualTo(expectedLineChart));
            _peopleClientMock.Verify(client => client.GetPersonMoviePopularityLineChart(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetPersonMovieGenreVariation_CallsPeopleClient_ReturnsPersonMovieGenreVariation()
        {
            // Arrange
            var expectedGenreVariation = new PersonMovieGenreVariationDto { /* Populate with test data */ };
            _peopleClientMock.Setup(client => client.GetPersonMovieGenreVariation(It.IsAny<int>())).ReturnsAsync(expectedGenreVariation);

            // Act
            var result = await _peopleService.GetPersonMovieGenreVariation(It.IsAny<int>());

            // Assert
            Assert.That(result, Is.EqualTo(expectedGenreVariation));
            _peopleClientMock.Verify(client => client.GetPersonMovieGenreVariation(It.IsAny<int>()), Times.Once);
        }


 
    }

