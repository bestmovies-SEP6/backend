using Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries = new[] {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DatabaseContext _databaseContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DatabaseContext databaseContext) {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    // [HttpGet]
    // [Route("/user")]
    // public IEnumerable<USer> GetUsers() {
    //     return _databaseContext.GetUser();
    // }
}