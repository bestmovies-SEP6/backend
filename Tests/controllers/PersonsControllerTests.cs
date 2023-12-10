using Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.person;
using WebApi.Controllers;

namespace Tests.controllers; 

[TestFixture]
public class PersonsControllerTests
{
    private Mock<IPersonService> _personServiceMock;
    private PersonsController _personsController;

    [SetUp]
    public void Setup()
    {
        _personServiceMock = new Mock<IPersonService>();
        _personsController = new PersonsController(_personServiceMock.Object);
    }

    [Test]
    public async Task GetPersonsByMovieId_ReturnsOkResult_WithPersonDtoList()
    {
        // Arrange
        int movieId = 1;
        var persons = new List<PersonDto> { new PersonDto() };
        _personServiceMock.Setup(service => service.GetPersonsByMovieId(movieId))
            .ReturnsAsync(persons);

        // Act
        var result = await _personsController.GetPersonsByMovieId(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var okResult = result.Result as ObjectResult;
        Assert.That(okResult?.Value, Is.InstanceOf<List<PersonDto>>());
    }

    [Test]
    public async Task GetPersonsByMovieId_ReturnsInternalServerError_OnException()
    {
        // Arrange
        int movieId = 1;
        _personServiceMock.Setup(service => service.GetPersonsByMovieId(movieId))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await _personsController.GetPersonsByMovieId(movieId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult?.StatusCode, Is.EqualTo(500));
    }
}
