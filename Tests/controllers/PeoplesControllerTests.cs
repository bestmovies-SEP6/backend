
using Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using WebApi.Controllers;

namespace Tests.controllers;

[TestFixture]
public class PeoplesControllerTests
{
    private Mock<IPeopleService> _peopleServiceMock;
    private PeoplesController _peoplesController;

    [SetUp]
    public void Setup()
    {
        _peopleServiceMock = new Mock<IPeopleService>();
        _peoplesController = new PeoplesController(_peopleServiceMock.Object);
    }

    [Test]
    public async Task GetPopularPeopleList_ReturnsOkResult_WithPeopleList()
    {
        // Arrange
        int pageId = 1;
        var peopleList = new List<PeopleDto> { new PeopleDto() };
        _peopleServiceMock.Setup(service => service.GetListOfPopularPeople(pageId))
            .ReturnsAsync(peopleList);

        // Act
        var result = await _peoplesController.GetPopularPeopleList(pageId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var okResult = result.Result as ObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<List<PeopleDto>>());
    }

    [Test]
    public async Task GetPopularPeopleList_ReturnsInternalServerError_OnException()
    {
        // Arrange
        int pageId = 1;
        _peopleServiceMock.Setup(service => service.GetListOfPopularPeople(pageId))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _peoplesController.GetPopularPeopleList(pageId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var statusCodeResult = result.Result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task GetPersonDetails_ReturnsOkResult_WithPersonDetails()
    {
        // Arrange
        int personId = 1;
        var personDetails = new PersonDetailsDto();
        _peopleServiceMock.Setup(service => service.GetPersonDetailsById(personId))
            .ReturnsAsync(personDetails);

        // Act
        var result = await _peoplesController.GetPersonDetails(personId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<PersonDetailsDto>());
    }

    [Test]
    public async Task GetPersonDetails_ReturnsInternalServerError_OnException()
    {
        // Arrange
        int personId = 1;
        _peopleServiceMock.Setup(service => service.GetPersonDetailsById(personId))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _peoplesController.GetPersonDetails(personId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var statusCodeResult = result.Result as ObjectResult;
        Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
    }

[Test]
public async Task GetPersonMovieRoles_ReturnsOkResult_WithPersonMovieRoles()
{
    // Arrange
    int personId = 1;
    var personMovieRoles = new PersonMovieRolesDto();
    _peopleServiceMock.Setup(service => service.GetPersonMoviePieChart(personId))
        .ReturnsAsync(personMovieRoles);

    // Act
    var result = await _peoplesController.GetPersonMovieRoles(personId);

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result.Result);
    var okResult = result.Result as OkObjectResult;
    Assert.That(okResult?.Value, Is.InstanceOf<PersonMovieRolesDto>());
}

[Test]
public async Task GetPersonMovieRoles_ReturnsInternalServerError_OnException()
{
    // Arrange
    int personId = 1;
    _peopleServiceMock.Setup(service => service.GetPersonMoviePieChart(personId))
        .ThrowsAsync(new Exception("Simulated exception"));

    // Act
    var result = await _peoplesController.GetPersonMovieRoles(personId);

    // Assert
    Assert.IsInstanceOf<ObjectResult>(result.Result);
    var statusCodeResult = result.Result as ObjectResult;
    Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
}

[Test]
public async Task GetPersonMoviePopularity_ReturnsOkResult_WithPersonMoviePopularity()
{
    // Arrange
    int personId = 1;
    var personMoviePopularity = new List<PersonMoviePopularityDto> { new PersonMoviePopularityDto() };
    _peopleServiceMock.Setup(service => service.GetPersonMoviePopularityLineChart(personId))
        .ReturnsAsync(personMoviePopularity);

    // Act
    var result = await _peoplesController.GetPersonMoviePopularity(personId);

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result.Result);
    var okResult = result.Result as OkObjectResult;
    Assert.That(okResult?.Value, Is.InstanceOf<List<PersonMoviePopularityDto>>());
}

[Test]
public async Task GetPersonMoviePopularity_ReturnsInternalServerError_OnException()
{
    // Arrange
    int personId = 1;
    _peopleServiceMock.Setup(service => service.GetPersonMoviePopularityLineChart(personId))
        .ThrowsAsync(new Exception("Simulated exception"));

    // Act
    var result = await _peoplesController.GetPersonMoviePopularity(personId);

    // Assert
    Assert.IsInstanceOf<ObjectResult>(result.Result);
    var statusCodeResult = result.Result as ObjectResult;
    Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
}

[Test]
public async Task GetPersonMovieGenreVariation_ReturnsOkResult_WithPersonMovieGenreVariation()
{
    // Arrange
    int personId = 1;
    var personMovieGenreVariation = new PersonMovieGenreVariationDto();
    _peopleServiceMock.Setup(service => service.GetPersonMovieGenreVariation(personId))
        .ReturnsAsync(personMovieGenreVariation);

    // Act
    var result = await _peoplesController.GetPersonMovieGenreVariation(personId);

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result.Result);
    var okResult = result.Result as OkObjectResult;
    Assert.That(okResult?.Value, Is.InstanceOf<PersonMovieGenreVariationDto>());
}

[Test]
public async Task GetPersonMovieGenreVariation_ReturnsInternalServerError_OnException()
{
    // Arrange
    int personId = 1;
    _peopleServiceMock.Setup(service => service.GetPersonMovieGenreVariation(personId))
        .ThrowsAsync(new Exception("Simulated exception"));

    // Act
    var result = await _peoplesController.GetPersonMovieGenreVariation(personId);

    // Assert
    Assert.IsInstanceOf<ObjectResult>(result.Result);
    var statusCodeResult = result.Result as ObjectResult;
    Assert.That(statusCodeResult!.StatusCode, Is.EqualTo(500));
}


}
